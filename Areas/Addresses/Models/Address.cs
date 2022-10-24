using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Models;

public class Address {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	[Range(1000, 9999, ErrorMessage = "Zip code must be 4 digits")]
	public string ZipCode { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string Street { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string House { get; set; }
}
