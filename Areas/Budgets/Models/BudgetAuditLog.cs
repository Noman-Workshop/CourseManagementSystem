using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseManagementSystem.Areas.Auth.Models;

namespace CourseManagementSystem.Areas.Budgets.Models;

public class BudgetAuditLog {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	[Required]
	public DateTime CreatedAt { get; set; }

	[Required]
	public DateTime UpdatedAt { get; set; }

	[Required]
	public User CreatedBy { get; set; }

	[Required]
	public User UpdatedBy { get; set; }

}
