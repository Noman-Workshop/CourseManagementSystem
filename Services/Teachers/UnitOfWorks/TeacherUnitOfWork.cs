using Microsoft.EntityFrameworkCore;
using Services.Addresses.Repository;
using Services.Teachers.Repositories;

namespace Services.Teachers.UnitOfWorks;

public class TeacherUnitOfWork : ITeacherUnitOfWork {
	public TeacherUnitOfWork(
		DbContext context,
		ITeacherRepository teacherRepository,
		IAddressRepository addressRepository
	) : base(context, teacherRepository, addressRepository) {
	}
}
