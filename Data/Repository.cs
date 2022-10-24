using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Data;

public abstract class Repository<T> : IRepository<T> where T : class {
	private readonly DbContext _context;

	protected Repository(DbContext context) {
		_context = context;
	}

	public Task<List<T>> FindAll() => _context.Set<T>().ToListAsync();

	public ValueTask<T?> FindById(int id) => _context.Set<T>().FindAsync(id);

	public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);

	public void Add(T entity) => _context.Set<T>().AddAsync(entity);

	public void Update(T entity) => _context.Set<T>().Update(entity);

	public void Remove(T entity) => _context.Set<T>().Remove(entity);
}
