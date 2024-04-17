using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.TutorCommands.AddTutorCompetence;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class TutorsController : BaseController
{
    private readonly ISender _sender;

    public TutorsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("competences/{competenceId:guid}")]
    [AuthorizeRoles(Role.Tutor)]
    public async Task<IActionResult> AddCompetenceToTutor([FromRoute] Guid competenceId,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new AddTutorCompetenceCommand(competenceId), cancellationToken);
        return HandleResult(result);
    }
}