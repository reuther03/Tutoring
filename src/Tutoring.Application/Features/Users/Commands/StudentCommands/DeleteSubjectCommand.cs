using MediatR;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Commands.StudentCommands;

public record DeleteSubjectCommand(Guid SubjectId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteSubjectCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _userRepository.GetSubjectByIdAsync(request.SubjectId, cancellationToken);
            if (subject is null)
                return Result.BadRequest("Subject not found");

            if (subject.CompetenceIds.Any())
                return Result.BadRequest("Subject is not empty ( contains competences )");

            _userRepository.RemoveSubject(subject);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(request.SubjectId);
        }
    }
}