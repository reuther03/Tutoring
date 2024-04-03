using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Users;

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
    }
}