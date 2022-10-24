using System.Linq.Expressions;

namespace CourseManagementSystem.Data;

public interface IRepository<T> {
	Task<List<T>> FindAll();
	ValueTask<T?> FindById(int id);
	IQueryable<T> Find(Expression<Func<T, bool>> predicate);
	void Add(T entity);
	void Update(T entity);
	void Remove(T entity);
}