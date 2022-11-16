using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseManagementSystem.Areas.Auth.Models;

public class User {
	[Key]
	[EmailAddress]
	public string Email { get; set; }

	[PasswordPropertyText]
	public string Password { get; set; }

	[ValidateNever]
	public virtual Role[] Roles { get; set; }
}
