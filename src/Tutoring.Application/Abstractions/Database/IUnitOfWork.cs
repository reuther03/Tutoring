﻿using Tutoring.Common.Primitives;

namespace Tutoring.Application.Abstractions.Database;

public interface IUnitOfWork
{
    Task<Result<bool>> CommitAsync(CancellationToken cancellationToken = default);
}