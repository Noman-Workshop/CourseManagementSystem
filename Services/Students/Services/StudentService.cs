using System.Linq.Expressions;
using Models;
using Services.Addresses.Repository;
using Services.Students.Repositories;
using Services.Students.UnitOfWorks;

namespace Services.Students.Services;

public class StudentService : IStudentService {
	private readonly IAddressRepository _addressRepository;
	private readonly IStudentRepository _studentRepository;
	private readonly IStudentUnitOfWork _studentUnitOfWork;

	public StudentService(IStudentUnitOfWork studentUnitOfWork) {
		_studentUnitOfWork = studentUnitOfWork;
		_studentRepository = studentUnitOfWork.StudentRepository;
		_addressRepository = studentUnitOfWork.AddressRepository;
	}

	public Task<List<Student>> Find() => _studentRepository.Find();

	public async ValueTask<Student> Find(string id) =>
		await _studentRepository.Find(id) ?? throw new ArgumentException();

	public Task<List<Student>> Find(Expression<Func<Student, bool>> condition, string includeAttributes) =>
		_studentRepository.Find(condition, includeAttributes);

	public Task Add(Student student) {
		_studentRepository.Add(student);
		return _studentUnitOfWork.CommitAsync();
	}

	public Task Update(Student student) {
		_studentRepository.Update(student);
		return _studentUnitOfWork.CommitAsync();
	}

	public Task Delete(Student student) {
		_studentRepository.Remove(student);
		_addressRepository.Remove(student.Address);
		return _studentUnitOfWork.CommitAsync();
	}

	public Task<bool> Exists(string id) => _studentRepository.Find(id).AsTask().ContinueWith(t => t.Result != null);
}
