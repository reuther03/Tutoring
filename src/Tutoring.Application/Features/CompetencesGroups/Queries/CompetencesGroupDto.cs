namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public class CompetencesGroupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetenceDto> Competences { get; init; } = null!;


    public static CompetencesGroupDto AsDto(Domain.Competences.CompetencesGroup competencesGroup)
    {
        return new CompetencesGroupDto
        {
            Id = competencesGroup.Id,
            Name = competencesGroup.Name,
            Description = competencesGroup.Description,
            Competences = competencesGroup.Competences.Select(CompetenceDto.AsDto).ToList()
        };
    }
}