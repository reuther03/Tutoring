using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Users;

namespace Tutoring.Domain.Availabilities;

public class Availability : Entity<Guid>
{
    public TimeOnly From { get; private set; }
    public TimeOnly To { get; private set; }
    public DayOfWeek Day { get; private set; }

    private Availability()
    {
    }

    private Availability(Guid id, TimeOnly from, TimeOnly to, DayOfWeek day)
        : base(id)
    {
        From = from;
        To = to;
        Day = day;
    }

    public static Availability Create(TimeOnly from, TimeOnly to, DayOfWeek day)
    {
        var availability = new Availability(Guid.NewGuid(), from, to, day);
        return availability;
    }
}