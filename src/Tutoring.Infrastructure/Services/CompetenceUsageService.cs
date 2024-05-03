using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Services;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;
using Tutoring.Infrastructure.Database;

namespace Tutoring.Infrastructure.Services;

internal sealed class CompetenceUsageService : ICompetenceUsageService
{
    private readonly TutoringDbContext _context;

    public CompetenceUsageService(TutoringDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsInUseByTutorAndStudentAsync(Competence competence, User student, User tutor, CancellationToken cancellationToken = default)
    {
        var isAssignedToTutor = await _context.Users.OfType<Tutor>()
            .Where(x => x.Id == tutor.Id)
            .AnyAsync(x => x.CompetenceIds.Any(c => c.Value == competence.Id.Value), cancellationToken);

        var isAssignedToStudent = await _context.Users.OfType<Student>()
            .Where(x => x.Id == student.Id)
            .Include(x => x.Subjects)
            .SelectMany(x => x.Subjects)
            .AnyAsync(x => x.CompetenceIds.Any(c => c.Value == competence.Id.Value), cancellationToken);

        return isAssignedToTutor && isAssignedToStudent;
    }
}