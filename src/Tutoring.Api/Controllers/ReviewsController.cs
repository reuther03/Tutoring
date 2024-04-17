using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.ReviewCommands;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class ReviewsController : BaseController
{
    private readonly ISender _sender;

    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> AddReview(AddReviewCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}