using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseManagementSystem.Areas.Courses.Models;
using CourseManagementSystem.Areas.Teachers.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Areas.Departments.Models;

public class Department {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	[Key]
	[StringLength(10, MinimumLength = 3)]
	public string Name { get; set; }

	public Teacher Head { get; set; }
	public virtual ICollection<Course> Courses { get; set; }
}
