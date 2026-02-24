using System.Linq;
using System.Text;
using System.Text.Json;
using Docker.DotNet;
using Docker.DotNet.Models;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Services;

internal sealed class DockerManager : IDockerManager
{
    private DockerClient Client { get; set; } = new DockerClientConfiguration(
            new Uri("npipe://./pipe/docker_engine"))
            .CreateClient();
    private string TempPath { get; set; } = "temp";

    public sealed class ContainerStatsData
    {
        public double CpuPercentage { get; set; }
        public double MemoryUsageMB { get; set; }
        public double MemoryLimitMB { get; set; }
        public double MemoryPercentage { get; set; }
        public DateTime Timestamp { get; set; }
    }

    #region public

    public async Task<bool> ImageExistsAsync(string imageName, CancellationToken cancellationToken)
    {
        try
        {
            await Client.Images.InspectImageAsync(imageName);
            return true;
        }
        catch (DockerImageNotFoundException)
        {
            return false;
        }
    }

    public async Task<bool> LoadDockerImageAsync(Stream tarStream, CancellationToken cancellationToken)
    {
        try
        {
            var parameters = new ImageLoadParameters
            {
                Quiet = false
            };

            await Client.Images.LoadImageAsync(parameters, tarStream, new Progress<JSONMessage>(), cancellationToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteDockerImageAsync(Compiler compiler, CancellationToken cancellationToken)
    {
        var deleteParams = new ImageDeleteParameters
        {
            Force = false,
        };

        try
        {
            await Client.Images.DeleteImageAsync(compiler.ImageName, deleteParams, cancellationToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> ExecuteCodeAsync(Compiler compiler, Guid runId, string code, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }

        var fileName = $"{Guid.NewGuid()}.cpp";
        var filePath = Path.Combine(TempPath, runId.ToString(), fileName);
        var compiledPath = Path.Combine(TempPath, runId.ToString(), Path.GetFileNameWithoutExtension(fileName));

        await File.WriteAllTextAsync(filePath, code, cancellationToken);

        var output = await ExecuteAsync(compiler, runId, fileName, cancellationToken);

        File.Delete(filePath);
        File.Delete(compiledPath);

        return output;
    }

    public async Task<string> ExecuteCodeAsync(Compiler compiler, Guid runId, Test test, List<HeaderFile> headers,
        CancellationToken cancellationToken)
    {
        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }

        var folder = Path.Combine(TempPath, runId.ToString());

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var filePath = Path.Combine(folder, test.Name);

        File.Copy(test.Path, filePath);

        foreach (var header in headers)
        {
            var headerPath = Path.Combine(folder, header!.Name);

            File.Copy(header.Path, headerPath);
        }

        var output = await ExecuteAsync(compiler, runId, test.Name, cancellationToken);

        Directory.Delete(folder, recursive: true);

        return output;
    }

    public async Task<string> ExecuteAsync(Compiler compiler, Guid runId, string fileName, CancellationToken cancellationToken)
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

        var fullPath = Path.GetFullPath(TempPath);

        var config = new CreateContainerParameters
        {
            Image = compiler.ImageName,
            Cmd = [compiler.CommandName, $"/src/{fileName}","-I", "src", "-o", $"/src/{nameWithoutExtension}"],
            WorkingDir = "/src",
            HostConfig = new HostConfig
            {
                Binds =
                [
                    $"{fullPath}/{runId}:/src:rw"
                ],
                Memory = 1024 * 1024 * 1024,
            },
            AttachStdout = true,
            AttachStderr = true,
        };

        var createResponse = await Client.Containers.CreateContainerAsync(config, cancellationToken);
        var containerId = createResponse.ID;
        var statsHistory = new List<ContainerStatsData>();

        try
        {
            var cts = new CancellationTokenSource();

            var monitoringTask = Task.Run(async () =>
            {
                await MonitorContainerStatsAsync(containerId, statsHistory, cts.Token);
            }, cts.Token);

            await Task.Delay(200, cancellationToken);

            var startTime = DateTime.UtcNow;
            Console.WriteLine($"Starting compilation of {fileName}...");

            await Client.Containers.StartContainerAsync(containerId, new ContainerStartParameters(), cancellationToken);

            var waitResult = await Client.Containers.WaitContainerAsync(containerId, cancellationToken);

            cts.Cancel();
            try { await monitoringTask; } catch { }

            Console.WriteLine($"\nCompilation completed in: {DateTime.UtcNow - startTime}");

            var logs = await GetContainerOutputAsync(containerId);

            DisplayStatsSummary(statsHistory);

            return logs;
        }
        finally
        {
            await Cleanup(containerId);
        }
    }

    public async Task<Dictionary<string, bool>> CheckDockersAsync(List<string> imageNames, CancellationToken cancellationToken)
    {
        var dockerExistence = new Dictionary<string, bool>();

        var images = await Client.Images.ListImagesAsync(new ImagesListParameters(), cancellationToken);

        var repoTags = images
            .SelectMany(x => x.RepoTags)
            .ToList();

        foreach (var imageName in imageNames)
        {
            dockerExistence.Add(imageName, repoTags.Contains(imageName));
        }

        return dockerExistence;
    }

    #endregion

    #region private

    [Obsolete]
    private async Task MonitorContainerStatsAsync(
        string containerId,
        List<ContainerStatsData> statsHistory,
        CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var stream = await Client.Containers.GetContainerStatsAsync(
                        containerId,
                        new ContainerStatsParameters { Stream = false },
                        cancellationToken))
                    {
                        using var reader = new StreamReader(stream);
                        string jsonString = await reader.ReadToEndAsync(cancellationToken);

                        Console.Write($"[Debug] {jsonString}");

                        var stats = ParseDockerStatsJson(jsonString);
                        if (stats != null)
                        {
                            statsHistory.Add(stats);

                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write($"[{DateTime.Now:HH:mm:ss}] CPU: {stats.CpuPercentage,5:F1}% | " +
                                         $"Mem: {stats.MemoryUsageMB,5:F1}/{stats.MemoryLimitMB,5:F1} MB ({stats.MemoryPercentage:F1}%)      ");
                        }
                    }

                    await Task.Delay(10, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nStats error: {ex.Message}");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nMonitoring failed: {ex.Message}");
        }
    }

    private static ContainerStatsData? ParseDockerStatsJson(string jsonString)
    {
        try
        {
            using JsonDocument doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;

            if (!root.TryGetProperty("cpu_stats", out var cpuStats) ||
                !root.TryGetProperty("precpu_stats", out var preCpuStats) ||
                !root.TryGetProperty("memory_stats", out var memoryStats))
            {
                return null;
            }

            double cpuPercent = 0.0;

            if (cpuStats.TryGetProperty("cpu_usage", out var currentCpuUsage) &&
                currentCpuUsage.TryGetProperty("total_usage", out var currentCpuTotalElement) &&
                cpuStats.TryGetProperty("system_cpu_usage", out var currentSystemCpuElement) &&
                preCpuStats.TryGetProperty("cpu_usage", out var prevCpuUsage) &&
                prevCpuUsage.TryGetProperty("total_usage", out var prevCpuTotalElement) &&
                preCpuStats.TryGetProperty("system_cpu_usage", out var prevSystemCpuElement))
            {
                var currentCpuTotal = currentCpuTotalElement.GetUInt64();
                var currentSystemCpu = currentSystemCpuElement.GetUInt64();
                var prevCpuTotal = prevCpuTotalElement.GetUInt64();
                var prevSystemCpu = prevSystemCpuElement.GetUInt64();

                var cpuDelta = currentCpuTotal - prevCpuTotal;
                var systemDelta = currentSystemCpu - prevSystemCpu;

                int onlineCpus = 1;
                if (cpuStats.TryGetProperty("online_cpus", out var onlineCpusElement))
                {
                    onlineCpus = onlineCpusElement.GetInt32();
                }

                if (systemDelta > 0 && cpuDelta > 0)
                {
                    cpuPercent = cpuDelta / (double)systemDelta * onlineCpus * 100.0;
                }
            }

            double memoryUsageMB = 0;
            double memoryLimitMB = 0;
            double memoryPercent = 0;

            if (memoryStats.TryGetProperty("usage", out var memoryUsageElement) &&
                memoryStats.TryGetProperty("limit", out var memoryLimitElement))
            {
                var memoryUsage = memoryUsageElement.GetUInt64();
                var memoryLimit = memoryLimitElement.GetUInt64();

                memoryUsageMB = memoryUsage / 1024.0 / 1024.0;
                memoryLimitMB = memoryLimit / 1024.0 / 1024.0;
                memoryPercent = memoryLimit > 0 ? memoryUsage / (double)memoryLimit * 100.0 : 0;
            }

            return new ContainerStatsData
            {
                CpuPercentage = Math.Round(cpuPercent, 1),
                MemoryUsageMB = Math.Round(memoryUsageMB, 1),
                MemoryLimitMB = Math.Round(memoryLimitMB, 1),
                MemoryPercentage = Math.Round(memoryPercent, 1),
                Timestamp = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFailed to parse stats JSON: {ex.Message}");
            return null;
        }
    }

    private static void DisplayStatsSummary(List<ContainerStatsData> statsHistory)
    {
        if (statsHistory == null || statsHistory.Count == 0)
        {
            Console.WriteLine("No statistics collected.");
            return;
        }

        Console.WriteLine("\n=== Compilation Statistics ===");
        Console.WriteLine($"Samples collected: {statsHistory.Count}");

        var validStats = statsHistory.Where(s => s != null).ToList();
        if (validStats.Count > 0)
        {
            var cpuValues = validStats.Select(s => s.CpuPercentage).ToList();
            var memoryValues = validStats.Select(s => s.MemoryUsageMB).ToList();

            Console.WriteLine($"CPU Usage - Max: {cpuValues.Max():F1}%, " +
                            $"Avg: {cpuValues.Average():F1}%, " +
                            $"Min: {cpuValues.Min():F1}%");

            Console.WriteLine($"Memory Usage - Peak: {memoryValues.Max():F1} MB, " +
                            $"Avg: {memoryValues.Average():F1} MB");

            DisplaySimpleCpuChart(cpuValues);
        }
    }

    private static void DisplaySimpleCpuChart(List<double> cpuValues)
    {
        if (cpuValues.Count < 2) return;

        Console.WriteLine("\nCPU Usage Chart:");
        Console.WriteLine("[" + new string('-', 50) + "]");

        var maxCpu = cpuValues.Max();

        for (int i = 0; i < Math.Min(10, cpuValues.Count); i++)
        {
            var step = cpuValues.Count / 10;
            var index = i * step;
            if (index >= cpuValues.Count) break;

            var cpu = cpuValues[index];
            var barLength = (int)(cpu / maxCpu * 50);
            Console.WriteLine($"{cpu,5:F1}% | {new string('█', barLength)}");
        }

        Console.WriteLine("[" + new string('-', 50) + "]");
    }

    public async Task<string> RunCompiledFileAsync(string fileName, string[] arguments)
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var compiledFilePath = Path.Combine(TempPath, nameWithoutExtension);

        if (!File.Exists(compiledFilePath))
        {
            throw new FileNotFoundException($"Compiled file not found: {compiledFilePath}");
        }

        var config = new CreateContainerParameters
        {
            Image = "gcc:15.2.0",
            Cmd = BuildRunCommand(nameWithoutExtension, arguments),
            WorkingDir = "/src",
            HostConfig = new HostConfig
            {
                Binds =
                [
                    $"{TempPath}:/src:rw"
                ]
            },
            AttachStdout = true,
            AttachStderr = true
        };

        var createResponse = await Client.Containers.CreateContainerAsync(config);

        try
        {
            await Client.Containers.StartContainerAsync(createResponse.ID, new ContainerStartParameters());

            var waitResult = await Client.Containers.WaitContainerAsync(createResponse.ID);

            var output = await GetContainerOutputAsync(createResponse.ID);

            if (waitResult.StatusCode != 0)
            {
                output += $"\n[Program exited with code: {waitResult.StatusCode}]";
            }

            return output;
        }
        finally
        {
            await CleanupContainer(createResponse.ID);
        }
    }

    private static List<string> BuildRunCommand(string binaryName, string[] arguments)
    {
        var command = new List<string> { $"./{binaryName}" };

        if (arguments != null && arguments.Length > 0)
        {
            command.AddRange(arguments);
        }

        return command;
    }

    private async Task<string> GetContainerOutputAsync(string containerId)
    {
        using var logs = await Client.Containers.GetContainerLogsAsync(
            containerId,
            false,
            new ContainerLogsParameters
            {
                ShowStdout = true,
                ShowStderr = true,
                Follow = false
            },
            CancellationToken.None);
        var (stdout, stderr) = await logs.ReadOutputToEndAsync(CancellationToken.None);

        var output = new StringBuilder();
        if (!string.IsNullOrEmpty(stdout))
        {
            output.Append(stdout);
        }

        if (!string.IsNullOrEmpty(stderr))
        {
            if (output.Length > 0) output.AppendLine();
            output.Append($"[STDERR] {stderr}");
        }

        return output.ToString();
    }

    private async Task CleanupContainer(string containerId)
    {
        if (!string.IsNullOrEmpty(containerId))
        {
            try
            {
                await Client.Containers.StopContainerAsync(containerId,
                    new ContainerStopParameters { WaitBeforeKillSeconds = 1 });
                await Client.Containers.RemoveContainerAsync(containerId,
                    new ContainerRemoveParameters { Force = true });
            }
            catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning during cleanup: {ex.Message}");
            }
        }
    }

    public async Task<bool> DownloadCompilerAsync(Compiler compiler, CancellationToken cancellationToken)
    {
        try
        {
            var imageName = compiler.ImageName;

            var images = await Client.Images.ListImagesAsync(new ImagesListParameters
            {
                All = true
            }, cancellationToken);

            var existsLocally = images.Any(img =>
                img.RepoTags != null &&
                img.RepoTags.Any(tag => tag == imageName || tag == $"{imageName}:latest"));

            if (existsLocally)
            {
                return true;
            }

            var createParameters = new ImagesCreateParameters
            {
                FromImage = compiler.Name,
                Tag = compiler.Version
            };

            var progress = new Progress<JSONMessage>(message =>
            {
                if (!string.IsNullOrEmpty(message.ErrorMessage))
                {
                    Console.WriteLine($"Docker Error: {message.ErrorMessage}");
                }
            });

            await Client.Images.CreateImageAsync(
                createParameters,
                null,
                progress,
                cancellationToken
            );

            return true;
        }
        catch (DockerApiException ex)
        {
            throw new InvalidOperationException($"Failed to download compiler image: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to download compiler image: {ex.Message}", ex);
        }
    }

    private async Task Cleanup(string containerId)
    {
        try
        {
            if (!string.IsNullOrEmpty(containerId))
            {
                await Client.Containers.StopContainerAsync(containerId,
                    new ContainerStopParameters { WaitBeforeKillSeconds = 1 });
                await Client.Containers.RemoveContainerAsync(containerId,
                    new ContainerRemoveParameters { Force = true });
            }
        }
        catch { /* Потребуется возможное логирование */ }
    }

    #endregion
}