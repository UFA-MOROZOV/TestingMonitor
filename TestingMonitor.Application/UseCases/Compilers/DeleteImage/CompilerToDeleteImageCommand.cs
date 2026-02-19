using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.DeleteImage;

/// <summary>
/// Команда удаления образа компилятора.
/// </summary>
public sealed class CompilerToDeleteImageCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; } = id;
}
