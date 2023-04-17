using System.Linq.Expressions;
using DTOs.Leaves;
using Models;
using Models.Constants;
using Services.Employees;
using Services.Leaves.LeaveCarriedRemaining;
using Services.Leaves.LeaveRequestReviews;
using Services.Leaves.LeaveRequests;
using Services.Leaves.LeaveTypes;
using Services.Users;

namespace Services.Leaves;

class LeaveRequestService : ILeaveRequestService {
	private readonly ILeaveTypeRepository _leaveTypeRepository;
	private readonly ILeaveRequestRepository _leaveRequestRepository;
	private readonly ILeaveCarriedRemaining _leaveCarriedRemaining;
	private readonly IEmployeeService _employeeService;
	private readonly ICalenderRepository _calenderRepository;
	private readonly IUserService _userService;
	private readonly ILeaveRequestReviewRepository _leaveRequestReviewRepository;

	public LeaveRequestService(
		ILeaveTypeRepository leaveTypeRepository,
		IEmployeeService employeeService,
		ILeaveRequestRepository leaveRequestRepository,
		ICalenderRepository calenderRepository,
		ILeaveCarriedRemaining leaveCarriedRemaining,
		IUserService userService,
		ILeaveRequestReviewRepository leaveRequestReviewRepository
	) {
		_leaveTypeRepository = leaveTypeRepository;
		_employeeService = employeeService;
		_leaveRequestRepository = leaveRequestRepository;
		_calenderRepository = calenderRepository;
		_leaveCarriedRemaining = leaveCarriedRemaining;
		_userService = userService;
		_leaveRequestReviewRepository = leaveRequestReviewRepository;
	}

	public Task<List<LeaveRequest>> Find() => throw new NotImplementedException();

	public ValueTask<LeaveRequest> Find(Guid id) => throw new NotImplementedException();

	public Task<List<LeaveRequest>> Find(Expression<Func<LeaveRequest, bool>> condition, string includeAttributes) =>
		throw new NotImplementedException();

	public Task<List<LeaveRequest>> Find(
		Expression<Func<LeaveRequest, bool>> condition,
		Expression<Func<LeaveRequest, object>> includeAttributes
	) => throw new NotImplementedException();

	public Task Add(LeaveRequest entity) => throw new NotImplementedException();

	public Task Update(LeaveRequest entity) => throw new NotImplementedException();

	public Task Delete(LeaveRequest entity) => throw new NotImplementedException();

	public Task<bool> Exists(Guid id) => throw new NotImplementedException();

	public Task<List<LeaveType>> GetLeaveTypes() {
		return _leaveTypeRepository.Find();
	}

	private static string[] ToGenderConstraints(string gender) {
		return gender switch {
			"male"   => new[] { "male", "all" },
			"female" => new[] { "female", "all" },
			_        => Array.Empty<string>()
		};
	}

	public async Task<List<LeaveType>> GetLeaveTypes(Guid employeeId) {
		var employee = (await _employeeService.Find(emp => emp.Id == employeeId, "LevelNavigation,IdNavigation"))[0];
		var serviceDuration = (DateTime.Now - employee.JoinDate)!.Value.Days;
		var genderConstraints = ToGenderConstraints(employee.IdNavigation.Gender);
		var employeeRank = employee.LevelNavigation.Rank;
		var leaveTypes =
			await _leaveTypeRepository.Find(
				type => serviceDuration > type.MinServiceDays
						&& employeeRank > type.MinEmployeeLevelRank
						&& genderConstraints.Contains(type.GenderConstraint),
				"");
		var filteredLeaveTypes = new List<LeaveType>();
		// find distinct leave types for every type of leave
		foreach (var leaveType in leaveTypes) {
			var type = filteredLeaveTypes.Find(type => type.ShortName == leaveType.ShortName);
			if (type == null) {
				filteredLeaveTypes.Add(leaveType);
				continue;
			}

			var isGreaterLeaveType = leaveType.MinServiceDays > type.MinServiceDays
									|| leaveType.MinEmployeeLevelRank > type.MinEmployeeLevelRank;
			if (isGreaterLeaveType) {
				filteredLeaveTypes.Remove(type);
				filteredLeaveTypes.Add(leaveType);
			}
		}

		return filteredLeaveTypes;
	}

	public async Task<int> GetTotalAvailableDays(Guid employeeId, Guid leaveTypeId) {
		var leaveType = await _leaveTypeRepository.FindFirst(type => type.Id == leaveTypeId);

		// fixed in a lifetime
		if (leaveType.NoOfDaysAllocatedPerYear == 0) {
			return leaveType.MaxDaysInOneGo * leaveType.NoOfTimesRedeemable;
		}

		// fixed in a year and not forwarded
		if (!leaveType.IsBalanceForwarded) {
			return leaveType.NoOfDaysAllocatedPerYear;
		}

		// fixed in a year and forwarded
		if (leaveType.NoOfDaysAllocatedPerYear != 0 && leaveType.IsBalanceForwarded) {
			var leaveCarriedRemaining = (await _leaveCarriedRemaining.Find(
											remaining => remaining.EmployeeId == employeeId
														&& remaining.LeaveTypeShortName
														== leaveType.ShortName, ""))[0];

			var totalAvailableDays = leaveCarriedRemaining.CarriedOver;
			if (leaveCarriedRemaining.LastAddedDate.Year == DateTime.Now.Year) {
				totalAvailableDays += leaveCarriedRemaining.LastAddedDays;
			}

			return totalAvailableDays;
		}

		throw new ArgumentException("Invalid leave type");
	}

	public async Task<int> GetTotalUsedDays(Guid employeeId, Guid leaveTypeId) {
		var leaveType = await _leaveTypeRepository.FindFirst(type => type.Id == leaveTypeId);

		// fixed in a lifetime
		if (leaveType.NoOfDaysAllocatedPerYear == 0) {
			var requestedLeaves = await _leaveRequestRepository.Find(
									request => request.EmployeeId == employeeId
												&& request.Type == leaveTypeId
												&& request.IsApproved == true
									, ""
								);

			return requestedLeaves.Sum(request => request.ActualDays);
		}

		// fixed in a year and not forwarded
		if (!leaveType.IsBalanceForwarded) {
			var requestedLeaves = await _leaveRequestRepository.Find(
									request => request.EmployeeId == employeeId
												&& request.Type == leaveTypeId
												&& request.IsApproved == true
												&& request.ActualStartDate.Year == DateTime.Now.Year
									, ""
								);

			return requestedLeaves.Sum(request => request.ActualDays);
		}

		// fixed in a year and forwarded
		// TODO: optimize using get total available used days
		if (leaveType.NoOfDaysAllocatedPerYear != 0 && leaveType.IsBalanceForwarded) {
			var leaveCarriedRemaining = (await _leaveCarriedRemaining.Find(
											remaining => remaining.EmployeeId == employeeId
														&& remaining.LeaveTypeShortName
														== leaveType.ShortName, ""))[0];

			var totalAvailableDays = leaveCarriedRemaining.CarriedOver;
			if (leaveCarriedRemaining.LastAddedDate.Year == DateTime.Now.Year) {
				totalAvailableDays += leaveCarriedRemaining.LastAddedDays;
			}

			return totalAvailableDays - leaveCarriedRemaining.RemainingDays;
		}

		throw new ArgumentException("Invalid leave type");
	}

	public async Task<int> CalculateDays(Guid employeeId, Guid leaveTypeId, DateTime startDate, DateTime endDate) {
		int totalDays = (endDate - startDate).Days + 1;
		int noOfWeekends = (await _calenderRepository.Find(
								calender => calender.Day >= startDate
											&& calender.Day <= endDate
											&& calender.Type == CalenderDayType.WEEKEND
								, ""
							)).Count;

		int noOfWorkDays = (await _calenderRepository.Find(
								calender => calender.Day >= startDate
											&& calender.Day <= endDate
											&& calender.Type == CalenderDayType.WORKDAY
								, ""
							)).Count;

		int noOfHolidays = totalDays - noOfWeekends - noOfWorkDays;

		var leaveType = await _leaveTypeRepository.FindFirst(type => type.Id == leaveTypeId);

		int calculatedDays = totalDays;
		if (!leaveType.IsWeekendIncluded) {
			calculatedDays -= noOfWeekends;
		}

		if (!leaveType.IsHolidayIncluded) {
			calculatedDays -= noOfHolidays;
		}

		return calculatedDays;
	}

	public Task<List<LeaveRequest>> GetLeaveRequests(Guid employeeId) => throw new NotImplementedException();

	public Task<List<LeaveRequest>> GetLeaveRequests(Guid employeeId, DateTime startDate, DateTime endDate) =>
		throw new NotImplementedException();

	public Task<List<LeaveRequest>> GetLeaveRequests(
		Guid employeeId,
		DateTime startDate,
		DateTime endDate,
		Guid leaveTypeId
	) => throw new NotImplementedException();

	public async Task CreateLeaveRequest(Guid employeeId, LeaveRequestDto request) {
		// check the leave type
		var leaveType = await GetLeaveType(request.LeaveTypeId);
		// calculate the no of days he has already taken and remaining days of that leave type
		var totalAvailableDays =
			await GetTotalAvailableDays(employeeId, request.LeaveTypeId);
		var totalUsedDays = await GetTotalUsedDays(employeeId, request.LeaveTypeId);
		// check if the proposed days are less than remaining days
		request.ProposedStartDate = Convert.ToDateTime(request.ProposedDateRange.Split(" - ")[0]);
		request.ProposedEndDate = Convert.ToDateTime(request.ProposedDateRange.Split(" - ")[1]);
		var proposedDays = await CalculateDays(employeeId, request.LeaveTypeId,
								request.ProposedStartDate, request.ProposedEndDate);
		if (proposedDays > (totalAvailableDays - totalUsedDays)) {
			throw new ArgumentException("Proposed days are more than remaining days");
		}

		// get user
		var user = await _userService.Find(employeeId);

		// create the leave request
		var leaveRequest = new LeaveRequest {
			EmployeeId = employeeId,
			Type = request.LeaveTypeId,
			ProposedStartDate = request.ProposedStartDate,
			ProposedEndDate = request.ProposedEndDate,
			ProposedDays = request.ProposedDays,
			ActualStartDate = request.ProposedStartDate,
			ActualEndDate = request.ProposedEndDate,
			ActualDays = request.ProposedDays,
			Purpose = request.Purpose,
			AddressId = user.AddressId,
			IsApproved = false
		};

		leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
		await _leaveRequestRepository.CommitAsync();

		// create leave request reviews for the leave request
		var leaveRequestReviews = new List<LeaveRequestReview>();
		var reviewDepth = leaveType.ReviewForwardDepth;
		var currentEmployeeId = user.Id;
		var order = 0;
		while (reviewDepth-- > 0) {
			var supervisorId = (await _employeeService.Find(currentEmployeeId)).SupervisorId;
			if (supervisorId == null) {
				break;
			}

			var leaveRequestReview = new LeaveRequestReview {
				RequestId = leaveRequest.Id,
				ReviewerId = supervisorId.Value,
				Order = ++order,
				IsApproved = false,
				PermittedStartDate = request.ProposedStartDate,
				PermittedEndDate = request.ProposedEndDate,
				PermittedDays = request.ProposedDays,
				Comment = ""
			};

			leaveRequestReviews.Add(leaveRequestReview);
		}

		// insert the leave request into the database
		_leaveRequestReviewRepository.AddRange(leaveRequestReviews);
		await _leaveRequestReviewRepository.CommitAsync();
	}

	public Task<LeaveType> GetLeaveType(Guid id) {
		return _leaveTypeRepository.FindFirst(type => type.Id == id);
	}
}
