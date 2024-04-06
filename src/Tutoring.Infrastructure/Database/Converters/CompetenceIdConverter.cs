using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Domain.Competences;

namespace Tutoring.Infrastructure.Database.Converters;

public class CompetenceIdConverter : ValueConverter<CompetenceId, Guid>
{
    public CompetenceIdConverter() : base(
        x => x.Value,
        x => CompetenceId.From(x))
    {
    }
}