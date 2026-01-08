using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Interfaces;

public interface IDockerExecutor
{
    public Task<string> ExecuteCodeAsync(Compiler compiler, string code, CancellationToken cancellationToken);

    public Task<bool> DownloadCompilerAsync(Compiler compiler, CancellationToken cancellationToken);
}
