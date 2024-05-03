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
    // public bool IsArchived { get; private set; }
    // public DateTime? ArchivedAt { get; private set; }

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    // public void SetArchiveData(bool isArchived, DateTime? archivedAt)
    // {
    //     IsArchived = isArchived;
    //     ArchivedAt = archivedAt;
    // }
}