using MediatR;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.Users.Commands.TutorCommands;

public record RemoveCompetenceCommand(Guid CompetenceId) : ICommand
{
    internal sealed class RemoveCompetenceCommandHandler : ICommandHandler<RemoveCompetenceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCompetenceCommandHandler(IUserContext userContext, IUserRepository userRepository, ICompetenceGroupRepository competenceGroupRepository,
            IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _competenceGroupRepository = competenceGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveCompetenceCommand request, CancellationToken cancellationToken)
        {
            var userRole = _userContext.Role;
            if (userRole != Role.Tutor)
                return Result<Unit>.Unauthorized("User is not a tutor");

            //TODO: mam sprawdzac id czy jest nulem, czy zostawic !
            var tutor = await _userRepository.GetTutorByIdAsync(_userContext.UserId!, cancellationToken);
            if (tutor is null)
                return Result<Unit>.BadRequest("Tutor not found");

            var competence = await _competenceGroupRepository.GetCompetenceByIdAsync(request.CompetenceId, cancellationToken);
            if (competence is null)
                return Result<Unit>.BadRequest("Competence not found");

            tutor.RemoveCompetence(competence.Id);
            var result = await _unitOfWork.CommitAsync(cancellationToken);

            return result.Map(Unit.Value);


        }
    }
}