using System.Linq.Expressions;
using Models;

namespace Services.Employees;

class EmployeeService : IEmployeeService {
	private readonly IEmployeeRepository _repository;

	public EmployeeService(IEmployeeRepository repository) {
		_repository = repository;
	}

	public Task<List<Employee>> Find() => throw new NotImplementedException();

	public ValueTask<Employee> Find(Guid id) {
		return _repository.Find(id);
	}

	public Task<List<Employee>> Find(Expression<Func<Employee, bool>> condition, string includeAttributes) {
		return _repository.Find(condition, includeAttributes);
	}

	public Task<List<Employee>> Find(
		Expression<Func<Employee, bool>> condition,
		Expression<Func<Employee, object>> includeAttributes
	) {
		return _repository.Find(condition, includeAttributes);
	}

	public Task Add(Employee entity) => throw new NotImplementedException();

	public Task Update(Employee entity) => throw new NotImplementedException();

	public Task Delete(Employee entity) => throw new NotImplementedException();

	public Task<bool> Exists(Guid id) => throw new NotImplementedException();
}
