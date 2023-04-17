using AutoMapper;
using DTOs.Calender;
using DTOs.Leaves;
using DTOs.User;
using Models;

namespace Services.Mappers;

public class MapperConfigurations {
	public MapperConfiguration Instance { get; }

	public MapperConfigurations() {
		Instance = new MapperConfiguration(cfg => {
												UserToDto(cfg);
												LeaveTypeToDropdownDto(cfg);
												CalenderToDto(cfg);
											});
	}

	private void UserToDto(IMapperConfigurationExpression cfg) {
		cfg.CreateMap<User, UserDto>()
			.ForMember(dto => dto.Id, opt => opt.MapFrom(user => user.Id))
			.ForMember(dto => dto.Email, opt => opt.MapFrom(user => user.Email))
			.ForMember(dto => dto.FirstName, opt => opt.MapFrom(user => user.FirstName))
			.ForMember(dto => dto.LastName, opt => opt.MapFrom(user => user.LastName))
			.ForMember(dto => dto.Gender, opt => opt.MapFrom(user => user.Gender))
			.ForMember(dto => dto.AddressId, opt => opt.MapFrom(user => user.AddressId))
			.ForMember(dto => dto.Password, opt => opt.Ignore())
			.ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(user => user.CreatedAt))
			.ForMember(dto => dto.UpdatedAt, opt => opt.MapFrom(user => user.UpdatedAt));
	}

	private void LeaveTypeToDropdownDto(IMapperConfigurationExpression cfg) {
		cfg.CreateMap<LeaveType, LeaveTypeDropdownDto>()
			.ForMember(dto => dto.Id, opt => opt.MapFrom(leaveType => leaveType.Id))
			.ForMember(dto => dto.Name, opt => opt.MapFrom(leaveType => leaveType.FullName));
	}

	private void CalenderToDto(IMapperConfigurationExpression cfg) {
		cfg.CreateMap<Models.Calender, CalendarDayDto>()
			.ForMember(dto => dto.Day, opt => opt.MapFrom(calender => calender.Day))
			.ForMember(dto => dto.Type, opt => opt.MapFrom(calender => calender.Type));
	}
}
