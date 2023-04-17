namespace DTOs.Leaves;

public class LeaveDaysInfoDto {
	public int TotalAvailableDays { get; set; }
	public int TotalTakenDays { get; set; }
	public int RemainingDays { get; set; }
	public int MaxDaysInOneGo { get; set; }
}
