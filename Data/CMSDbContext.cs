using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Data;

public class CMSDbContext : DbContext {
	public CMSDbContext(DbContextOptions options) : base(options) {
	}
}
