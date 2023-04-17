using System.Net;
using System.Security.Claims;
using CourseManagementSystem.Extensions;
using DTOs.Leaves;
using DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Models;
using Services.Leaves;
using Services.Leaves.LeaveRequests;
using Services.Mappers;

namespace CourseManagementSystem.Areas.Leaves.Controllers;

[Area("Leaves")]
[Authorize]
public class HomeController : Controller {
	private readonly ILeaveRequestService _leaveRequestService;
	private readonly IMapperService _mapperService;
	private readonly IDistributedCache _cache;

	public HomeController(
		ILeaveRequestService leaveRequestService,
		IMapperService mapperService,
		IDistributedCache cache,
		ILeaveRequestRepository leaveRequestRepository
	) {
		_leaveRequestService = leaveRequestService;
		_mapperService = mapperService;
		_cache = cache;
	}

	[HttpGet]
	public async Task<IActionResult> Create() {
		var userEmail = User.FindFirstValue(ClaimTypes.Email);
		var user = await _cache.Get<UserDto>(userEmail);
		var applicableLeaveTypes = await _leaveRequestService.GetLeaveTypes(user!.Id);
		var model = new LeaveRequestDto {
			EmployeeId = user.Id,
			EmployeeCode = Math.Abs(user.Id.GetHashCode()),
			EmployeeName = user.FirstName + " " + user.LastName,
			LeaveTypes = applicableLeaveTypes.Select(x => _mapperService.Map<LeaveType, LeaveTypeDropdownDto>(x))
		};
		return View(model);
	}

	[HttpGet]
	public async Task<LeaveDaysInfoDto> GetDaysInfo(Guid leaveTypeId) {
		var userEmail = User.FindFirstValue(ClaimTypes.Email);
		var user = await _cache.Get<UserDto>(userEmail);
		var leaveType = await _leaveRequestService.GetLeaveType(leaveTypeId);

		var totalAvailableDays = await _leaveRequestService.GetTotalAvailableDays(user!.Id, leaveTypeId);
		var totalTakenDays = await _leaveRequestService.GetTotalUsedDays(user.Id, leaveTypeId);
		var daysInfo = new LeaveDaysInfoDto {
			TotalAvailableDays = totalAvailableDays,
			TotalTakenDays = totalTakenDays,
			RemainingDays = totalAvailableDays - totalTakenDays,
			MaxDaysInOneGo = leaveType.MaxDaysInOneGo
		};
		return daysInfo;
	}

	public async Task<int> CalculateDays(Guid leaveTypeId, DateTime startDate, DateTime endDate) {
		var userEmail = User.FindFirstValue(ClaimTypes.Email);
		var user = await _cache.Get<UserDto>(userEmail);

		return await _leaveRequestService.CalculateDays(user!.Id, leaveTypeId, startDate, endDate);
	}

	[HttpPost]
	public async Task Create(LeaveRequestDto request) {
		// get the requesting user
		var userEmail = User.FindFirstValue(ClaimTypes.Email);
		var user = await _cache.Get<UserDto>(userEmail);
		await _leaveRequestService.CreateLeaveRequest(user.Id, request);
	}
}
