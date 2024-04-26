using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Query;

public record GetFilteredTutorsQuery : IQuery<PaginatedList<TutorDetailsDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<Guid> CompetenceIds { get; init; } = [];

    internal sealed class Handler : IQueryHandler<GetFilteredTutorsQuery, PaginatedList<TutorDetailsDto>>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<TutorDetailsDto>>> Handle(GetFilteredTutorsQuery request, CancellationToken cancellationToken)
        {
            var tutors = await _dbContext.Users.OfType<Tutor>()
                .Include(x => x.CompetenceIds)
                .Where(x => x.CompetenceIds.Any())
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .AsQueryable()
                .ToListAsync(cancellationToken);

            if (request.CompetenceIds.Count != 0)
            {
                tutors = tutors.Where(x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y))).ToList();
            }

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);
            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }
    }
}