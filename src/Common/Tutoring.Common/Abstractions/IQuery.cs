using MediatR;
using Tutoring.Common.Primitives;

namespace Tutoring.Common.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;