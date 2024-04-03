namespace Tutoring.Common.Primitives;

public class Result
{
    public bool IsSuccess { get; init; }
    public int StatusCode { get; init; }
    public string? Message { get; init; }

    protected Result(bool isSuccess, int statusCode, string? message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }

    #region Result

    public static Result Ok()
        => new(true, 200, null);

    public static Result BadRequest(string message)
        => new(false, 400, message);

    public static Result BadRequest(string message, params object[] args)
        => new(false, 400, string.Format(message, args.Select(x => x.ToString())));

    public static Result Unauthorized(string message)
        => new(false, 401, message);

    public static Result Unauthorized(string message, params object[] args)
        => new(false, 401, string.Format(message, args.Select(x => x.ToString())));

    public static Result NotFound(string message)
        => new(false, 404, message);

    public static Result NotFound(string message, params object[] args)
        => new(false, 404, string.Format(message, args.Select(x => x.ToString())));

    public static Result InternalServerError(string message)
        => new(false, 500, message);

    public static Result InternalServerError(string message, params object[] args)
        => new(false, 500, string.Format(message, args.Select(x => x.ToString())));

    #endregion

    #region Result<T>

    public static Result<T> Ok<T>(T value)
        => Result<T>.Ok(value);

    public static Result<T> BadRequest<T>(string message)
        => Result<T>.BadRequest(message);

    public static Result<T> BadRequest<T>(string message, params object[] args)
        => Result<T>.BadRequest(message, args);

    public static Result<T> Unauthorized<T>(string message)
        => Result<T>.Unauthorized(message);

    public static Result<T> Unauthorized<T>(string message, params object[] args)
        => Result<T>.Unauthorized(message, args);

    public static Result<T> NotFound<T>(string message)
        => Result<T>.NotFound(message);

    public static Result<T> NotFound<T>(string message, params object[] args)
        => Result<T>.NotFound(message, args);

    public static Result<T> InternalServerError<T>(string message)
        => Result<T>.InternalServerError(message);

    public static Result<T> InternalServerError<T>(string message, params object[] args)
        => Result<T>.InternalServerError(message, args);

    #endregion
}