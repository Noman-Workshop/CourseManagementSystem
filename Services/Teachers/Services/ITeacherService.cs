using CourseManagementSystem.Data;
using CourseManagementSystem.ViewTables;
using Models;

namespace Services.Teachers.Services;

public interface ITeacherService : IService<Teacher, string> {
	Task<PagedResponse<Teacher>> Find(JqueryDatatableParam param);
}
