using Tutoring.Domain.Competences;
using Tutoring.Domain.Reviews;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.Users.Dto;

public class TutorDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<CompetenceId> Competences { get; init; } = null!;
    public double AverageRating { get; init; }

    public static TutorDto AsDto(Tutor tutor)
    {
        return new TutorDto
        {
            Email = tutor.Email,
            FirstName = tutor.FirstName,
            LastName = tutor.LastName,
            Role = tutor.Role.ToString(),
            Competences = tutor.CompetenceIds.ToList(),
            AverageRating = tutor.AverageRating
        };
    }
}