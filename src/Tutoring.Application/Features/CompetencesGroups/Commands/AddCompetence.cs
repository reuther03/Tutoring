using System.Text.Json.Serialization;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record AddCompetenceCommand([property: JsonIgnore] Guid CompetencesGroupId, string Name, string Description) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddCompetenceCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;

        public Handler(IUnitOfWork unitOfWork, ICompetenceGroupRepository competenceGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _competenceGroupRepository = competenceGroupRepository;
        }

        public async Task<Result<Guid>> Handle(AddCompetenceCommand request, CancellationToken cancellationToken)
        {
            var competencesGroup = await _competenceGroupRepository.GetByIdAsync(request.CompetencesGroupId, cancellationToken);

            if (competencesGroup is null)
                return Result<Guid>.BadRequest("Competences group not found");

            var competence = Competence.Create(request.Name, request.Description);

            competencesGroup.AddCompetence(competence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(competence.Id.Value);
        }
    }
}