using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class TutorDetailsDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<CompetenceId> Competences { get; init; } = null!;
    public List<ReviewDto> Reviews { get; init; } = null!;

    public static TutorDetailsDto AsDto(Tutor tutor)
    {
        return new TutorDetailsDto
        {
            Email = tutor.Email,
            FirstName = tutor.FirstName,
            LastName = tutor.LastName,
            Role = tutor.Role.ToString(),
            Competences = tutor.CompetenceIds.ToList(),
            Reviews = tutor.Reviews.Select(ReviewDto.AsDto).ToList()
        };
    }
}