namespace Tutoring.Application.Features.CompetencesGroups.Dto;

public class CompetenceGroupDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public List<CompetenceDto> Competences { get; init; } = null!;


    public static CompetenceGroupDto AsDto(Domain.Competences.CompetenceGroup competenceGroup)
    {
        return new CompetenceGroupDto
        {
            Id = competenceGroup.Id,
            Name = competenceGroup.Name,
            Description = competenceGroup.Description,
            Competences = competenceGroup.Competences.Select(CompetenceDto.AsDto).ToList()
        };
    }
}