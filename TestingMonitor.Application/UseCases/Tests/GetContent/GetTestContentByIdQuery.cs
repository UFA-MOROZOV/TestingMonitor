using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.GetContent;

/// <summary>
/// Query of getting a test content.
/// </summary>
public sealed class GetTestContentByIdQuery(Guid id) : IRequest<GetTestContentByIdResponse>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<GetTestContentByIdQuery, GetTestContentByIdResponse>
    {
        public async Task<GetTestContentByIdResponse> Handle(GetTestContentByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await dbContext.Tests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (test == null)
            {
                ErrorCode.TestNotFound.Throw();
            }

            return new GetTestContentByIdResponse
            {
                Id = test!.Id,
                Name = test.Name,
                Content = await fileProvider.GetContent(test.Path, cancellationToken),
                TestGroupId = test.TestGroupId
            };
        }
    }
}
