using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTOs.Budgets;

public class BudgetUploadDto {
	[Required]
	public IFormFile File { get; set; }

	[Required]
	public bool IsEditable { get; set; }

	public DateTime? EditDeadline { get; set; }
}
