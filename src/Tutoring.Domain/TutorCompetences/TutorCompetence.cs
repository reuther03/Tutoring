using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.TutorCompetences;

public class TutorCompetence : Entity<Guid>
{
    public Guid TutorId { get; private set; }
    public Guid CompetenceId { get; private set; }

    public TutorCompetence(Guid id, Guid tutorId, Guid competenceId) : base(id)
    {
        TutorId = tutorId;
        CompetenceId = competenceId;
    }
}