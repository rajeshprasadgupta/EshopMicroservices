namespace BuildingBlocks.Pagination;
public class PaginatedResult<TEntity>
	(int pageindex, int pageSize, long totalCount, IEnumerable<TEntity> data) 
	where TEntity : class
{
	public int PageIndex { get; } = pageindex; 
	public int PageSize { get; } = pageSize;
	public long TotalCount { get; } = totalCount;
	public IEnumerable<TEntity> Data { get; } = data;
}