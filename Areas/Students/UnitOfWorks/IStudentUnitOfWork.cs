using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Students.Repositories;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Students.UnitOfWorks;

public abstract class IStudentUnitOfWork : UnitOfWork {
	public IStudentRepository StudentRepository { get; }
	public IAddressRepository AddressRepository { get; }

	protected IStudentUnitOfWork(
		DbContext context,
		IStudentRepository studentRepository,
		IAddressRepository addressRepository
	) : base(context) {
		StudentRepository = studentRepository;
		AddressRepository = addressRepository;
	}
}
