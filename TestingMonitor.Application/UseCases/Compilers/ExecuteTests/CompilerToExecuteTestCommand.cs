using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteTests;

/// <summary>
/// Команда тестирования компилятора.
/// </summary>
public sealed class CompilerToExecuteTestCommand : IRequest<Guid>
{
    /// <summary>
    /// Имя для задания.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Идентификатор компилятора.
    /// </summary>
    public int CompilerId { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid? TestId { get; set; }

    /// <summary>
    /// Идентификатор группы теста.
    /// </summary>
    public Guid? TestGroupId { get; set; }
}
