using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tutoring.Application.Abstractions;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;
using Tutoring.Infrastructure.Authentication;

namespace Tutoring.Infrastructure.Auth;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    [MemberNotNullWhen(true, nameof(UserId), nameof(Email), nameof(Role))]
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public UserId? UserId => IsAuthenticated ? GetUserIdFromClaims(_httpContextAccessor.HttpContext?.User) : null;
    public Email? Email => IsAuthenticated ? GetEmailFromClaims(_httpContextAccessor.HttpContext?.User) : null;
    public Role? Role => IsAuthenticated ? GetRoleFromClaims(_httpContextAccessor.HttpContext?.User) : null;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private static UserId? GetUserIdFromClaims(ClaimsPrincipal? claims)
    {
        if (claims is null)
            return null;

        var userId = claims.FindFirst(ClaimConsts.UserId)?.Value;
        return userId is null ? null : UserId.From(userId);
    }

    private static Email? GetEmailFromClaims(ClaimsPrincipal? claims)
    {
        if (claims is null)
            return null;

        var email = claims.FindFirst(ClaimConsts.Email)?.Value;
        return email is null ? null : new Email(email);
    }

    // private static Role? GetRoleFromClaims(ClaimsPrincipal? claims)
    // {
    //     if (claims is null)
    //         return null;
    //
    //     var role = claims.FindFirst(ClaimConsts.Role)?.Value;
    //     return role is null ? null : new Role();
    // }
    private static Role? GetRoleFromClaims(ClaimsPrincipal? claims)
    {
        if (claims is null)
            return null;

        var roleClaim = claims.FindFirst(ClaimTypes.Role)?.Value;

        if (Enum.TryParse<Role>(roleClaim, out var role))
        {
            return role;
        }

        return null; // or handle the case where the role is not a valid enum value
    }
}