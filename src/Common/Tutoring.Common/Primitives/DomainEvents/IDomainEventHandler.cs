using MediatR;

namespace Tutoring.Common.Primitives.DomainEvents;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;