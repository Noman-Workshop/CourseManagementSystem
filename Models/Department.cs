using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models;

public class Department {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[ValidateNever]
	public string Id { get; set; }

	// unique key for the department
	[Key]
	[StringLength(10, MinimumLength = 3)]
	public string Name { get; set; }

	public Teacher Head { get; set; }

	[ValidateNever]
	public virtual ICollection<Course> Courses { get; set; }
}
