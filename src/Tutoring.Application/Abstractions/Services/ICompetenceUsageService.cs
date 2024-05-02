using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Abstractions.Services;

public interface ICompetenceUsageService
{
    Task<bool> IsInUseByTutorAndStudentAsync(Competence competence, User student, User tutor, CancellationToken cancellationToken = default);
}