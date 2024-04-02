namespace Tutoring.Common.Exceptions.Domain;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args.Select(x => x.ToString())))
    {
    }
}