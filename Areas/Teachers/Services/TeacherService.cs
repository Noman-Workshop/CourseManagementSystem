using System.Linq.Expressions;
using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Areas.Teachers.UnitOfWorks;

namespace CourseManagementSystem.Areas.Teachers.Services;

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
