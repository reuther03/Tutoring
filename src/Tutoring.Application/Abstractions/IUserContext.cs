using System.Diagnostics.CodeAnalysis;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Abstractions;

public interface IUserContext
{
    [MemberNotNullWhen(true, nameof(UserId), nameof(Email), nameof(Role))]
    public bool IsAuthenticated { get; }

    public UserId? UserId { get; }
    public Email? Email { get; }
    public Role? Role { get; }

    [MemberNotNull(nameof(UserId), nameof(Email), nameof(Role))]
    public void EnsureAuthenticated();
}