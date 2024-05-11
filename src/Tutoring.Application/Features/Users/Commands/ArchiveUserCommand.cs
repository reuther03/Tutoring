using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Commands;

public class ArchiveUserCommand : ICommand
{
    internal sealed class ArchiveUserCommandHandler : ICommandHandler<ArchiveUserCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveUserCommandHandler(IUserContext userContext, IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(ArchiveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken);
            if (user is null)
                return Result<Guid>.NotFound("User not found");

            if (user.IsArchived)
                return Result<Guid>.BadRequest("User is already archived");

            _userRepository.RemoveUser(user);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(user.Id);
        }
    }
}