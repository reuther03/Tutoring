using MediatR;
using Tutoring.Common.Primitives;

namespace Tutoring.Common.Abstractions;

/// <summary>
/// Marker interface for <see cref="ICommand"/> and <see cref="ICommand{TResponse}"/>
/// </summary>
public interface ICommandBase;

/// <summary>
/// Marker interface for commands
/// </summary>
public interface ICommand : IRequest<Result<Unit>>, ICommandBase;

/// <summary>
/// Marker interface for commands with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase;