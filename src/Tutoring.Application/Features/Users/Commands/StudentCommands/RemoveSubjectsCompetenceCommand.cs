﻿using MediatR;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.Users.Commands.StudentCommands;

public record RemoveSubjectsCompetenceCommand(Guid SubjectId, Guid CompetenceId) : ICommand
{
    internal sealed class Handler : ICommandHandler<RemoveSubjectsCompetenceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserContext userContext, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveSubjectsCompetenceCommand request, CancellationToken cancellationToken)
        {
            if (!_userContext.IsAuthenticated)
                return Result<Unit>.Unauthorized("User is not authenticated");

            if (_userContext.Role != Role.Student)
                return Result<Unit>.Unauthorized("User is not a student");

            var student = await _userRepository.GetStudentByIdAsync(_userContext.UserId, cancellationToken);
            if (student is null)
                return Result<Unit>.BadRequest("Student not found");

            var subjects = await _userRepository.GetSubjectByIdAsync(request.SubjectId, cancellationToken);
            if (subjects is null)
                return Result<Unit>.BadRequest("Subject not found");


            subjects.RemoveCompetence(request.CompetenceId);

            var result = await _unitOfWork.CommitAsync(cancellationToken);
            return result.Map(Unit.Value);
        }
    }
}