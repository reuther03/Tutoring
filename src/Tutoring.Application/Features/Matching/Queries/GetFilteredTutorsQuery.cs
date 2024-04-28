using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Queries;

public record GetFilteredTutorsQuery : IQuery<PaginatedList<TutorDetailsDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<Guid> CompetenceIds { get; init; } = [];

    // [AllowedValues([1, 2, 3, 4, 5])]
    public int? AverageRating { get; init; } = null;

    public List<Day> Days { get; init; } = [];

    public string? SearchValue { get; set; } = string.Empty;

    internal sealed class Handler : IQueryHandler<GetFilteredTutorsQuery, PaginatedList<TutorDetailsDto>>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<TutorDetailsDto>>> Handle(GetFilteredTutorsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Users.OfType<Tutor>()
                .Where(x => x.CompetenceIds.Any())
                .WhereIf(request.AverageRating.HasValue, x => x.AverageRating >= request.AverageRating)
                .WhereIf(request.CompetenceIds.Count != 0, x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y.Value)))
                .WhereIf(request.Days.Count != 0, x => x.Availabilities.Any(y => request.Days.Contains(y.Day)));

            // NOTE: to samo co WhereIf ⬆️
            // if (request.CompetenceIds.Count != 0)
            // {
            //     query = query.Where(x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y)));
            // }

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x => EF.Functions.Like(x.FirstName, $"%{request.SearchValue}%") ||
                                         EF.Functions.Like(x.LastName, $"%{request.SearchValue}%"));
            }


            var tutors = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);
            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }
    }
}