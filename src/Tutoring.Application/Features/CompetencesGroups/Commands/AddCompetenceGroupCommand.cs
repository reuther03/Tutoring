using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Features.CompetencesGroups.Payloads;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record AddCompetenceGroupCommand(string GroupName, string GroupDescription, List<CompetencePayload> Competences) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddCompetenceGroupCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;

        public Handler(IUnitOfWork unitOfWork, ICompetenceGroupRepository competenceGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _competenceGroupRepository = competenceGroupRepository;
        }

        public async Task<Result<Guid>> Handle(AddCompetenceGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _competenceGroupRepository.ExistsWithNameAsync(request.GroupName, cancellationToken))
                return Result<Guid>.BadRequest("Competences group with this name already exists");

            var competencesGroup = CompetenceGroup.Create(request.GroupName, request.GroupDescription);

            foreach (var competencePayload in request.Competences)
            {
                var competence = Competence.Create(competencePayload.DetailName, competencePayload.Description);
                competencesGroup.AddCompetence(competence);
            }

            // var competences = request.Competences.Select(competence => Competence.Create(competence.DetailName, competence.Description)).ToList();
            // competences.ForEach(competence => competencesGroup.AddCompetence(competence));

            _competenceGroupRepository.Add(competencesGroup);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(competencesGroup.Id);
        }
    }
}