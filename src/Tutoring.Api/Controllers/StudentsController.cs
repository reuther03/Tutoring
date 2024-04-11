using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.StudentCommands;

namespace Tutoring.Api.Controllers;

public class StudentsController : BaseController
{
    private readonly ISender _sender;

    public StudentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSubject(AddSubjectCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}