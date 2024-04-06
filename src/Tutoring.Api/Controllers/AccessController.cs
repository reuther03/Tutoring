using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.LoginCommand;
using Tutoring.Application.Features.Users.SignUp;

namespace Tutoring.Api.Controllers;

public class AccessController : BaseController
{
    private readonly ISender _sender;

    public AccessController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return HandleResult(result);
    }
}