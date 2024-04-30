using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Availabilities;

namespace Tutoring.Application.Features.Matching.Payloads;

public class AvailabilityPayload
{
    public TimeOnly? From { get; init; }
    public TimeOnly? To { get; init; }
    public Day? Day { get; init; }

    public static AvailabilityPayload AsDto(Availability availability)
    {
        return new AvailabilityPayload
        {
            From = availability.From,
            To = availability.To,
            Day = availability.Day
        };
    }
}