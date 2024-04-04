namespace Tutoring.Infrastructure.Authentication;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
}