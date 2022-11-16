using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Areas.Auth.Models;

public class Role {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	[Key]
	[StringLength(50, MinimumLength = 3)]
	public string Name { get; set; }
}
