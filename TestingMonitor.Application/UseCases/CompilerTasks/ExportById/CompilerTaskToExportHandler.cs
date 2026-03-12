using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.CompilerTasks.GetById;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.CompilerTasks.ExportById;

internal class CompileTaskToExportHandler(IDbContext dbContext, IMapper mapper) : IRequestHandler<CompilerTaskToExportCommand, byte[]>
{
    public async Task<byte[]> Handle(CompilerTaskToExportCommand request, CancellationToken cancellationToken)
    {
        var compilerTask = await dbContext.CompilerTasks
            .ProjectTo<CompilerTaskByIdDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (compilerTask == null)
        {
            ErrorCode.CompilerTaskNotFound.Throw();
        }

        return ExportTestExecutions(compilerTask!.TestsExecuted.ToList());
    }

    public static byte[] ExportTestExecutions(List<TestExecutionDto> executions)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Test Executions");

        worksheet.Cell(1, 1).Value = "Test ID";
        worksheet.Cell(1, 2).Value = "Test Name";
        worksheet.Cell(1, 3).Value = "Duration (s)";
        worksheet.Cell(1, 4).Value = "Compile Duration (s)";
        worksheet.Cell(1, 5).Value = "Run Duration (s)";
        worksheet.Cell(1, 6).Value = "Compiler Output";
        worksheet.Cell(1, 7).Value = "Program Output";
        worksheet.Cell(1, 8).Value = "Compiler Exit Code";
        worksheet.Cell(1, 9).Value = "Program Exit Code";
        worksheet.Cell(1, 10).Value = "Compilation Succeeded";

        for (int i = 0; i < executions.Count; i++)
        {
            var exec = executions[i];
            int row = i + 2;

            worksheet.Cell(row, 1).Value = exec.Test?.Id.ToString();
            worksheet.Cell(row, 2).Value = exec.Test?.Name ?? "";
            worksheet.Cell(row, 3).Value = exec.Duration.TotalSeconds;
            worksheet.Cell(row, 4).Value = exec.CompileDuration?.TotalSeconds;
            worksheet.Cell(row, 5).Value = exec.RunDuration?.TotalSeconds;
            worksheet.Cell(row, 6).Value = exec.CompilerOutput;
            worksheet.Cell(row, 7).Value = exec.ProgramOutput;
            worksheet.Cell(row, 8).Value = exec.CompilerExitCode;
            worksheet.Cell(row, 9).Value = exec.ProgramExitCode;
            worksheet.Cell(row, 10).Value = exec.CompilationSucceeded.ToString();

            worksheet.Cell(row, 3).Style.NumberFormat.Format = "0.00";
            worksheet.Cell(row, 4).Style.NumberFormat.Format = "0.00";
            worksheet.Cell(row, 5).Style.NumberFormat.Format = "0.00";
        }

        worksheet.Columns().AdjustToContents();

        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
