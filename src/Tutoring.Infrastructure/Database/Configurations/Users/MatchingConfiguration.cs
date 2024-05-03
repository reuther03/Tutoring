using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Matchings;
using Tutoring.Infrastructure.Database.Converters;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

public class MatchingConfiguration : IEntityTypeConfiguration<Matching>
{
    public void Configure(EntityTypeBuilder<Matching> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.CompetencesGroupName)
            .HasConversion<NameConverter>()
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CompetenceId)
            .HasConversion<CompetenceIdConverter>()
            .IsRequired();


        builder.Property(x => x.StudentId)
            .HasConversion<UserIdConverter>()
            .IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(x => x.TutorId)
            .HasConversion<UserIdConverter>()
            .IsRequired();

        builder.HasOne(x => x.Tutor)
            .WithMany()
            .HasForeignKey(x => x.TutorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => !x.IsArchived);
    }
}