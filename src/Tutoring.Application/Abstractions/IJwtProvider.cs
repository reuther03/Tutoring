using Tutoring.Domain.Users;

namespace Tutoring.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}