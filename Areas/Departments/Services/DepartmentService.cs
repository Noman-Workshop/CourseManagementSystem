using System.Linq.Expressions;
using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Areas.Departments.Repositories;

namespace CourseManagementSystem.Areas.Departments.Services;

public class DepartmentService : IDepartmentService {
	private readonly IDepartmentRepository _departmentRepository;

	public DepartmentService(IDepartmentRepository departmentRepository) {
		_departmentRepository = departmentRepository;
	}

	public Task<List<Department>> Find() => _departmentRepository.Find();

	public async ValueTask<Department> Find(string id) =>
		await _departmentRepository.Find(id) ?? throw new ArgumentException();

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
