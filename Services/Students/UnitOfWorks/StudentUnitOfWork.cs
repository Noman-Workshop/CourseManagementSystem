using Microsoft.EntityFrameworkCore;
using Services.Addresses.Repository;
using Services.Students.Repositories;

namespace Services.Students.UnitOfWorks;

public class StudentUnitOfWork : IStudentUnitOfWork {
	public StudentUnitOfWork(
		DbContext context,
		IStudentRepository studentRepository,
		IAddressRepository addressRepository
	) : base(context, studentRepository, addressRepository) {
	}
}
