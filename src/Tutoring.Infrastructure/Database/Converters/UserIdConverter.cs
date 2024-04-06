using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tutoring.Domain.Users;

namespace Tutoring.Infrastructure.Database.Converters;

public class UserIdConverter : ValueConverter<UserId, Guid>
{
    public UserIdConverter() : base(
        x => x.Value,
        x => UserId.From(x))
    {
    }
}