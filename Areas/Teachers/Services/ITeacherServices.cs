using CourseManagementSystem.Areas.Teachers.Models;

namespace CourseManagementSystem.Areas.Teachers.Services;

public interface ITeacherServices {
	// Get All Teachers
	Task<List<Teacher>> GetAll();

	// Get Teacher By Id
	Task<Teacher> GetById(int id);

	// Create Teacher
	Task<Teacher> Create(Teacher teacher);

	// Update Teacher
	Task<Teacher> Update(Teacher teacher);

	// Delete Teacher
	Task<Teacher> Delete(int id);

	// Exist Teacher
	Task<bool> Exist(int id);
}

