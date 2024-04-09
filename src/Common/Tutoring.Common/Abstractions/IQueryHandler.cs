using MediatR;
using Tutoring.Common.Primitives;

namespace Tutoring.Common.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;