namespace Shared;

public class PaginatedResult<TEntity>
{
    public PaginatedResult(int pageindex, int pagesize, int count, IEnumerable<TEntity> data)
    {
        PageIndex = pageindex;
        PageSize = pagesize;
        Count = count;
        Data = data;
    }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<TEntity> Data { get; set; }
}
