using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Delete;

/// <summary>
/// Команда удаления группы тестов.
/// </summary>
public sealed class TestGroupToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; } = id;
}
