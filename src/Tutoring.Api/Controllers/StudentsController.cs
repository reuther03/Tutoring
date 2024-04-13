using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.StudentCommands;
using Tutoring.Application.Features.Users.Queries;

namespace Tutoring.Api.Controllers;

public class StudentsController : BaseController
{
    private readonly ISender _sender;

    public StudentsController(ISender sender)
    {
        _sender = sender;
    }


    [HttpGet("Subjects/{subjectId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetSubject([FromRoute] Guid subjectId)
    {
        var result = await _sender.Send(new GetSubjectQuery(SubjectId: subjectId));
        return HandleResult(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSubject([FromBody] AddSubjectCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPost("Subjects/{subjectId:guid}/Competences/{competenceId:guid}")]
    [Authorize]
    public async Task<IActionResult> AddSubjectsCompetence(
        [FromRoute] Guid subjectId,
        [FromRoute] Guid competenceId)
    {
        var result = await _sender.Send(new AddSubjectsCompetenceCommand(
            SubjectId: subjectId,
            CompetenceId: competenceId));
        return HandleResult(result);
    }
}