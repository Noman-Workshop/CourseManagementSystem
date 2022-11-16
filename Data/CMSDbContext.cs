using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Auth.Models;
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
	public DbSet<Role> Roles { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Department>().HasKey(department => new { department.Id, department.Name });
		modelBuilder.Entity<Department>().HasIndex(department => department.Name).IsUnique();

		modelBuilder.Entity<Enrollment>().HasKey(enrollment => new { enrollment.CourseId, enrollment.StudentId });

		modelBuilder.Entity<Role>().HasKey(role => new { role.Id, role.Name });
		modelBuilder.Entity<Role>().HasIndex(role => role.Name).IsUnique();

		SeedData(modelBuilder);
	}

	private void SeedData(ModelBuilder modelBuilder) {
	}
}
