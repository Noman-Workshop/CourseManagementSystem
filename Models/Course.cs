using System.ComponentModel.DataAnnotations;

namespace Models;

public class Course {
	[Key]
	[StringLength(10, MinimumLength = 3)]
	public string Id { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string Name { get; set; }

	[StringLength(2048, MinimumLength = 10)]
	public string Description { get; set; }

	[Range(1, 5)]
	public float Credits { get; set; }

	public virtual Department Department { get; set; }

	public virtual ICollection<Teacher> Teachers { get; set; }

	public virtual ICollection<Enrollment> Enrollments { get; set; }
}
