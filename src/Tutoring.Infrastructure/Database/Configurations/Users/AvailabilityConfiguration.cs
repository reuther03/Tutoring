using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Availabilities;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.From)
            .IsRequired();

        builder.Property(x => x.To)
            .IsRequired();

        builder.Property(x => x.Day)
            .IsRequired();
    }
}