using System.ComponentModel.DataAnnotations;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Areas.Students.Models;

public class Student {
	[Key]
	public string Id { get; set; }

	[StringLength(10, MinimumLength = 3)]
	public string Name { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	public Address Address { get; set; }

	public virtual ICollection<Enrollment> Enrollments { get; set; }
}
