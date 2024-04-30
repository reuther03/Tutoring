using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Matching.Payloads;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Availabilities;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Queries;

public record GetFilteredTutorsQuery : IQuery<PaginatedList<TutorDetailsDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<Guid?> CompetenceIds { get; init; } = [];
    public int? AverageRating { get; init; } = null;
    public List<AvailabilityPayload> Availability { get; init; } = [];
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
            var prefilteredTutors = await _dbContext.Users.OfType<Tutor>()
                .Include(x => x.Availabilities)
                .Where(x => x.CompetenceIds.Any())
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.SearchValue),
                    x => EF.Functions.Like(x.FirstName, $"%{request.SearchValue}%") ||
                        EF.Functions.Like(x.LastName, $"%{request.SearchValue}%"))
                .WhereIf(request.AverageRating.HasValue, x => x.AverageRating >= request.AverageRating)
                .WhereIf(request.CompetenceIds.Count != 0, x => x.CompetenceIds.Any(y => request.CompetenceIds.Contains(y.Value)))
                .Select(x => new { x.Id, x.Availabilities })
                .ToListAsync(cancellationToken);

            var tutorIds = prefilteredTutors
                .Where(tutor => tutor.Availabilities.Any(tutorAvailability =>
                    request.Availability.Any(reqAvailability => CheckAvailabilityMatch(reqAvailability, tutorAvailability))))
                .Select(x => x.Id);

            var tutors = await _dbContext.Users.OfType<Tutor>()
                .Where(x => tutorIds.Any(y => y == x.Id))
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);
            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }

        /// <summary>
        /// Check if the availability of the tutor matches the requested availability
        /// </summary>
        /// <param name="reqAvailability">The requested availability</param>
        /// <param name="tutorAvailability">The tutor's availability</param>
        /// <returns>True if the tutor's availability matches the requested availability, false otherwise</returns>
        private static bool CheckAvailabilityMatch(AvailabilityPayload reqAvailability, Availability tutorAvailability)
        {
            var hasDayValue = reqAvailability.Day == null;
            var hasFromValue = reqAvailability.From == null;
            var hasToValue = reqAvailability.To == null;

            var hasMatch = (hasDayValue, hasFromValue, hasToValue) switch
            {
                (true, true, true) => true,
                (true, true, false) => reqAvailability.To >= tutorAvailability.From,
                (true, false, true) => reqAvailability.From <= tutorAvailability.To,
                (true, false, false) => reqAvailability.From <= tutorAvailability.To && reqAvailability.To >= tutorAvailability.From,
                (false, true, true) => reqAvailability.Day == tutorAvailability.Day,
                (false, true, false) => reqAvailability.Day == tutorAvailability.Day && reqAvailability.To >= tutorAvailability.From,
                (false, false, true) => reqAvailability.Day == tutorAvailability.Day && reqAvailability.From <= tutorAvailability.To,
                (false, false, false) => reqAvailability.Day == tutorAvailability.Day && reqAvailability.From <= tutorAvailability.To &&
                    reqAvailability.To >= tutorAvailability.From
            };

            return hasMatch;
        }
    }
}