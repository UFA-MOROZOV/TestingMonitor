using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteCode;

/// <summary>
/// Команда выполнения кода компилятором.
/// </summary>
public sealed class CompilerToExecuteCodeCommand : IRequest<string>
{
    /// <summary>
    /// Идентификатор компилятора.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Код.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Code { get; set; } = null!;
}
