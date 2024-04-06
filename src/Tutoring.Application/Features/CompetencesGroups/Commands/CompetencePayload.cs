using Tutoring.Domain.Competences;

namespace Tutoring.Application.Features.CompetencesGroups.Commands;

public class CompetencePayload
{
    public string DetailName { get; init; } = null!;
    public string Description { get; init; } = null!;

    //asdto
    public static CompetencePayload AsDto(Competence competence)
    {
        return new CompetencePayload
        {
            DetailName = competence.DetailedName,
            Description = competence.Description
        };
    }
}