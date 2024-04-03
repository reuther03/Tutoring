using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<Student>(nameof(Student))
            .HasValue<Tutor>(nameof(Tutor))
            .IsComplete(false);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();
    }
}