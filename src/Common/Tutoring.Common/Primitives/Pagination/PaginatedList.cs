namespace Tutoring.Common.Primitives.Pagination;

public class PaginatedList<T>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;
    public List<T> Items { get; private set; }

    public PaginatedList(int page, int pageSize, int totalCount, List<T> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public static Result<PaginatedList<T>> Create(int page, int pageSize, int totalCount, List<T> items)
    {
        //todo: tu zmienilem zwracany typ na Result<PaginatedList<T>>
        var result =  new PaginatedList<T>(page, pageSize, totalCount, items);
        return Result<PaginatedList<T>>.Ok(result);
    }
}