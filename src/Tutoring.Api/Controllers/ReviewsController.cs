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

    /// <summary>
    ///  Gets the current user's reviews
    /// </summary>
    /// <remarks>
    /// Example request:
    ///
    /// GET /reviews/me
    /// </remarks>
    /// <param name="page">page number</param>
    /// <param name="pageSize">page size</param>
    /// <returns>Returns the current user's reviews</returns>
    [HttpGet("me")]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> GetMyReviews(int page = 1, int pageSize = 10)
    {
        var result = await _sender.Send(new GetCurrentUserReviewsQuery(page, pageSize));
        return HandleResult(result);
    }

    /// <summary>
    ///  Gets the user's reviews
    /// </summary>
    /// <remarks>
    /// Example request: <br/>
    ///
    /// GET /reviews/{userId} <br/>
    ///
    /// </remarks>
    /// <param name="userId">The user's id</param>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The page size</param>
    /// <returns>Returns the user's reviews</returns>
    [HttpGet("{userId:guid}")]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> GetUserReviews([FromRoute] Guid userId, int page = 1, int pageSize = 10)
    {
        var result = await _sender.Send(new GetUserReviewsQuery(userId, page, pageSize));
        return HandleResult(result);
    }

    /// <summary>
    /// Adds a review
    /// </summary>
    /// <remarks>
    ///  Example request: <br/>
    ///
    /// { <br/>
    ///  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", <br/>
    ///  "description": "string", <br/>
    ///  "rating": 0 <br/>
    /// } <br/>
    ///  POST /reviews
    /// </remarks>
    /// <param name="command">
    /// userId (Guid): The user's id <br/>
    /// description (string): The review's description <br/>
    /// rating (int): The review's rating (1-5) <br/>
    /// </param>
    /// <returns></returns>
    [HttpPost]
    [AuthorizeRoles(Role.Student, Role.Tutor)]
    public async Task<IActionResult> AddReview(AddReviewCommand command)
    {
        var result = await _sender.Send(command);
        return HandleResult(result);
    }
}