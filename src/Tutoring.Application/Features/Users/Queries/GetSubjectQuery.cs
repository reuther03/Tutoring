using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Queries;

public record GetSubjectQuery(Guid SubjectId) : IQuery<SubjectDto>
{
    internal sealed class Handler : IQueryHandler<GetSubjectQuery, SubjectDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public Handler(IUserRepository userRepository, IUserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Result<SubjectDto>> Handle(GetSubjectQuery query, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _userRepository.GetStudentByIdAsync(userId, cancellationToken);

            if (user is null)
                Result.NotFound<SubjectDto>("User not found");

            var subject = user?.Subjects.FirstOrDefault(x => x.Id == query.SubjectId);

            return subject is null
                ? Result.NotFound<SubjectDto>("Subject not found")
                : Result.Ok(SubjectDto.AsDto(subject));
        }
    }
}