using FluentValidation.Results;

namespace Tutoring.Common.Exceptions.Application;

public sealed class ApplicationValidationException : AppException
{
    public ApplicationValidationException(string message)
        : base(message)
    {
    }

    public ApplicationValidationException(string messageFormat, params object[] args)
        : base(messageFormat, args)
    {
    }

    public ApplicationValidationException(string messageFormat, IEnumerable<ValidationFailure> validationFailures)
        : base(string.Format(messageFormat, string.Join(", ", validationFailures.Select(x => x.ErrorMessage))))
    {
    }
}