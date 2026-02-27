using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.GetContent;

/// <summary>
/// Запрос на получение содержимого теста.
/// </summary>
public sealed class GetTestContentByIdQuery(Guid id): IRequest<GetTestContentByIdResponse>
{
    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IFileProvider fileProvider)
    {
        public async Task<GetTestContentByIdResponse> Handle(GetTestContentByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await dbContext.Tests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (test == null)
            {
                throw new ApiException("Теста с данным идентификатором нет в базе.");
            }

            return new GetTestContentByIdResponse
            {
                Id = test.Id,
                Name = test.Name,
                Content = await fileProvider.GetContent(test.Path, cancellationToken),
            };
        }
    }
}
