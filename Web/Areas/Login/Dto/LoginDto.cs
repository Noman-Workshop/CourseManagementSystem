using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Areas.Login.Dto;

public class LoginDto {
	[Required]
	[StringLength(50, MinimumLength = 3)]
	public string UserEmail { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 8)]
	public string Password { get; set; }
}
