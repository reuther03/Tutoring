namespace Tutoring.Common.Exceptions.Application;

public abstract class AppException : Exception
{
    protected AppException(string message)
        : base(message)
    {
    }

    protected AppException(string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args.Select(x => x.ToString())))
    {
    }
}