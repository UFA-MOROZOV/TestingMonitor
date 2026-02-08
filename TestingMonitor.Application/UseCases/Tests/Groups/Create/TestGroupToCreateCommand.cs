using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Create;

/// <summary>
/// Команда создания группы тестов.
/// </summary>
public sealed class TestGroupToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Группа в которой будет находится эта группа.
    /// </summary>
    public Guid? ParentGroupId { get; set; }
}
