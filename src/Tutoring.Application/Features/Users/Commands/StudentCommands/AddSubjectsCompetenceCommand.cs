using System.Text.Json.Serialization;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Commands.StudentCommands;

public record AddSubjectsCompetenceCommand(
    [property: JsonIgnore]
    Guid SubjectId,
    [property: JsonIgnore]
    Guid CompetenceId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddSubjectsCompetenceCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, ICompetenceGroupRepository competenceGroupRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _userRepository = userRepository;
            _competenceGroupRepository = competenceGroupRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(AddSubjectsCompetenceCommand request,
            CancellationToken cancellationToken)
        {
            var studentId = _userContext.UserId;
            var student = await _userRepository.GetStudentByIdAsync(studentId, cancellationToken);


            var subject = student?.Subjects.FirstOrDefault(x => x.Id == request.SubjectId);
            if (subject is null)
                return Result<Guid>.NotFound("Subject not found");

            var competence = await _competenceGroupRepository.GetCompetenceByIdAsync(request.CompetenceId, cancellationToken);
            if (competence is null)
                return Result<Guid>.NotFound("Competence not found");

            subject.AddCompetence(competence);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(subject.Id);
        }
    }
}