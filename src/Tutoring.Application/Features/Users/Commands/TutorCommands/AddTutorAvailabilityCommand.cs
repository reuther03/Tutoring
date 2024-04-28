using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Availabilities;

namespace Tutoring.Application.Features.Users.Commands.TutorCommands;

public record AddTutorAvailabilityCommand(TimeOnly From, TimeOnly To, Day Day) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddTutorAvailabilityCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(AddTutorAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var tutorId = _userContext.UserId;
            if (tutorId is null)
                return Result.Unauthorized<Guid>("User not authenticated");

            var tutor = await _userRepository.GetTutorByIdAsync(tutorId, cancellationToken);
            if (tutor is null)
                return Result.NotFound<Guid>("Tutor not found");

            var availability = Availability.Create(request.From, request.To, request.Day);
            tutor.AddAvailability(availability);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(tutor.Id.Value);
        }
    }
}