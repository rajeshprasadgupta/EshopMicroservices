namespace BuildingBlocks.Pagination;
public class PaginatedResult<TEntity>
	(int pageindex, int pageSize, long count, IEnumerable<TEntity> data) 
	where TEntity : class
{
	public int PageIndex { get; } = pageindex; 
	public int PageSize { get; } = pageSize;
	public long PageCount { get; } = count;
	public IEnumerable<TEntity> Data { get; } = data;
}