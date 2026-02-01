using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.DeleteTest;

/// <summary>
/// Команда удаления теста.
/// </summary>
internal class TestToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; } = id;
}
