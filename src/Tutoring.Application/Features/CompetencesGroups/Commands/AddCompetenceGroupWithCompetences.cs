using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record AddCompetenceGroupWithCompetences(string GroupName, string GroupDescription, List<CompetencePayload> Competences) : ICommand<CompetencesGroupPayload>
{
    internal sealed class Handler : ICommandHandler<AddCompetenceGroupWithCompetences, CompetencesGroupPayload>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompetencesGroupRepository _competencesGroupRepository;

        public Handler(IUnitOfWork unitOfWork, ICompetencesGroupRepository competencesGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _competencesGroupRepository = competencesGroupRepository;
        }

        public async Task<Result<CompetencesGroupPayload>> Handle(AddCompetenceGroupWithCompetences request, CancellationToken cancellationToken)
        {
            if (await _competencesGroupRepository.ExistsWithNameAsync(request.GroupName, cancellationToken))
                return Result<CompetencesGroupPayload>.BadRequest("Competences group with this name already exists");

            var competencesGroup = CompetencesGroup.Create(new Name(request.GroupName), new Description(request.GroupDescription));

            // foreach (var competenceRequest in request.Competences)
            // {
            //     var competence = new Competence(CompetenceId.New(), new Name(competenceRequest.DetailName), new Description(competenceRequest.Description));
            //     competencesGroup.AddCompetence(competence);
            // }

            foreach (var competence in request.Competences.Select(competenceRequest
                         => new Competence(CompetenceId.New(), new Name(competenceRequest.DetailName), new Description(competenceRequest.Description))))
            {
                competencesGroup.AddCompetence(competence);
            }

            await _competencesGroupRepository.AddAsync(competencesGroup, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result<CompetencesGroupPayload>.Ok(CompetencesGroupPayload.AsDto(competencesGroup));
        }
    }
}