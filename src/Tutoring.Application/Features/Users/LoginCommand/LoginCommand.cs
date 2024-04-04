using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Exceptions.Application;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.LoginCommand;

public record LoginCommand(string Email, string Password) : ICommand<AccessToken>
{
    internal sealed class Handler : ICommandHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public Handler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new ApplicationValidationException("User not found");

            if (!user.Password.Verify(request.Password))
                return Result.BadRequest<AccessToken>("Invalid password");

            var token = AccessToken.Create(user, _jwtProvider.Generate(user));

            return Result.Ok(token);
        }
    }
}