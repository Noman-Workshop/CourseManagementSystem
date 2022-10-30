using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.UnitOfWorks;

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
