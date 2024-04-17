namespace Tutoring.Application.Features.CompetencesGroups.Payloads;

public class CompetenceGroupPayload
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetencePayload> Competences { get; init; } = null!;

    public static CompetenceGroupPayload AsDto(Domain.Competences.CompetenceGroup competenceGroup)
    {
        return new CompetenceGroupPayload
        {
            Name = competenceGroup.Name,
            Description = competenceGroup.Description,
            Competences = competenceGroup.Competences.Select(CompetencePayload.AsDto).ToList()
        };
    }
}