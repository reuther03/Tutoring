namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public class CompetencesGroupPayload
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetencePayload> Competences { get; init; } = null!;

    public static CompetencesGroupPayload AsDto(Domain.Competences.CompetencesGroup competencesGroup)
    {
        return new CompetencesGroupPayload
        {
            Name = competencesGroup.Name,
            Description = competencesGroup.Description,
            Competences = competencesGroup.Competences.Select(CompetencePayload.AsDto).ToList()
        };
    }
}