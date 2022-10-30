using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Addresses.Repository;

public abstract class IAddressRepository : Repository<Address, string> {
	protected IAddressRepository(DbContext context) : base(context) {
	}
}
