using System.Text.RegularExpressions;
using Tutoring.Common.Exceptions.Domain;
using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.Users.ValueObjects;

public sealed partial record Email : ValueObject
{
    public string Value { get; }

    private Email()
    {
    }

    public Email(string value)
    {
        Validate(value);
        Value = value;
    }

    private static void Validate(string value)
    {
        if (!EmailRegex().IsMatch(value))
            throw new DomainException("Invalid email: {0}", value);
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);


    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,}$")]
    private static partial Regex EmailRegex();
}