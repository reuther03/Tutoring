using MediatR;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Commands.ReviewCommands;

public record DeleteReviewCommand(Guid ReviewId, Guid UserId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken);
            if (currentUser is null)
            {
                return Result.NotFound("User not found");
            }

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.NotFound("User not found");
            }

            var review = await _userRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);
            if (review is null)
            {
                return Result.NotFound("Review not found");
            }

            if (currentUser.Id == user.Id)
            {
                return Result.BadRequest("You can't delete review from yourself");
            }

            if (currentUser.Role == user.Role)
            {
                return Result.BadRequest("You can't delete review from user with the same role");
            }

            _userRepository.RemoveReview(review);
            var result = await _unitOfWork.CommitAsync(cancellationToken);

            return result.Map(Unit.Value);
        }
    }
}