using System.Linq.Expressions;
using Models;
using Services.Departments.Repositories;

namespace CourseManagementSystem.Areas.Departments.Services;

public class DepartmentService : IDepartmentService {
	private readonly IDepartmentRepository _departmentRepository;

	public DepartmentService(IDepartmentRepository departmentRepository) {
		_departmentRepository = departmentRepository;
	}

	public Task<List<Department>> Find() => _departmentRepository.Find();

	public async ValueTask<Department> Find(string id) =>
		await _departmentRepository.Find(id) ?? throw new ArgumentException();

	public async Task<Department> Find(string id, string name) {
		var department =
			await _departmentRepository.FindFirst(
				department => department.Id == id && department.Name == name,
				"Head");
		return department ?? throw new ArgumentException();
	}

	public Task<List<Department>> Find(Expression<Func<Department, bool>> condition, string includeAttributes) =>
		_departmentRepository.Find(condition, includeAttributes);

	public Task Add(Department department) {
		_departmentRepository.Add(department);
		return _departmentRepository.CommitAsync();
	}

	public Task Update(Department department) {
		_departmentRepository.Update(department);
		return _departmentRepository.CommitAsync();
	}

	public Task Delete(Department department) {
		_departmentRepository.Remove(department);
		return _departmentRepository.CommitAsync();
	}

	public Task<bool> Exists(string id) => _departmentRepository.Find(id).AsTask().ContinueWith(t => t.Result != null);
}
