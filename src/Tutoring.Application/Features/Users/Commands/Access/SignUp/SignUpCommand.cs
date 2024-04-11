using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;
using UserPassword = Tutoring.Domain.Users.ValueObjects.Password;

namespace Tutoring.Application.Features.Users.Commands.Access.SignUp;

public record SignUpCommand(string Email, string FirstName, string LastName, string Password, Role Role) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<SignUpCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsWithEmailAsync(new Email(request.Email), cancellationToken))
                return Result.BadRequest<Guid>("Email already exists");

            if (request.Role is not (Role.Student or Role.Tutor))
                return Result.BadRequest<Guid>("Invalid role");

            var email = new Email(request.Email);
            var firstName = new Name(request.FirstName);
            var lastName = new Name(request.LastName);
            var password = UserPassword.Create(request.Password);

            User user = null!;
            Functional.IfElse(request.Role is Role.Student,
                () => user = Student.Create(email, firstName, lastName, password),
                () => user = Tutor.Create(email, firstName, lastName, password));

            await _userRepository.AddAsync(user, cancellationToken);
            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(user.Id.Value);
        }
    }
}