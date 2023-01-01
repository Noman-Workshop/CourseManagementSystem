using System.Linq.Expressions;
using CourseManagementSystem.ViewTables;
using Models;
using Services.Addresses.Repository;
using Services.Teachers.Repositories;
using Services.Teachers.UnitOfWorks;

namespace Services.Teachers.Services;

public class TeacherService : ITeacherService {
	private readonly IAddressRepository _addressRepository;
	private readonly ITeacherRepository _teacherRepository;
	private readonly ITeacherUnitOfWork _teacherUnitOfWork;

	public TeacherService(ITeacherUnitOfWork teacherUnitOfWork) {
		_teacherUnitOfWork = teacherUnitOfWork;
		_teacherRepository = teacherUnitOfWork.TeacherRepository;
		_addressRepository = teacherUnitOfWork.AddressRepository;
	}

	public Task<List<Teacher>> Find() => _teacherRepository.Find();

	public async ValueTask<Teacher> Find(string id) =>
		await _teacherRepository.Find(id) ?? throw new ArgumentException();

	public Task<List<Teacher>> Find(Expression<Func<Teacher, bool>> condition, string includeAttributes) =>
		_teacherRepository.Find(condition, includeAttributes);

	public async Task<PagedResponse<Teacher>> Find(JqueryDatatableParam param) {
		List<Teacher> teachers = await Find();
		if (param.sSearch != null) {
			teachers = teachers.Where(t => t.Name.ToLower().Contains(param.sSearch)
											|| t.Email.ToLower().Contains(param.sSearch)).ToList();
		}

		var sortedTeachers = teachers.OrderBy(t => t.Name);
		int sortColumnIndex = param.iSortCol_0;
		string sortDirection = param.sSortDir_0;
		sortedTeachers = sortColumnIndex switch {
			0 => sortDirection == "asc"
					? sortedTeachers.OrderBy(t => t.Name)
					: sortedTeachers.OrderByDescending(t => t.Name),
			1 => sortDirection == "asc"
					? sortedTeachers.OrderBy(t => t.Email)
					: sortedTeachers.OrderByDescending(t => t.Email),
			_ => sortedTeachers
		};

		List<Teacher> displayedTeachers = sortedTeachers.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
		return new PagedResponse<Teacher>() {
			data = displayedTeachers,
			totalRecords = teachers.Count,
			pageSize = displayedTeachers.Count
		};
	}

	public Task Add(Teacher teacher) {
		_teacherRepository.Add(teacher);
		return _teacherUnitOfWork.CommitAsync();
	}

	public Task Update(Teacher teacher) {
		_teacherRepository.Update(teacher);
		return _teacherUnitOfWork.CommitAsync();
	}

	public Task Delete(Teacher teacher) {
		_teacherRepository.Remove(teacher);
		_addressRepository.Remove(teacher.Address);
		return _teacherUnitOfWork.CommitAsync();
	}

	public Task<bool> Exists(string id) => _teacherRepository.Find(id).AsTask().ContinueWith(t => t.Result != null);
}
