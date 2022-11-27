using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CourseManagementSystem.Areas.Auth.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Areas.Users.Models;

public class User {
	[Key]
	[EmailAddress]
	public string Email { get; set; }

	[PasswordPropertyText]
	public string Password { get; set; }

	[ValidateNever]
	public virtual ICollection<Role> Roles { get; set; }
}
