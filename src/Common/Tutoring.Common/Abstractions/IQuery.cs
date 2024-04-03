using MediatR;

namespace Tutoring.Common.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>;