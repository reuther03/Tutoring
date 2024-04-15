using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Tutoring.Common.Abstractions;

namespace Tutoring.Application.Behaviores;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private const string QuerySuffix = "Query";
    private const string CommandSuffix = "Command";

    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var baseTypeName = GetBaseTypeName(request);

        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "[{Timestamp} | {RequestType}] Handling {RequestName}",
            DateTime.UtcNow,
            baseTypeName,
            typeof(TRequest).Name);

        try
        {
            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation(
                "[{Timestamp} | {RequestType}] Handled successfully {RequestName} | {ElapsedMilliseconds}ms",
                DateTime.UtcNow,
                baseTypeName,
                typeof(TRequest).Name,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception e)
        {
            stopwatch.Stop();

            _logger.LogError(
                e,
                "[{Timestamp} | {RequestType}] Error handling {RequestName} | {ElapsedMilliseconds}ms",
                DateTime.UtcNow,
                baseTypeName,
                typeof(TRequest).Name,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }

    private static string GetBaseTypeName(TRequest request)
    {
        var typeName = request.GetType().Name;

        if (typeName.Contains(QuerySuffix))
            return QuerySuffix;

        if (typeName.Contains(CommandSuffix))
            return CommandSuffix;

        return "Unknown";
    }
}