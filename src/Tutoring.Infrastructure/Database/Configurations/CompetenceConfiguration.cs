using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Configurations;

public class CompetenceConfiguration : IEntityTypeConfiguration<Competence>
{
    public void Configure(EntityTypeBuilder<Competence> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => CompetenceId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.DetailedName)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new Description(x))
            .IsRequired()
            .HasMaxLength(200);
    }
}