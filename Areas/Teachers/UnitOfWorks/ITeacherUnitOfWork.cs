using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Data;

namespace CourseManagementSystem.Areas.Teachers.UnitOfWorks;

public interface ITeacherUnitOfWork : IUnitOfWork {
	public ITeacherRepository TeacherRepository { get; }
	public IAddressRepository AddressRepository { get; }
}
