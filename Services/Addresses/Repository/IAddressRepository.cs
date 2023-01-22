using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Addresses.Repository;

public abstract class IAddressRepository : Repository<Address, string> {
	protected IAddressRepository(DbContext context) : base(context) {
	}
}