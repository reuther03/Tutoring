using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Query;

public record GetFilteredTutorsQuery : IQuery<PaginatedList<TutorDetailsDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<Guid> CompetenceIds { get; init; } = [];

    // [AllowedValues([1, 2, 3, 4, 5])]
    public int? AverageRating { get; set; }

    public string SearchValue { get; set; } = string.Empty;

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
                .WhereIf(request.CompetenceIds.Count != 0, x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y.Value)));

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                query = query.Where(x => EF.Functions.Like(x.FirstName, $"%{request.SearchValue}%") ||
                                         EF.Functions.Like(x.LastName, $"%{request.SearchValue}%"));
            }

            // NOTE: to samo co WhereIf ⬆️
            // if (request.CompetenceIds.Count != 0)
            // {
            //     query = query.Where(x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y)));
            // }

            var tutors = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            /*
             * .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);
             */

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);
            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }
    }
}