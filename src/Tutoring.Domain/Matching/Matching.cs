using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Matching;

public class Matching : AggregateRoot<Guid>
{
    public Name? CompetencesGroupName { get; private set; }
    public CompetenceId CompetenceId { get; private set; }

    public UserId StudentId { get; private set; }
    public virtual User Student { get; private set; }

    public UserId TutorId { get; private set; }
    public virtual User Tutor { get; private set; }


    private Matching()
    {
    }

    public Matching(Guid id, Name? competencesGroupName, CompetenceId competenceId, UserId studentId, UserId tutorId)
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
}