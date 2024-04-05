using Tutoring.Application.Abstractions.Database;
using Tutoring.Common.Primitives;

namespace Tutoring.Infrastructure.Database.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly TutoringDbContext _tutoringDbContext;

    public UnitOfWork(TutoringDbContext tutoringDbContext)
    {
        _tutoringDbContext = tutoringDbContext;
    }

    public async Task<Result<bool>> CommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await _tutoringDbContext.SaveChangesAsync(cancellationToken) > 0;
        return result ? Result.Ok(true) : Result.InternalServerError<bool>("Failed to save changes");
    }
}