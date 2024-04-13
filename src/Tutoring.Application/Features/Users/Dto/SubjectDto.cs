using Tutoring.Domain.Competences;
using Tutoring.Domain.Subjects;

namespace Tutoring.Application.Features.Users.Dto;

public class SubjectDto
{
    public string Description { get; init; } = null!;
    public List<CompetenceId> CompetenceIds { get; init; } = null!;

    public static SubjectDto AsDto(Subject subject)
    {
        return new SubjectDto
        {
            Description = subject.Description,
            CompetenceIds = subject.CompetenceIds.ToList()
        };
    }
}