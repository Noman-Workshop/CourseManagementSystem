using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models;

public class Budget {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Currency { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public decimal FinalAmount { get; set; }

	[Required]
	public DateTime StartDate { get; set; }

	[Required]
	public DateTime EndDate { get; set; }

	[Required]
	public Department Department { get; set; }

	[ValidateNever]
	public ICollection<BudgetAuditLog> AuditLogs { get; set; }

	[Required]
	public DateTime? EditDeadline { get; set; }
}
