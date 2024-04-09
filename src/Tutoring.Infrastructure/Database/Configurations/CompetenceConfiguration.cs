using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Competences;
using Tutoring.Infrastructure.Database.Converters;

namespace Tutoring.Infrastructure.Database.Configurations;

public class CompetenceConfiguration : IEntityTypeConfiguration<Competence>
{
    public void Configure(EntityTypeBuilder<Competence> builder)
    {
        builder.ToTable("Competences");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<CompetenceIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.DetailedName)
            .HasConversion<NameConverter>()
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion<DescriptionConverter>()
            .IsRequired()
            .HasMaxLength(200);
    }
}