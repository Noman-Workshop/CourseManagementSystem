using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Courses.Models;
using CourseManagementSystem.Areas.Departments.Models;
using Microsoft.EntityFrameworkCore;
using CourseManagementSystem.Models;
using CourseManagementSystem.Areas.Students.Models;
using CourseManagementSystem.Areas.Teachers.Models;

namespace CourseManagementSystem.Data;

public class CMSDbContext : DbContext {
	public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options) {
	}

	public DbSet<Course> Courses { get; set; }
	public DbSet<Department> Departments { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }

	public DbSet<Address> Addresses { get; set; }

	public DbSet<Enrollment> Enrollments { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Department>().HasKey(d => new { d.Id, d.Name });
		modelBuilder.Entity<Enrollment>().HasKey(e => new { e.CourseId, e.StudentId });
		SeedData(modelBuilder);
	}

	private void SeedData(ModelBuilder modelBuilder) {
	}
}
