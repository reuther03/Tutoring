using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Users;

namespace Tutoring.Domain.Availabilities;

public class Availability : Entity<AvailabilityId>
{
    public TimeOnly From { get; private set; }
    public TimeOnly To { get; private set; }
    public DayOfWeek Day { get; private set; }
    public UserId UserId { get; private set; }

    private Availability()
    {
    }

    private Availability(AvailabilityId id, TimeOnly from, TimeOnly to, DayOfWeek day, UserId userId)
        : base(id)
    {
        From = from;
        To = to;
        Day = day;
        UserId = userId;
    }

    public static Availability Create(TimeOnly from, TimeOnly to, DayOfWeek day, UserId userId)
    {
        var availability = new Availability(AvailabilityId.New(), from, to, day, userId);
        return availability;
    }
}