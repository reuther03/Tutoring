using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.TutorCommands;
using Tutoring.Application.Features.Users.Queries.Tutors;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class TutorsController : BaseController
{
    private readonly ISender _sender;

    public TutorsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AuthorizeAllRoles]
    public async Task<IActionResult> GetTutors([FromQuery] GetTutorDetailsQuery detailsQuery,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(detailsQuery, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{userId:guid}")]
    [AuthorizeAllRoles]
    public async Task<IActionResult> GetTutor([FromRoute] Guid userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new GetTutorDetailsQuery(userId), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("competences/{competenceId:guid}")]
    [AuthorizeRoles(Role.Tutor)]
    public async Task<IActionResult> AddCompetenceToTutor([FromRoute] Guid competenceId,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new AddTutorCompetenceCommand(competenceId), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("availabilities")]
    [AuthorizeRoles(Role.Tutor)]
    public async Task<IActionResult> AddAvailabilityToTutor([FromBody] AddTutorAvailabilityCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return HandleResult(result);
    }
}