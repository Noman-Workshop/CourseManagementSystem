using Microsoft.EntityFrameworkCore;
using Models;
using Services.Common;

namespace Services.Addresses.Repository;

public abstract class IAddressRepository : Repository<Address, string> {
	protected IAddressRepository(DbContext context) : base(context) {
	}
}
