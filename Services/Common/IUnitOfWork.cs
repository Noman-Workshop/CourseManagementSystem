namespace Services.Common;

public interface IUnitOfWork : IDisposable {
	public Task CommitAsync();
}