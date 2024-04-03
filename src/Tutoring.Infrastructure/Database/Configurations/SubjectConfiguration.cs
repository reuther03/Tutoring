using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Subjects;

namespace Tutoring.Infrastructure.Database.Configurations;

internal sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.OwnsMany(x => x.CompetenceIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("SubjectId");
            ownedBuilder.ToTable("SubjectCompetenceIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("CompetenceId");

            builder.Metadata.FindNavigation(nameof(Subject.CompetenceIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        // NOTE: nie zadziałają metody `Contains` i `Any` na kolekcji CompetenceIds
        // builder.Property(x => x.CompetenceIds)
        //     .HasConversion(new ValueConverter<List<CompetenceId>, string>(
        //         v => string.Join(";", v.Select(x => x.Value)),
        //         v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(CompetenceId.From).ToList()))
        //     .Metadata.SetValueComparer(new ValueComparer<List<CompetenceId>>(
        //         (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
        //         c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        //         c => c.ToList()));
    }
}