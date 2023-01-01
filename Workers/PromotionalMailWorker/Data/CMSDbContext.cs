using Microsoft.EntityFrameworkCore;
using Models;

namespace CourseManagementSystem.Data;

public class CMSDbContext : DbContext {
	public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options) {
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Department> Departments { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public DbSet<Enrollment> Enrollments { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<Budget> Budgets { get; set; }
	public DbSet<BudgetAuditLog> BudgetAuditLogs { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>(entity => { entity.HasIndex(user => user.Email).IsUnique(); });

		modelBuilder.Entity<BudgetAuditLog>()
			.HasOne(log => log.CreatedBy)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Department>().HasKey(department => new { department.Id, department.Name });
		modelBuilder.Entity<Department>().HasIndex(department => department.Name).IsUnique();

		modelBuilder.Entity<Enrollment>().HasKey(enrollment => new { enrollment.CourseId, enrollment.StudentId });

		modelBuilder.Entity<Role>().HasIndex(role => role.Name).IsUnique();

		SeedData(modelBuilder);
	}

	private void SeedData(ModelBuilder modelBuilder) {
	}
}
