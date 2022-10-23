using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Models;

public class Course {
	[Key]
	public string Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }
	[Required]
	public float Credits { get; set; }
}
