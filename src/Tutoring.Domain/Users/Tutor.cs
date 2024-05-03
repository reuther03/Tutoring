﻿using Tutoring.Common.Exceptions.Domain;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public sealed class Tutor : User
{
    private readonly List<CompetenceId> _competenceIds = [];

    public IReadOnlyList<CompetenceId> CompetenceIds => _competenceIds.AsReadOnly();

    private Tutor()
    {
    }

    private Tutor(UserId id, DateTime? archivedAt, bool isArchived, Email email, Name firstname, Name lastname, Password password)
        : base(id, archivedAt, isArchived, email, firstname, lastname, password, Role.Tutor)
    {
    }

    public static Tutor Create(Email email, Name firstname, Name lastname, Password password)
    {
        var tutor = new Tutor(UserId.New(), null, false, email, firstname, lastname, password);
        return tutor;
    }

    public void AddCompetence(CompetenceId competenceId)
    {
        if (_competenceIds.Contains(competenceId))
        {
            throw new DomainException("Competence already added.");
        }

        _competenceIds.Add(competenceId);
    }
}