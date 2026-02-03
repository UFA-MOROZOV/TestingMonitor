using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Helpers;

/// <summary>
/// Вспомогательный класс для обработки ZIP архивов с тестами
/// </summary>
public sealed class ZipTestArchiveProcessor
{
    private readonly IDbContext _dbContext;
    private readonly IFileProvider _fileProvider;
    private readonly Dictionary<string, TestGroup> _createdGroups = new();
    private readonly HashSet<string> _supportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".c", ".cpp", ".cc", ".h", ".hpp"
    };

    public ZipTestArchiveProcessor(IDbContext dbContext, IFileProvider fileProvider)
    {
        _dbContext = dbContext;
        _fileProvider = fileProvider;
    }

    /// <summary>
    /// Обработать ZIP архив и создать/обновить группы и тесты
    /// </summary>
    public async Task ProcessAsync(
        Stream zipStream,
        Guid? parentGroupId,
        CancellationToken cancellationToken)
    {
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        await ProcessArchiveEntriesAsync(archive, parentGroupId, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Обработать все записи в архиве
    /// </summary>
    private async Task ProcessArchiveEntriesAsync(
        ZipArchive archive,
        Guid? rootParentGroupId,
        CancellationToken cancellationToken)
    {
        var sortedEntries = GetSortedEntries(archive);

        foreach (var entry in sortedEntries)
        {
            await ProcessEntryAsync(entry, rootParentGroupId, cancellationToken);
        }
    }

    /// <summary>
    /// Получить отсортированные записи архива
    /// </summary>
    private static List<ZipArchiveEntry> GetSortedEntries(ZipArchive archive)
    {
        return archive.Entries
            .Where(entry => !string.IsNullOrWhiteSpace(entry.FullName))
            .OrderBy(entry => entry.FullName)
            .ToList();
    }

    /// <summary>
    /// Обработать одну запись в архиве
    /// </summary>
    private async Task ProcessEntryAsync(
        ZipArchiveEntry entry,
        Guid? rootParentGroupId,
        CancellationToken cancellationToken)
    {
        var normalizedPath = NormalizePath(entry.FullName);

        if (IsFolderEntry(entry))
        {
            await EnsureFolderHierarchyExistsAsync(normalizedPath, rootParentGroupId, cancellationToken);
        }
        else if (IsSupportedTestFile(entry))
        {
            await ProcessTestFileAsync(entry, normalizedPath, rootParentGroupId, cancellationToken);
        }
    }

    /// <summary>
    /// Нормализовать путь (заменить обратные слеши)
    /// </summary>
    private static string NormalizePath(string path)
    {
        return path.Replace('\\', '/').TrimEnd('/');
    }

    /// <summary>
    /// Проверить, является ли запись папкой
    /// </summary>
    private static bool IsFolderEntry(ZipArchiveEntry entry)
    {
        return entry.FullName.EndsWith("/") ||
               entry.FullName.EndsWith("\\") ||
               entry.Length == 0 && string.IsNullOrEmpty(Path.GetExtension(entry.FullName));
    }

    /// <summary>
    /// Проверить, поддерживается ли файл как тест
    /// </summary>
    private bool IsSupportedTestFile(ZipArchiveEntry entry)
    {
        var extension = Path.GetExtension(entry.Name);
        return !string.IsNullOrEmpty(extension) &&
               _supportedExtensions.Contains(extension);
    }

    /// <summary>
    /// Убедиться, что иерархия папок существует
    /// </summary>
    private async Task EnsureFolderHierarchyExistsAsync(
        string folderPath,
        Guid? rootParentGroupId,
        CancellationToken cancellationToken)
    {
        var pathParts = folderPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        TestGroup? currentParent = null;
        string accumulatedPath = "";

        for (int i = 0; i < pathParts.Length; i++)
        {
            accumulatedPath = BuildAccumulatedPath(accumulatedPath, pathParts[i]);
            var parentId = i == 0 ? rootParentGroupId : currentParent?.Id;

            currentParent = await GetOrCreateTestGroupAsync(
                pathParts[i],
                parentId,
                accumulatedPath,
                currentParent,
                cancellationToken);
        }
    }

    /// <summary>
    /// Построить накапливаемый путь
    /// </summary>
    private static string BuildAccumulatedPath(string currentPath, string newPart)
    {
        return string.IsNullOrEmpty(currentPath)
            ? newPart
            : $"{currentPath}/{newPart}";
    }

    /// <summary>
    /// Получить или создать группу тестов
    /// </summary>
    private async Task<TestGroup> GetOrCreateTestGroupAsync(
        string groupName,
        Guid? parentId,
        string accumulatedPath,
        TestGroup? currentParent,
        CancellationToken cancellationToken)
    {
        if (_createdGroups.TryGetValue(accumulatedPath, out var existingGroup))
        {
            return existingGroup;
        }

        var group = await FindExistingTestGroupAsync(groupName, parentId, cancellationToken);

        if (group == null)
        {
            group = CreateNewTestGroup(groupName, parentId);
            await AddGroupToDatabaseAndParentAsync(group, currentParent, cancellationToken);
        }

        _createdGroups[accumulatedPath] = group;
        return group;
    }

    /// <summary>
    /// Найти существующую группу тестов
    /// </summary>
    private async Task<TestGroup?> FindExistingTestGroupAsync(
        string groupName,
        Guid? parentId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.TestGroups
            .FirstOrDefaultAsync(g =>
                g.Name == groupName &&
                g.ParentGroupId == parentId,
                cancellationToken);
    }

    /// <summary>
    /// Создать новую группу тестов
    /// </summary>
    private static TestGroup CreateNewTestGroup(string name, Guid? parentGroupId)
    {
        return new TestGroup
        {
            Id = Guid.NewGuid(),
            Name = name,
            ParentGroupId = parentGroupId,
            Tests = new List<Test>(),
            SubGroups = new List<TestGroup>()
        };
    }

    /// <summary>
    /// Добавить группу в БД и родительскую группу
    /// </summary>
    private async Task AddGroupToDatabaseAndParentAsync(
        TestGroup group,
        TestGroup? parentGroup,
        CancellationToken cancellationToken)
    {
        await _dbContext.TestGroups.AddAsync(group, cancellationToken);

        if (parentGroup != null)
        {
            parentGroup.SubGroups.Add(group);
        }
    }

    /// <summary>
    /// Обработать файл теста
    /// </summary>
    private async Task ProcessTestFileAsync(
        ZipArchiveEntry entry,
        string filePath,
        Guid? rootParentGroupId,
        CancellationToken cancellationToken)
    {
        var directoryPath = Path.GetDirectoryName(filePath)?.Replace('\\', '/') ?? string.Empty;
        var fileName = Path.GetFileName(filePath);

        var parentGroup = await GetParentGroupForFileAsync(directoryPath, rootParentGroupId, cancellationToken);

        if (await TestFileAlreadyExistsAsync(fileName, parentGroup, cancellationToken))
        {
            return;
        }

        await CreateTestFromFileAsync(entry, fileName, parentGroup, cancellationToken);
    }

    /// <summary>
    /// Получить родительскую группу для файла
    /// </summary>
    private async Task<TestGroup?> GetParentGroupForFileAsync(
        string directoryPath,
        Guid? rootParentGroupId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(directoryPath))
        {
            return rootParentGroupId.HasValue
                ? await FindTestGroupByIdAsync(rootParentGroupId.Value, cancellationToken)
                : null;
        }

        await EnsureFolderHierarchyExistsAsync(directoryPath, rootParentGroupId, cancellationToken);

        return _createdGroups.TryGetValue(directoryPath, out var parentGroup)
            ? parentGroup
            : null;
    }

    /// <summary>
    /// Найти группу тестов по ID
    /// </summary>
    private async Task<TestGroup?> FindTestGroupByIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return await _dbContext.TestGroups
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);
    }

    /// <summary>
    /// Проверить, существует ли уже файл теста
    /// </summary>
    private async Task<bool> TestFileAlreadyExistsAsync(
        string fileName,
        TestGroup? parentGroup,
        CancellationToken cancellationToken)
    {
        var parentGroupId = parentGroup?.Id;

        return await _dbContext.Tests
            .AnyAsync(t =>
                t.Name == fileName &&
                t.TestGroupId == parentGroupId,
                cancellationToken);
    }

    /// <summary>
    /// Создать тест из файла
    /// </summary>
    private async Task CreateTestFromFileAsync(
        ZipArchiveEntry entry,
        string fileName,
        TestGroup? parentGroup,
        CancellationToken cancellationToken)
    {
        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = fileName,
            TestGroupId = parentGroup?.Id,
            TestGroup = parentGroup
        };

        var savedFilePath = await SaveFileToStorageAsync(entry, test.Id, cancellationToken);

        if (savedFilePath == null)
        {
            return;
        }

        test.Path = savedFilePath;

        await _dbContext.Tests.AddAsync(test, cancellationToken);
    }

    /// <summary>
    /// Сохранить файл в хранилище
    /// </summary>
    private async Task<string?> SaveFileToStorageAsync(
        ZipArchiveEntry entry,
        Guid id,
        CancellationToken cancellationToken)
    {
        using var entryStream = entry.Open();
        using var memoryStream = new MemoryStream();

        return await _fileProvider.UploadFileAsync(
            entryStream,
            id,
            cancellationToken);
    }
}