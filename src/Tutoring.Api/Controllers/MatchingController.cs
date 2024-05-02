using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Matchings.Commands;
using Tutoring.Application.Features.Matchings.Queries;

namespace Tutoring.Api.Controllers;

public class MatchingController : BaseController
{
    private readonly ISender _sender;

    public MatchingController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchTutors([FromQuery] GetFilteredTutorsBySubjects query)
    {
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetFilteredTutors([FromBody] GetFilteredTutorsQuery query)
    {
        var result = await _sender.Send(query);
        return HandleResult(result);
    }

    [HttpPost("match")]
    public async Task<IActionResult> Match([FromBody] MatchCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}