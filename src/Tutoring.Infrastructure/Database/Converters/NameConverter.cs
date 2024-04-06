using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Converters;

public class NameConverter : ValueConverter<Name, string>
{
    public NameConverter() : base(
        x => x.Value,
        x => new Name(x))
    {
    }
}