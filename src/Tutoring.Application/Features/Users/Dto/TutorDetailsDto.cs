using Tutoring.Application.Features.Matching.Payloads;
using Tutoring.Domain.Availabilities;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class TutorDetailsDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<CompetenceId> Competences { get; init; } = null!;
    public List<ReviewDto> Reviews { get; init; } = null!;
    public List<AvailabilityPayload> Availabilities { get; init; } = null!;

    public static TutorDetailsDto AsDto(Tutor tutor)
    {
        return new TutorDetailsDto
        {
            Id = tutor.Id,
            Email = tutor.Email,
            FirstName = tutor.FirstName,
            LastName = tutor.LastName,
            Role = tutor.Role.ToString(),
            Competences = tutor.CompetenceIds.ToList(),
            Reviews = tutor.Reviews.Select(ReviewDto.AsDto).ToList(),
            Availabilities = tutor.Availabilities.Select(AvailabilityPayload.AsDto).ToList()
        };
    }
}