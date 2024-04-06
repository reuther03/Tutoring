using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Users;

namespace Tutoring.Infrastructure.Database.Configurations.Users;

internal sealed class BackOfficeUserConfiguration : IEntityTypeConfiguration<BackOfficeUser>
{
    public void Configure(EntityTypeBuilder<BackOfficeUser> builder)
    {

    }
}