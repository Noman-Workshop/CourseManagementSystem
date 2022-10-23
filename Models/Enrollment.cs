using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseManagementSystem.Areas.Courses.Models;
using CourseManagementSystem.Areas.Students.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Models;

public class Enrollment {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	[Key]
	public string StudentId { get; set; }

	public virtual Student Student { get; set; }

	[Key]
	public string CourseId { get; set; }

	public virtual Course Course { get; set; }

	[DisplayFormat(NullDisplayText = "No grade")]
	public Grade? Grade { get; set; }
}
