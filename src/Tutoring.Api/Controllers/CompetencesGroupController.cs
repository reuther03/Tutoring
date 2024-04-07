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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var competencesGroup = await _sender.Send(new GetCompetencesGroupQuery(id));
        return HandleResult(competencesGroup);
    }

    [HttpPost]
    public async Task<IActionResult> AddCompetenceGroupWithCompetences([FromBody] AddCompetenceGroupWithCompetences command)
    {
        var competencesGroup = await _sender.Send(command);
        return HandleResult(competencesGroup);
    }

    // [HttpPost("{CompetenceGroupId:guid}/competences")]
    // public async Task<IActionResult> AddCompetence([FromRoute] Guid competenceGroupId, [FromBody] AddCompetence command)
    // {
    //     var competenceId = await _sender.Send(command with { CompetencesGroupId = competenceGroupId });
    //     return HandleResult(competenceId);
    // }

    [HttpPost("{id:guid}/competences")]
    public async Task<IActionResult> AddCompetence([FromRoute] Guid id, [FromBody] AddCompetence command)
    {
        var competence = await _sender.Send(command with { CompetencesGroupId = id });
        return HandleResult(competence);
    }
}