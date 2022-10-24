using System.ComponentModel.DataAnnotations;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Addresses.Services;

public class AddressServices : IAddressServices {
	private readonly CMSDbContext _context;

	public AddressServices(CMSDbContext context) {
		_context = context;
	}

	public async Task<List<Address>> GetAll() => await _context.Addresses.ToListAsync();

	public async Task<Address> GetById(string id) =>
		await _context.Addresses.FindAsync(id) ?? throw new ArgumentException();

	public async Task<Address> Create(Address address) {
		bool isValid = Validator.TryValidateObject(address, new ValidationContext(address), null, true);
		if (!isValid) {
			throw new ArgumentException();
		}

		_context.Addresses.Add(address);
		await _context.SaveChangesAsync();
		return address;
	}

	public async Task<Address> Update(Address address) {
		bool isValid = Validator.TryValidateObject(address, new ValidationContext(address), null, true);
		if (!isValid) {
			throw new ArgumentException();
		}

		_context.Entry(address).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return address;
	}

	public async Task Delete(string id) {
		Address address = await GetById(id);
		_context.Addresses.Remove(address);
		await _context.SaveChangesAsync();
	}

	public bool Exists(string id) => _context.Addresses.Any(e => e.Id == id);
}
