using Microsoft.EntityFrameworkCore;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Data;

public class CMSDbContext : DbContext {
	public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options) {
	}
	public DbSet<Course> Course { get; set; }
}
