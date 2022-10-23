using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Models;

public class Enrollment {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	public string StudentId { get; set; }
	public virtual Student Student { get; set; }

	public string CourseId { get; set; }
	public virtual Course Course { get; set; }
}
