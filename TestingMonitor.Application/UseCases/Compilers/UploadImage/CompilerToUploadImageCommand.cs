using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.UploadImage;

/// <summary>
/// Команда загрузки образа компилятора.
/// </summary>
public sealed class CompilertToUploadImageCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Файл.
    /// </summary>
    public Stream Stream { get; set; } = null!;
}