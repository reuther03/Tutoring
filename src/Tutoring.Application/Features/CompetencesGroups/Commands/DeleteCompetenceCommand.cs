using MediatR;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record DeleteCompetenceCommand(Guid CompetenceGroupId, Guid CompetenceId) : ICommand<Unit>
{
    internal sealed class Handler : ICommandHandler<DeleteCompetenceCommand, Unit>
    {
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICompetenceGroupRepository competenceGroupRepository, IUnitOfWork unitOfWork)
        {
            _competenceGroupRepository = competenceGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var competenceGroup = await _competenceGroupRepository.GetByIdAsync(request.CompetenceGroupId, cancellationToken);
            if (competenceGroup is null)
                return Result<Unit>.BadRequest("Competence group not found");

            var competence = competenceGroup.Competences.FirstOrDefault(x => x.Id == new CompetenceId(request.CompetenceId));
            if (competence is null)
                return Result<Unit>.BadRequest("Competence not found");

            var isAssigned = await _competenceGroupRepository.IsCompetenceInUseAsync(competence.Id, cancellationToken);
            if (isAssigned)
                return Result<Unit>.BadRequest("Competence is assigned to user");

            competenceGroup.RemoveCompetence(competence);

            _competenceGroupRepository.RemoveCompetence(competence);
            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(Unit.Value);
        }
    }
}