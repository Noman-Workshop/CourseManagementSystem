using System.Linq.Expressions;

namespace Services.Common;

public interface IService<TEntity, TKey> {
	Task<List<TEntity>> Find();
	ValueTask<TEntity> Find(TKey id);
	public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, string includeAttributes);
	public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> includeAttributes);

	Task Add(TEntity entity);
	Task Update(TEntity entity);
	Task Delete(TEntity entity);
	Task<bool> Exists(TKey id);
}
