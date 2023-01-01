using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Students.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Students.UnitOfWorks;

public class StudentUnitOfWork : IStudentUnitOfWork {
	public StudentUnitOfWork(
		DbContext context,
		IStudentRepository studentRepository,
		IAddressRepository addressRepository
	) : base(context, studentRepository, addressRepository) {
	}
}
