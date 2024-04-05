using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.Competences;

public record CompetenceId : EntityId
{
    public CompetenceId(Guid value) : base(value)
    {
    }

    public static CompetenceId New() => new(Guid.NewGuid());
    public static CompetenceId From(Guid value) => new(value);
    public static CompetenceId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(CompetenceId competenceId) => competenceId.Value;
    public static implicit operator CompetenceId(Guid userId) => new(userId);


    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}