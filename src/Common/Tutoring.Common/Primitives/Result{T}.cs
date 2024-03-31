namespace Tutoring.Common.Primitives;

public class Result<T> : Result
{
    public T? Value { get; init; }

    private Result(bool isSuccess, string? message, T? value)
        : base(isSuccess, message)
    {
        Value = value;
    }

    internal static Result<T> Success(T value)
        => new(true, null, value);

    internal new static Result<T> Fail(string message)
        => new(false, message, default);

    internal new static Result<T> Fail(string message, params object[] args)
        => new(false, string.Format(message, args.Select(x => x.ToString())), default);
}