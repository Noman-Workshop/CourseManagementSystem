using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Data;

public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class {
	private readonly DbContext _context;

	protected Repository(DbContext context) {
		_context = context;
	}

	public Task<List<TEntity>> Find() => _context.Set<TEntity>().ToListAsync();

	public ValueTask<TEntity?> Find(TKey id) => _context.Set<TEntity>().FindAsync(id);

	public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> condition, string? includeAttributes) {
		IQueryable<TEntity> query = _context.Set<TEntity>();
		if (string.IsNullOrEmpty(includeAttributes)) {
			return query.Where(condition).ToListAsync();
		}

		string[] attributes = includeAttributes.Split(new[] { ',' },
			StringSplitOptions.RemoveEmptyEntries);
		typeof(TEntity).GetProperties().Where(p => attributes.Contains(p.Name))
			.ToList().ForEach(p => query = query.Include(p.Name));
		return query.Where(condition).ToListAsync();
	}

	// find all entities with paging
	public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> condition, string? includeAttributes = null) {
		List<TEntity> entities = await Find(condition, includeAttributes);
		if (entities.Count == 0) {
			throw new KeyNotFoundException();
		}

		return entities[0];
	}

	public void Add(TEntity entity) => _context.Set<TEntity>().AddAsync(entity);

	public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

	public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);

	public async Task CommitAsync() => await _context.SaveChangesAsync();
}
