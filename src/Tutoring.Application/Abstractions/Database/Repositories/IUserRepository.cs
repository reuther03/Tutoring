using Tutoring.Domain.Reviews;
using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Abstractions.Database.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(UserId? id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    void RemoveUser(User user);

    #region Tutor

    Task<Tutor?> GetTutorByIdAsync(UserId id, CancellationToken cancellationToken = default);

    #endregion

    #region Student

    //todo: null na id bo sie swieci a blad wyrzuci wczesnije z controllera
    Task<Student?> GetStudentByIdAsync(UserId? id, CancellationToken cancellationToken = default);

    #endregion

    #region Subjects
    Task<Subject?> GetSubjectByIdAsync(Guid subjectId, CancellationToken cancellationToken = default);
    void RemoveSubject(Subject subject);

    #endregion

    Task<Review?> GetReviewByIdAsync(Guid reviewId, CancellationToken cancellationToken = default);
    void RemoveReview(Review review);

}