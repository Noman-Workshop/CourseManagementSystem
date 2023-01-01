using System.Linq.Expressions;

namespace CourseManagementSystem.Data;

public interface IRepository<TEntity, TKey> {
	Task<List<TEntity>> Find();
	ValueTask<TEntity?> Find(TKey id);
	Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, string? includeAttributes = null);
	Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> condition, string? includeAttributes = null);
	void Add(TEntity entity);
	void AddRange(IEnumerable<TEntity> entities);
	void Update(TEntity entity);
	void Remove(TEntity entity);
}
