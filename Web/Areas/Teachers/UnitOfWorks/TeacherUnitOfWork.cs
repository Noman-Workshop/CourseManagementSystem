using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.UnitOfWorks;

public class TeacherUnitOfWork : ITeacherUnitOfWork {
	public TeacherUnitOfWork(
		DbContext context,
		ITeacherRepository teacherRepository,
		IAddressRepository addressRepository
	) : base(context, teacherRepository, addressRepository) {
	}
}
