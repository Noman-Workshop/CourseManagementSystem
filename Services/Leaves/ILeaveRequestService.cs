using DTOs.Leaves;
using Models;
using Services.Common;

namespace Services.Leaves;

public interface ILeaveRequestService : IService<LeaveRequest, Guid> {
	public Task<LeaveType> GetLeaveType(Guid id);
	public Task<List<LeaveType>> GetLeaveTypes();
	Task<List<Models.LeaveType>> GetLeaveTypes(Guid employeeId);
	Task<int> GetTotalAvailableDays(Guid employeeId, Guid leaveTypeId);
	Task<int> GetTotalUsedDays(Guid employeeId, Guid leaveTypeId);
	Task<int> CalculateDays(Guid employeeId, Guid leaveTypeId, DateTime startDate, DateTime endDate);
	Task<List<LeaveRequest>> GetLeaveRequests(Guid employeeId);
	Task<List<LeaveRequest>> GetLeaveRequests(Guid employeeId, DateTime startDate, DateTime endDate);
	Task<List<LeaveRequest>> GetLeaveRequests(Guid employeeId, DateTime startDate, DateTime endDate, Guid leaveTypeId);
	Task CreateLeaveRequest(Guid employeeId, LeaveRequestDto leaveRequest);
}
