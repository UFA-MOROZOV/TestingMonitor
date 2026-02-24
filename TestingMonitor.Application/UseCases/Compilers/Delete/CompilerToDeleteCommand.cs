using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

/// <summary>
/// Команда удаления компилятора.
/// </summary>
public sealed class CompilerToDeleteCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; } = id;
}
