namespace Tutoring.Application.Features.CompetencesGroups.Dto;

public class CompetencesGroupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetenceDto> Competences { get; init; } = null!;


    public static CompetencesGroupDto AsDto(Domain.Competences.CompetenceGroup competenceGroup)
    {
        return new CompetencesGroupDto
        {
            Id = competenceGroup.Id,
            Name = competenceGroup.Name,
            Description = competenceGroup.Description,
            Competences = competenceGroup.Competences.Select(CompetenceDto.AsDto).ToList()
        };
    }
}