using Tutoring.Common.Primitives.DomainEvents;

namespace Tutoring.Common.Primitives.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// The domain events raised by the entity.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    //TODO set jest public bo w interceotorze nie bylo dostepnu do pola
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }

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