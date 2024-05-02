using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Abstractions.Services;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Matchings;

namespace Tutoring.Application.Features.Matchings.Commands;

public record MatchCommand(Guid TutorId, Guid CompetenceId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<MatchCommand, Guid>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IMatchingRepository _matchingRepository;
        private readonly ICompetenceUsageService _competenceUsageService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IUserContext userContext,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ICompetenceGroupRepository competenceGroupRepository,
            IMatchingRepository matchingRepository,
            ICompetenceUsageService competenceUsageService
        )
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _competenceGroupRepository = competenceGroupRepository;
            _matchingRepository = matchingRepository;
            _competenceUsageService = competenceUsageService;
        }

        public async Task<Result<Guid>> Handle(MatchCommand request, CancellationToken cancellationToken)
        {
            var studentId = _userContext.UserId;
            var student = await _userRepository.GetStudentByIdAsync(studentId, cancellationToken);
            if (student is null)
            {
                return Result<Guid>.NotFound("Student not found");
            }

            var tutor = await _userRepository.GetTutorByIdAsync(request.TutorId, cancellationToken);
            if (tutor is null)
            {
                return Result<Guid>.NotFound("Tutor not found");
            }

            var competence = await _competenceGroupRepository.GetCompetenceByIdAsync(request.CompetenceId, cancellationToken);
            if (competence is null)
            {
                return Result<Guid>.NotFound("Competence not found");
            }

            var isMatched = await _competenceUsageService.IsInUseByTutorAndStudentAsync(competence, student!, tutor, cancellationToken);
            if (!isMatched)
            {
                return Result<Guid>.NotFound("Competence is not in use by any tutor or student");
            }

            var competenceGroup = await _competenceGroupRepository.GetByCompetenceIdAsync(request.CompetenceId, cancellationToken);
            if (competenceGroup is null)
            {
                return Result<Guid>.NotFound("Competence group not found");
            }

            var match = Matching.Create(competenceGroup.Name, request.CompetenceId, student.Id, request.TutorId);

            await _matchingRepository.AddAsync(match, cancellationToken);
            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(match.Id);
        }
    }
}