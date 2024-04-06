using Tutoring.Domain.Users;

namespace Tutoring.Application.Abstractions;

public sealed class AccessToken
{
    public string Token { get; init; } = null!;
    public Guid UserId { get; init; }
    public Enum Role { get; init; } = null!;

    public static AccessToken Create(User user, string token)
    {
        return new AccessToken
        {
            Token = token,
            UserId = user.Id,
            Role = user.Role
        };
    }
}