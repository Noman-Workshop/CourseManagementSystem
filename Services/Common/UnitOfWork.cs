using Microsoft.EntityFrameworkCore;

namespace Services.Common;

public class UnitOfWork : IUnitOfWork {
	private readonly DbContext _context;

	protected UnitOfWork(DbContext context) {
		_context = context;
	}

	public async void Dispose() {
		await _context.DisposeAsync();
	}

	public Task CommitAsync() {
		return _context.SaveChangesAsync();
	}
}