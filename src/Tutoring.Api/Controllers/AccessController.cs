using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands;
using Tutoring.Application.Features.Users.Commands.Access;
using Tutoring.Application.Features.Users.Queries.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class AccessController : BaseController
{
    private readonly ISender _sender;

    public AccessController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Gets the current user
    /// </summary>
    /// <remarks>
    /// Example request:
    ///
    /// GET /access/current-user
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns>The current user</returns>
    [HttpGet("current-user")]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetCurrentUserQuery(), cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Signs up a new user
    /// </summary>
    /// <remarks>
    /// Example request: <br/>
    /// POST /access/sign-up <br/>
    ///
    /// { <br/>
    ///  "email": "Example@gmail.com", <br/>
    ///  "firstName": "Example", <br/>
    ///  "lastName": "Example", <br/>
    ///  "password": "Example", <br/>
    ///  "role": 1 <br/>
    /// } <br/>
    ///
    /// </remarks>
    /// <param name="command">
    /// - `Email` (string): The user's email address. Must be a valid email format.
    /// - `FirstName` (string): The user's first name.
    /// - `LastName` (string): The user's last name.
    /// - `Password` (string): The user's password. Must be at least 6 characters long.
    /// - `Role` (int): The user's role. Must be either 1 or 2.(1 for students and 2 for tutors)
    ///
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns> The id of the new user</returns>
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    ///  Logs in a user
    /// </summary>
    /// <remarks>
    /// Example request: <br/>
    /// POST /access/login <br/>
    ///
    /// { <br/>
    ///  "email": "Example@gmail.com", <br/>
    ///  "password": "Example" <br/>
    /// } <br/>
    ///
    /// </remarks>
    /// <param name="loginCommand">
    /// - `Email` (string): The user's email address. Must be a valid email format.
    /// - `Password` (string): The user's password. Must be at least 6 characters long.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns> </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(loginCommand, cancellationToken);
        return HandleResult(result);
    }

    /// <summary>
    /// Archives the current user
    /// </summary>
    /// <remarks>
    /// Example request: <br/>
    /// DELETE /access/archive-user
    /// </remarks>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> ArchiveUser()
    {
        var result = await _sender.Send(new ArchiveUserCommand());
        return HandleResult(result);
    }
}