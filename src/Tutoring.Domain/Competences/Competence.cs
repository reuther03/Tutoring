﻿using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Competences;

public class Competence : Entity<CompetenceId>
{
    public Name DetailedName { get; private set; }
    public Description Description { get; private set; }

    private Competence()
    {
    }

    private Competence(CompetenceId id, Name detailedName, Description description) : base(id)
    {
        DetailedName = detailedName;
        Description = description;
    }

    public static Competence Create(Name detailedName, Description description)
    {
        var competence = new Competence(CompetenceId.New(), detailedName, description);
        return competence;
    }
}