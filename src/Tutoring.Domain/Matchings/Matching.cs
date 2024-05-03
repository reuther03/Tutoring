using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Matchings;

public sealed class Matching : AggregateRoot<Guid>, IArchivable
{
    public Name? CompetencesGroupName { get; private set; }
    public CompetenceId CompetenceId { get; private set; } = null!;

    public UserId StudentId { get; private set; } = null!;
    public User Student { get; private set; } = null!;

    public UserId TutorId { get; private set; } = null!;
    public User Tutor { get; private set; } = null!;

    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }

    private Matching()
    {
    }

    private Matching(Guid id, Name? competencesGroupName, CompetenceId competenceId, UserId studentId, UserId tutorId)
        : base(id)
    {
        CompetencesGroupName = competencesGroupName;
        CompetenceId = competenceId;
        StudentId = studentId;
        TutorId = tutorId;
    }

    public static Matching Create(Name? competencesGroupName, CompetenceId competenceId, UserId studentId, UserId tutorId)
    {
        return new Matching(Guid.NewGuid(), competencesGroupName, competenceId, studentId, tutorId);
    }

    public void SetArchiveData(bool isArchived, DateTime? archivedAt)
    {
        IsArchived = isArchived;
        ArchivedAt = archivedAt;
    }
}