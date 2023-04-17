using System.Linq.Expressions;

namespace Services.Common;

public interface IRepository<TEntity, TKey> {
	Task<List<TEntity>> Find();
	ValueTask<TEntity> Find(TKey id);
	Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, string? includeAttributes = null);

	public Task<List<TEntity>> Find(
		Expression<Func<TEntity, bool>> condition,
		Expression<Func<TEntity, object>> includeAttributes
	);

	Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> condition, string? includeAttributes = null);
	ValueTask<TEntity> Add(TEntity entity);
	void AddRange(IEnumerable<TEntity> entities);
	void Update(TEntity entity);
	void Remove(TEntity entity);
}
