using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

/// <summary>
/// Команда удаления компилятора.
/// </summary>
public sealed class CompilerToDeleteCommand(int id, bool deleteDocker) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; } = id;

    /// <summary>
    /// Удалить ли докер.
    /// </summary>
    public bool DeleteDocker { get; set; } = deleteDocker;
}
