using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Configurations;

public class CompetencesGroupConfiguration : IEntityTypeConfiguration<CompetencesGroup>
{
    public void Configure(EntityTypeBuilder<CompetencesGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new Description(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(x => x.Competences)
            .WithOne()
            .HasForeignKey("CompetencesGroupId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}