using System.Linq.Expressions;
using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Teachers.Models;

namespace CourseManagementSystem.Data;

public interface IService<TEntity, TKey> {
	Task<List<TEntity>> Find();
	ValueTask<TEntity> Find(TKey id);
	public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, string includeAttributes);
	Task Add(TEntity entity);
	Task Update(TEntity entity);
	Task Delete(TEntity entity);
	Task<bool> Exists(TKey id);
}
