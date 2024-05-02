using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tutoring.Common.Primitives.DomainEvents;

namespace Tutoring.Common.Primitives.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// The domain events raised by the entity.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // public bool IsArchived { get; private set; }

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
}