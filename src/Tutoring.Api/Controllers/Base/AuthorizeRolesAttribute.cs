using Microsoft.AspNetCore.Authorization;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers.Base;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles);
    }
}