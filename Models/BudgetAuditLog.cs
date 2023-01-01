using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class BudgetAuditLog {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	[Required]
	public DateTime CreatedAt { get; set; }

	[Required]
	public User CreatedBy { get; set; }

	[Required]
	public Budget Budget { get; set; }
}
