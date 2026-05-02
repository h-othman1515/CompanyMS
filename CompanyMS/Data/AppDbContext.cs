using CompanyMS.Models;
using Microsoft.EntityFrameworkCore;
using CompanyMS.Models;

namespace CompanyMS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Manager> Managers => Set<Manager>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<EmployeeDepartment> EmployeeDepartments => Set<EmployeeDepartment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite PK for junction table
        modelBuilder.Entity<EmployeeDepartment>()
            .HasKey(ed => new { ed.EmployeeId, ed.DepartmentId });

        modelBuilder.Entity<EmployeeDepartment>()
            .HasOne(ed => ed.Employee)
            .WithMany(e => e.EmployeeDepartments)
            .HasForeignKey(ed => ed.EmployeeId);

        modelBuilder.Entity<EmployeeDepartment>()
            .HasOne(ed => ed.Department)
            .WithMany(d => d.EmployeeDepartments)
            .HasForeignKey(ed => ed.DepartmentId);

        // Unique constraint on NationalId
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.NationalId)
            .IsUnique();
    }
}