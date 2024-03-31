using Tutoring.Common.Primitives.DomainEvents;

namespace Tutoring.Common.Primitives.Domain;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}