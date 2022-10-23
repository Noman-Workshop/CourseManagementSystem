using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace CourseManagementSystem.Models;

public class Address {
	[Range(1000, 9999, ErrorMessage = "Zip code must be 4 digits")]
	public string ZipCode { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string Street { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string house { get; set; }
}
