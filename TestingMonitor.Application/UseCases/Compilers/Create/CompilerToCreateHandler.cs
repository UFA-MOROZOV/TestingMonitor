using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Compilers.Create;

/// <summary>
/// Обработчик загрузки докера компилятора.
/// </summary>
internal sealed class CompilerToCreateHandler(IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilerToCreateCommand, int>
{
    /// <inheritdoc/>
    public async Task<int> Handle(CompilerToCreateCommand command, CancellationToken cancellationToken)
    {
        if (await dbContext.Compilers
            .AnyAsync(x => x.Name == command.Name && x.Version == command.Version, cancellationToken))
        {
            throw new ApiException("Компилятор с такими данными уже существует.");
        }

        var compiler = new Compiler
        {
            Name = command.Name,
            Version = command.Version,
            CommandName = command.CommandName,
            ImageName = command.ImageName,
            HasDockerLocally = await dockerManager.ImageExistsAsync(command.ImageName, cancellationToken)
        };


        if (!compiler.HasDockerLocally && command.Tar != Stream.Null)
        {
            var result = await dockerManager.LoadDockerImageAsync(command.Tar, cancellationToken);

            if (result)
            {
                compiler.HasDockerLocally = true;
            }
        }

        await dbContext.Compilers.AddAsync(compiler, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return compiler.Id;
    }
}
