using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using ICommand = Tutoring.Common.Abstractions.ICommand;

namespace Tutoring.Application.Features.Matchings.Commands;

public record DeleteMatchCommand(Guid MatchingId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteMatchCommand>
    {
        private readonly IMatchingRepository _matchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMatchingRepository matchRepository, IUnitOfWork unitOfWork)
        {
            _matchRepository = matchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetMatchingByIdAsync(request.MatchingId, cancellationToken);
            if (match is null)
                return Result.BadRequest("Match not found");

            if (match.IsArchived)
                return Result.BadRequest("Match is already archived");

            // _matchRepository.ArchiveMatching(match);
            _matchRepository.RemoveMatching(match);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(request.MatchingId);
        }
    }
}