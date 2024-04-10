using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.CompetencesGroups.Commands;
using Tutoring.Application.Features.CompetencesGroups.Queries;

namespace Tutoring.Api.Controllers;

public class CompetencesGroupController : BaseController
{
    private readonly ISender _sender;

    public CompetencesGroupController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var competencesGroups = await _sender.Send(new GetAllCompetenceGroupsQuery(page, pageSize));
        return HandleResult(competencesGroups);
    }

    [HttpGet("{competenceGroupId:guid}")]
    public async Task<IActionResult> GetGroupById([FromRoute] Guid competenceGroupId)
    {
        var competencesGroup = await _sender.Send(new GetCompetenceGroupQuery(competenceGroupId));
        return HandleResult(competencesGroup);
    }

    [HttpPost]
    public async Task<IActionResult> AddCompetenceGroupWithCompetences([FromBody] AddCompetenceGroupCommand command)
    {
        var competencesGroup = await _sender.Send(command);
        return HandleResult(competencesGroup);
    }

    [HttpPost("{competenceGroupId:guid}/competences")]
    public async Task<IActionResult> AddCompetence([FromRoute] Guid competenceGroupId, [FromBody] AddCompetenceCommand command)
    {
        var competence = await _sender.Send(command with { CompetencesGroupId = competenceGroupId });
        return HandleResult(competence);
    }

    [HttpDelete("{competenceGroupId:guid}")]
    public async Task<IActionResult> DeleteCompetenceGroup([FromRoute] Guid competenceGroupId)
    {
        var result = await _sender.Send(new DeleteCompetenceGroupCommand(competenceGroupId));
        return HandleResult(result);
    }

    [HttpDelete("{competenceGroupId:guid}/competences/{competenceId:guid}")]
    public async Task<IActionResult> DeleteCompetence([FromRoute] Guid competenceGroupId, [FromRoute] Guid competenceId)
    {
        var result = await _sender.Send(new DeleteCompetenceCommand(competenceGroupId, competenceId));
        return HandleResult(result);
    }
}