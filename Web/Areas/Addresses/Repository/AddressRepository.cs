using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Addresses.Repository;

public class AddressRepository : IAddressRepository {
	public AddressRepository(DbContext context) : base(context) {
	}
}
