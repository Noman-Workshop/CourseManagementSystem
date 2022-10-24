using CourseManagementSystem.Models;

namespace CourseManagementSystem.Areas.Addresses.Services;

public interface IAddressServices {
	public Task<List<Address>> GetAll();
	public Task<Address> GetById(string id);
	public Task<Address> Create(Address address);
	public Task<Address> Update(Address address);
	public Task Delete(string id);
	public bool Exists(string id);
}
