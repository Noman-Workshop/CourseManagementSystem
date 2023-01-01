namespace CourseManagementSystem.Data;

public interface IUnitOfWork : IDisposable {
	public Task CommitAsync();
}