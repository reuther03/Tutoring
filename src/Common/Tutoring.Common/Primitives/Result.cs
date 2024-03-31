namespace Tutoring.Common.Primitives;

public class Result
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }

    protected Result(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    #region Result

    public static Result Success()
        => new(true, null);

    public static Result Fail(string message)
        => new(false, message);

    public static Result Fail(string message, params object[] args)
        => new(false, string.Format(message, args.Select(x => x.ToString())));

    #endregion

    #region Result<T>

    public static Result<T> Success<T>(T value)
        => Result<T>.Success(value);

    public static Result<T> Fail<T>(string message)
        => Result<T>.Fail(message);

    public static Result<T> Fail<T>(string message, params object[] args)
        => Result<T>.Fail(message, args);

    #endregion
}