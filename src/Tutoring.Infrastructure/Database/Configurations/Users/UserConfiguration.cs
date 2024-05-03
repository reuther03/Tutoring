using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Users;
using Tutoring.Infrastructure.Database.Converters;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<Student>(nameof(Student))
            .HasValue<Tutor>(nameof(Tutor))
            .HasValue<BackOfficeUser>(nameof(BackOfficeUser))
            .IsComplete(false);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<UserIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion<EmailConverter>()
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FirstName)
            .HasConversion<NameConverter>()
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .HasConversion<NameConverter>()
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .HasConversion<PasswordConverter>()
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.Reviews)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Availabilities)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => !x.IsArchived);
    }
}