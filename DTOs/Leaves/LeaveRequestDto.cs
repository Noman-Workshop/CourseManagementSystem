using Microsoft.AspNetCore.Http;

namespace DTOs.Leaves;

public class LeaveRequestDto {
	public Guid EmployeeId { get; set; }
	public int EmployeeCode { get; set; }
	public string EmployeeName { get; set; }
	public IEnumerable<LeaveTypeDropdownDto> LeaveTypes { get; set; }
	public Guid LeaveTypeId { get; set; }
	public DateTime ProposedStartDate { get; set; }
	public DateTime ProposedEndDate { get; set; }
	public string ProposedDateRange { get; set; }
	public int ProposedDays { get; set; }
	public string Purpose { get; set; }
	public string Address { get; set; }
	public IFormFile Attachment { get; set; }
}
