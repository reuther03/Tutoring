using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tutoring.Api.Controllers.Base;
using Tutoring.Application.Features.Users.Commands.ReviewCommands;
using Tutoring.Application.Features.Users.Queries.Reviews;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Api.Controllers;

public class ReviewsController : BaseController
{
    private readonly ISender _sender;

    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{userId:guid}")]
    // [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> GetUserReviews([FromRoute]Guid userId, int page = 1, int pageSize = 10)
    {
        var result = await _sender.Send(new GetUserReviewsQuery(userId, page, pageSize));
        return HandleResult(result);
    }

    [HttpPost]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> AddReview(AddReviewCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}