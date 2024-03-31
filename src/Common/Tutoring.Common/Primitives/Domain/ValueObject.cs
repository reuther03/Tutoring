namespace Tutoring.Common.Primitives.Domain;

public abstract record ValueObject
{
    public abstract override string ToString();
    protected abstract IEnumerable<object> GetAtomicValues();

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), HashCode.Combine);
    }
}