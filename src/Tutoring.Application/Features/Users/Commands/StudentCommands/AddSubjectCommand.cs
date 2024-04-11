using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Subjects;

namespace Tutoring.Application.Features.Users.Commands.StudentCommands;

public record AddSubjectCommand(string Description) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddSubjectCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _userRepository.GetStudentByIdAsync(userId, cancellationToken);
            if (user is null)
                return Result<Guid>.NotFound("Student not found");

            var subject = Subject.Create(request.Description);
            user.AddSubject(subject);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(subject.Id);
        }
    }
}