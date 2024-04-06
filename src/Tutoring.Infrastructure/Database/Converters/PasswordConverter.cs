using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Converters;

public class PasswordConverter : ValueConverter<Password, string>
{
    public PasswordConverter() : base(
        x => x.Value,
        x => new Password(x))
    {
    }
}