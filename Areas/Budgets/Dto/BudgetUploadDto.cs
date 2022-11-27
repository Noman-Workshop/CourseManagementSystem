using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Areas.Budgets.Dto;

public class BudgetUploadDto {
	[Required]
	public IFormFile File { get; set; }

	[Required]
	public bool IsEditable { get; set; }

	public DateTime? EditDeadline { get; set; }
}
