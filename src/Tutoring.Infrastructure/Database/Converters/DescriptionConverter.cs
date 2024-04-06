using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Common.ValueObjects;

namespace Tutoring.Infrastructure.Database.Converters;

public class DescriptionConverter : ValueConverter<Description, string>
{
    public DescriptionConverter() : base(
        x => x.Value,
        x => new Description(x))
    {
    }
}