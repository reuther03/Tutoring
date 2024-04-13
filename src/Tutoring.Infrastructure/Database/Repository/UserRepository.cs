using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly TutoringDbContext _context;

    public UserRepository(TutoringDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string email)
        => _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await _context.Users.AddAsync(user, cancellationToken);

    #region Tutor

    public async Task<Tutor?> GetTutorByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _context.Users.OfType<Tutor>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    #endregion

    #region Student

    public async Task<Student?> GetStudentByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _context.Users.OfType<Student>()
            .Include(x => x.Subjects)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<Competence?> GetCompetenceByIdAsync(CompetenceId competenceId, CancellationToken cancellationToken = default)
        => _context.CompetencesGroups
            .SelectMany(x => x.Competences)
            .FirstOrDefaultAsync(x => x.Id == competenceId, cancellationToken);

    #endregion
}