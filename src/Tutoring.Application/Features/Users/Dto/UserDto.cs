using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class UserDto
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Role { get; init; } = null!;
    public List<CompetenceId> Competences { get; init; }
    public List<SubjectDto> Subject { get; init; }
    public List<ReviewDto> Reviews { get; init; } = null!;
    public double AverageRating { get; init; }

    public static UserDto AsTutorDto(Tutor tutor)
    {
        return new UserDto
        {
            Email = tutor.Email.Value,
            FirstName = tutor.FirstName.Value,
            LastName = tutor.LastName.Value,
            Role = tutor.Role.ToString(),
            Competences = tutor.CompetenceIds.ToList(),
            Reviews = tutor.Reviews.Select(ReviewDto.AsDto).ToList(),
            AverageRating = tutor.Reviews.Count == 0 ? 0 : tutor.Reviews.Average(x => x.Rating)
        };
    }

    public static UserDto AsStudentDto(Student student)
    {
        return new UserDto
        {
            Email = student.Email.Value,
            FirstName = student.FirstName.Value,
            LastName = student.LastName.Value,
            Role = student.Role.ToString(),
            Subject = student.Subjects.Select(SubjectDto.AsDto).ToList(),
            Reviews = student.Reviews.Select(ReviewDto.AsDto).ToList(),
            AverageRating = student.Reviews.Count == 0 ? 0 : student.Reviews.Average(x => x.Rating)
        };
    }
}