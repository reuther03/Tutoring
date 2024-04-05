using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.LoginCommand;
using Tutoring.Application.Features.Users.SignUp;
using Tutoring.Application.Features.Users.Tutors;

namespace Tutoring.Api.Controllers;

public class UserController : BaseController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
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

    [HttpPost("tutor/competence/{competenceId:guid}/")]
    [Authorize]
    public async Task<IActionResult> AddCompetenceToTutor(AddCompetenceCommand command, Guid competenceId, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command with { CompetenceId = competenceId }, cancellationToken);
        return HandleResult(result);
    }
}