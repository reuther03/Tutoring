using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Features.CompetencesGroups.Payloads;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record UpdateCompetenceGroupCommand(Guid CompetenceGroupId, string Name, string Description, List<UpdateCompetencePayload> Competences) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<UpdateCompetenceGroupCommand, Guid>
    {
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICompetenceGroupRepository competenceGroupRepository, IUnitOfWork unitOfWork)
        {
            _competenceGroupRepository = competenceGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCompetenceGroupCommand request, CancellationToken cancellationToken)
        {
            var competenceGroup = await _competenceGroupRepository.GetByIdAsync(request.CompetenceGroupId, cancellationToken);
            if (competenceGroup is null)
                return Result<Guid>.BadRequest("Competence group not found");

            #region Remove

            var competencesWithId = request.Competences.Where(c => c.Id.HasValue).Select(c => c.Id!.Value).ToList();
            var competencesToRemove = competenceGroup.Competences.Where(c => !competencesWithId.Contains(c.Id)).ToList();
            foreach (var competenceToRemove in competencesToRemove)
                _competenceGroupRepository.RemoveCompetence(competenceToRemove);

            #endregion

            foreach (var competence in request.Competences)
            {
                if (competence.Id is null || competence.Id == Guid.Empty)
                {
                    // TODO: Add new Competence
                    // competenceGroup.AddCompetence(newCompetence);
                }
                else
                {
                    var existingCompetence = competenceGroup.Competences.FirstOrDefault(c => c.Id == CompetenceId.From(competence.Id!.Value));
                    // TODO: Update existing Competence
                }
            }

            // Commit changesv
            throw new NotImplementedException();
        }
    }
}