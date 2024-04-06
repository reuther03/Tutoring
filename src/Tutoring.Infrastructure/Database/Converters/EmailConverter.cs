using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Converters;

public class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        x => x.Value,
        x => new Email(x))
    {
    }
}