using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Courses.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Areas.Teachers.Models;

public class Teacher {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string Name { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	[ValidateNever]
	public virtual Address Address { get; set; }

	[ValidateNever]
	public virtual ICollection<Course> Courses { get; set; }
}
