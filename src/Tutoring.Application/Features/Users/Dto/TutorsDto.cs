using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class TutorsDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<CompetenceId> Competences { get; init; } = null!;
    public double AverageRating { get; init; }

    public static TutorsDto AsDto(Tutor tutor)
    {
        return new TutorsDto
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