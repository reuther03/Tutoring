using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Queries.Tutors;

public record GetTutorsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TutorsDto>>
{
    internal sealed class Handler : IQueryHandler<GetTutorsQuery, PaginatedList<TutorsDto>>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<TutorsDto>>> Handle(GetTutorsQuery request, CancellationToken cancellationToken)
        {
            var tutors = _dbContext.Users.OfType<Tutor>()
                .Include(x => x.Reviews)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize).AsEnumerable()
                .Select(TutorsDto.AsDto)
                .ToList();

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);

            return PaginatedList<TutorsDto>.Create(request.Page, request.PageSize, totalTutors, tutors);
        }
    }
}