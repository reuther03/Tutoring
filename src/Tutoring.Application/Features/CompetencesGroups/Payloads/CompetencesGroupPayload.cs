namespace Tutoring.Application.Features.CompetencesGroups.Payloads;

public class CompetencesGroupPayload
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetencePayload> Competences { get; init; } = null!;

    public static CompetencesGroupPayload AsDto(Domain.Competences.CompetenceGroup competenceGroup)
    {
        return new CompetencesGroupPayload
        {
            Name = competenceGroup.Name,
            Description = competenceGroup.Description,
            Competences = competenceGroup.Competences.Select(CompetencePayload.AsDto).ToList()
        };
    }
}