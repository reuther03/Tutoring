using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.Users.Tutors;

public record AddCompetenceCommand(CompetenceId CompetenceId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddCompetenceCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(AddCompetenceCommand request, CancellationToken cancellationToken)
        {
            var id = _userContext.UserId;
            if (id is null)
                return Result.Unauthorized<Guid>("User not authenticated");

            var tutor = await _userRepository.GetTutorByIdAsync(id, cancellationToken);

            if (tutor is null)
                return Result.NotFound<Guid>("Tutor not found");


            var competence = await _userRepository.GetCompetenceByIdAsync(request.CompetenceId, cancellationToken);
            if (competence is null)
                return Result.NotFound<Guid>("Competence not found");

            //czy jest sens zrobic competence repository i sprawdzac czy kompetencja istnieje?


            tutor.AddCompetence(competence.Id);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(tutor.Id.Value);
        }
    }
}