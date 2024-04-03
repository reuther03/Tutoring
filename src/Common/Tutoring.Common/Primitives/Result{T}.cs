namespace Tutoring.Common.Primitives;

public class Result<T> : Result
{
    public T? Value { get; init; }

    private Result(bool isSuccess, int statusCode, string? message, T? value)
        : base(isSuccess, statusCode, message)
    {
        Value = value;
    }

    public static Result<T> Ok(T value)
        => new(true, 200, null, value);

    public new static Result<T> BadRequest(string message)
        => new(false, 400, message, default);

    public new static Result<T> BadRequest(string message, params object[] args)
        => new(false, 400, string.Format(message, args), default);

    public new static Result<T> Unauthorized(string message)
        => new(false, 401, message, default);

    public new static Result<T> Unauthorized(string message, params object[] args)
        => new(false, 401, string.Format(message, args), default);

    public new static Result<T> NotFound(string message)
        => new(false, 404, message, default);

    public new static Result<T> NotFound(string message, params object[] args)
        => new(false, 404, string.Format(message, args), default);

    public new static Result<T> InternalServerError(string message)
        => new(false, 500, message, default);

    public new static Result<T> InternalServerError(string message, params object[] args)
        => new(false, 500, string.Format(message, args), default);
}