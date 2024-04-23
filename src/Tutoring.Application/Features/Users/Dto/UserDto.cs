using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class UserDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<ReviewDto> Reviews { get; init; } = [];
    public double AverageRating { get; init; }

    public static UserDto AsDto(User user)
    {
        return new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role.ToString(),
            AverageRating = user.AverageRating
        };
    }
}