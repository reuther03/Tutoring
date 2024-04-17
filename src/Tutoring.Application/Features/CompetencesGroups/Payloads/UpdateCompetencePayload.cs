namespace Tutoring.Application.Features.CompetencesGroups.Payloads;

public class UpdateCompetencePayload
{
    public Guid? Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
}