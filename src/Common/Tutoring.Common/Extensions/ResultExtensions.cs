using Tutoring.Common.Primitives;

namespace Tutoring.Common.Extensions;

public static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T value)
    {
        return Result<T>.Ok(value);
    }

    public static Result<T> Map<T>(this Result result, T value)
    {
        return result.IsSuccess  // true
            ? Result<T>.Ok(value) // Result<T>.Ok(value)
            : Result<T>.BadRequest(result.Message!); // Result<T>.BadRequest(result.Message!)
    }
}