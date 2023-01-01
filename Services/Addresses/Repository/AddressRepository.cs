using Microsoft.EntityFrameworkCore;

namespace Services.Addresses.Repository;

public class AddressRepository : IAddressRepository {
	public AddressRepository(DbContext context) : base(context) {
	}
}
