using System.ComponentModel.DataAnnotations;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Addresses.Services;

public class AddressesServices {
	private readonly CMSDbContext _context;

	public AddressesServices(CMSDbContext context) {
		_context = context;
	}

	public async Task<List<Address?>> GetAddresses() {
		return await _context.Addresses.ToListAsync();
	}

	public async Task<Address?> GetAddress(string id) {
		return await _context.Addresses.FindAsync(id);
	}

	public async Task<Address> CreateAddress(Address address) {
		var isValid = Validator.TryValidateObject(address, new ValidationContext(address), null, true);
		if (!isValid) {
			throw new ArgumentException();
		}

		_context.Addresses.Add(address);
		await _context.SaveChangesAsync();
		return address;
	}

	public async Task<Address?> UpdateAddress(int id, Address address) {
		var isValid = Validator.TryValidateObject(address, new ValidationContext(address), null, true);
		if (!isValid) {
			return null;
		}

		_context.Entry(address).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return address;
	}

	public async void DeleteAddress(int id) {
		var address = await _context.Addresses.FindAsync(id);
		if (address == null) {
			return;
		}

		_context.Addresses.Remove(address);
		await _context.SaveChangesAsync();
	}
}
