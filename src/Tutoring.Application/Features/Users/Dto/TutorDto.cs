using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class TutorDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public double AverageRating { get; init; }

    public static TutorDto AsDto(Tutor tutor)
    {
        return new TutorDto
        {
            Email = tutor.Email,
            FirstName = tutor.FirstName,
            LastName = tutor.LastName,
            Role = tutor.Role.ToString(),
            AverageRating = tutor.AverageRating
        };
    }
}