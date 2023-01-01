using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Services.Addresses.Repository;
using Services.Teachers.Repositories;

namespace Services.Teachers.UnitOfWorks;

public abstract class ITeacherUnitOfWork : UnitOfWork {
	public ITeacherRepository TeacherRepository { get; }
	public IAddressRepository AddressRepository { get; }

	protected ITeacherUnitOfWork(
		DbContext context,
		ITeacherRepository teacherRepository,
		IAddressRepository addressRepository
	) : base(context) {
		TeacherRepository = teacherRepository;
		AddressRepository = addressRepository;
	}
}
