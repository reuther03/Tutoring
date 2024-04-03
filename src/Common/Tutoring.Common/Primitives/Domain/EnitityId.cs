namespace Tutoring.Common.Primitives.Domain;

/// <summary>
/// Base class for <see cref="AggregateRoot{TId}" /> identifiers.
/// </summary>
public abstract record EntityId : ValueObject
{
    public Guid Value { get; }

    protected EntityId(Guid value) => Value = value;

    public static implicit operator Guid(EntityId id) => id.Value;
    public static implicit operator string(EntityId id) => id.Value.ToString();

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Value);
    }
}