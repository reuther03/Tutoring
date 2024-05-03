using Tutoring.Common.Primitives.DomainEvents;

namespace Tutoring.Common.Primitives.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// The domain events raised by the entity.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id, DateTime? archivedAt, bool isArchived = false)
        : base(id)
    {
        IsArchived = isArchived;
        ArchivedAt = archivedAt;
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}