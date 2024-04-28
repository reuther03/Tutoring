using System.Linq.Expressions;

namespace Tutoring.Common.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Filters a sequence of values based on a predicate if a condition is met.
    /// </summary>
    /// <param name="query">The <see cref="IQueryable{T}"/> to filter.</param>
    /// <param name="condition">The condition to be evaluated.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
    /// <returns>An <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }
}