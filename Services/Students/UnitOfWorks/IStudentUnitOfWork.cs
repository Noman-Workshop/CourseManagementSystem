using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Services.Addresses.Repository;
using Services.Students.Repositories;

namespace Services.Students.UnitOfWorks;

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
