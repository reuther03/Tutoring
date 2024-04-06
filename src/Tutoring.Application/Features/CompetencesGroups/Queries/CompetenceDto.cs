using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public class CompetenceDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;

    public static CompetenceDto AsDto(Competence competence)
    {
        return new CompetenceDto
        {
            Id = competence.Id,
            Name = competence.DetailedName,
            Description = competence.Description
        };
    }
}