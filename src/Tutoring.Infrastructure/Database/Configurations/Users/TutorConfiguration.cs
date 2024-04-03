using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Users;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

internal sealed class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.OwnsMany(x => x.CompetenceIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TutorId");
            ownedBuilder.ToTable("TutorCompetenceIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("CompetenceId");

            builder.Metadata.FindNavigation(nameof(Tutor.CompetenceIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}