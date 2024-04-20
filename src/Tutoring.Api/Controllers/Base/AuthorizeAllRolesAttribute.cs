using Microsoft.AspNetCore.Authorization;

namespace Tutoring.Api.Controllers.Base;

public class AuthorizeAllRolesAttribute : AuthorizeAttribute
{
    public AuthorizeAllRolesAttribute()
    {
        Roles = "Admin,Tutor,Student";
    }
}