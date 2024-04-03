using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.Users;

public record UserId : EntityId
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId New() => new(Guid.NewGuid());
    public static UserId From(Guid value) => new(value);
    public static UserId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(UserId userId) => userId.Value;
    public static implicit operator UserId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}