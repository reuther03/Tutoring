using System.Text.Json.Serialization;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Commands.TutorCommands.AddTutorCompetence;

public record AddTutorCompetenceCommand([property: JsonIgnore] Guid CompetenceId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddTutorCompetenceCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly ICompetenceGroupRepository _competenceGroupRepository;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserContext userContext, ICompetenceGroupRepository competenceGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userContext = userContext;
            _competenceGroupRepository = competenceGroupRepository;
        }

        public async Task<Result<Guid>> Handle(AddTutorCompetenceCommand request, CancellationToken cancellationToken)
        {
            var id = _userContext.UserId;
            if (id is null)
                return Result.Unauthorized<Guid>("User not authenticated");

            var tutor = await _userRepository.GetTutorByIdAsync(id, cancellationToken);

            if (tutor is null)
                return Result.NotFound<Guid>("Tutor not found");


            var competence = await _competenceGroupRepository.GetCompetenceByIdAsync(request.CompetenceId, cancellationToken);
            if (competence is null)
                return Result.NotFound<Guid>("Competence not found");

            //czy jest sens zrobic competence repository i sprawdzac czy kompetencja istnieje?


            tutor.AddCompetence(competence.Id);
            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(tutor.Id.Value);
        }
    }
}