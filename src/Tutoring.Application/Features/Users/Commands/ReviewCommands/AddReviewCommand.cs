﻿using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Reviews;

namespace Tutoring.Application.Features.Users.Commands.ReviewCommands;

public record AddReviewCommand(Guid UserId, string Description, int Rating) : IQuery<Guid>
{
    internal sealed class Handler : IQueryHandler<AddReviewCommand, Guid>
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

        public async Task<Result<Guid>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _userContext.UserId;
            var userId = request.UserId;
            if (currentUserId == Domain.Users.UserId.From(userId))
                return Result.BadRequest<Guid>("You can't review yourself.");

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user is null)
                return Result.NotFound<Guid>("User not found.");

            if (user.Role == _userContext.Role)
                return Result.BadRequest<Guid>("You can't review user with the same role.");

            var review = Review.Create(new Description(request.Description), request.Rating, currentUserId!);
            user.AddReview(review);

            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(review.Id);
        }
    }
}