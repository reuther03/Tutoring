using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Reviews;
using Tutoring.Domain.Users;
using Tutoring.Infrastructure.Database.Converters;

namespace Tutoring.Infrastructure.Database.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Description)
            .HasConversion<DescriptionConverter>();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            //tak dziala a z hasConversion<useridconverter> nie dziala
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}