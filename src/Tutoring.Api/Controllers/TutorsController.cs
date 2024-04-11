using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.TutorCommands.AddTutorCompetence;

namespace Tutoring.Api.Controllers;

public class TutorsController : BaseController
{
    private readonly ISender _sender;

    public TutorsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("competences/{competenceId:guid}")]
    [Authorize]
    public async Task<IActionResult> AddCompetenceToTutor([FromRoute] Guid competenceId, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new AddTutorCompetenceCommand(competenceId), cancellationToken);
        return HandleResult(result);
    }
}