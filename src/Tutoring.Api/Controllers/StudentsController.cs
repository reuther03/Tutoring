using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.StudentCommands;
using Tutoring.Application.Features.Users.Queries.Subjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class StudentsController : BaseController
{
    private readonly ISender _sender;

    public StudentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("Subjects")]
    [AuthorizeRoles(Role.Student)]
    public async Task<IActionResult> GetSubjects([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _sender.Send(new GetUserSubjectsQuery(Page: page, PageSize: pageSize));
        return HandleResult(result);
    }

    [HttpGet("Subjects/{subjectId:guid}")]
    [AuthorizeRoles(Role.Student)]
    public async Task<IActionResult> GetSubject([FromRoute] Guid subjectId)
    {
        var result = await _sender.Send(new GetUserSubjectQuery(SubjectId: subjectId));
        return HandleResult(result);
    }

    [HttpPost]
    [AuthorizeRoles(Role.Student)]
    public async Task<IActionResult> AddSubject([FromBody] AddSubjectCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }

    [HttpPost("Subjects/{subjectId:guid}/Competences/{competenceId:guid}")]
    [AuthorizeRoles(Role.Student)]
    public async Task<IActionResult> AddSubjectsCompetence(
        [FromRoute] Guid subjectId,
        [FromRoute] Guid competenceId)
    {
        var result = await _sender.Send(new AddSubjectsCompetenceCommand(
            SubjectId: subjectId,
            CompetenceId: competenceId));
        return HandleResult(result);
    }

    [HttpDelete("Subjects/{subjectId:guid}")]
    [AuthorizeRoles(Role.Student)]
    public async Task<IActionResult> DeleteSubject([FromRoute] Guid subjectId)
    {
        var result = await _sender.Send(new DeleteSubjectCommand(SubjectId: subjectId));
        return HandleResult(result);
    }
}