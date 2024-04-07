using System.Text.Json.Serialization;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record AddCompetence([property: JsonIgnore] Guid CompetencesGroupId, string Name, string Description) : ICommand<Competence>
{
    internal sealed class Handler : ICommandHandler<AddCompetence, Competence>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompetencesGroupRepository _competencesGroupRepository;

        public Handler(IUnitOfWork unitOfWork, ICompetencesGroupRepository competencesGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _competencesGroupRepository = competencesGroupRepository;
        }

        public async Task<Result<Competence>> Handle(AddCompetence request, CancellationToken cancellationToken)
        {
            var competencesGroup = await _competencesGroupRepository.GetByIdAsync(request.CompetencesGroupId, cancellationToken);

            var competence = Competence.Create(request.Name, request.Description);

            competencesGroup.AddCompetence(competence);

            // await _competencesGroupRepository.UpdateAsync(competencesGroup, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result<Competence>.Ok(competence);
        }
    }
}