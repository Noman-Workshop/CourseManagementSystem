using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.UnitOfWorks;

public class TeacherUnitOfWork : UnitOfWork, ITeacherUnitOfWork {
	public TeacherUnitOfWork(
		DbContext context,
		ITeacherRepository teacherRepository,
		IAddressRepository addressRepository
	) : base(context) {
		TeacherRepository = teacherRepository;
		AddressRepository = addressRepository;
	}

	public ITeacherRepository TeacherRepository { get; }
	public IAddressRepository AddressRepository { get; }
}
