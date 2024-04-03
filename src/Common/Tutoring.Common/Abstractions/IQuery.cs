using MediatR;

namespace TripManager.Common.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>;