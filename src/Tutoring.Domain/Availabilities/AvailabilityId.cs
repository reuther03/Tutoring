using Tutoring.Common.Exceptions.Domain;
using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.Availabilities;

public record AvailabilityId : ValueObject
{
    public Guid Value { get; }

    public AvailabilityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Availability id cannot be empty.");
        }

        Value = value;
    }

    public static AvailabilityId New() => new(Guid.NewGuid());
    public static AvailabilityId From(Guid value) => new(value);
    public static AvailabilityId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(AvailabilityId tripId) => tripId.Value;
    public static implicit operator AvailabilityId(Guid tripId) => new(tripId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}