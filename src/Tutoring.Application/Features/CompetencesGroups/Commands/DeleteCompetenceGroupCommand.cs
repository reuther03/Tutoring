using MediatR;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public record DeleteCompetenceGroupCommand(Guid CompetenceGroupId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteCompetenceGroupCommand>
    {
        private readonly ICompetenceGroupRepository _competenceGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICompetenceGroupRepository competenceGroupRepository, IUnitOfWork unitOfWork)
        {
            _competenceGroupRepository = competenceGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCompetenceGroupCommand request, CancellationToken cancellationToken)
        {
            var competenceGroup = await _competenceGroupRepository.GetByIdAsync(request.CompetenceGroupId, cancellationToken);

            if (competenceGroup is null)
                return Result<Unit>.BadRequest("Competence group not found");

            if (competenceGroup.Competences.Any())
                return Result<Unit>.BadRequest("Competence group is not empty ( contains competences )");

            _competenceGroupRepository.Remove(competenceGroup);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(Unit.Value);
        }
    }
}