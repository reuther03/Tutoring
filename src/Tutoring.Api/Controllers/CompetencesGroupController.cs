using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
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
}