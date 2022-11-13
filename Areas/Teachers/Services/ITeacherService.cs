using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models.Table;

namespace CourseManagementSystem.Areas.Teachers.Services;

public interface ITeacherService : IService<Teacher, string> {
	Task<PagedResponse<Teacher>> Find(JqueryDatatableParam param);
}
