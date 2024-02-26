using employee_system.Models;
using Microsoft.EntityFrameworkCore;



namespace pos_employee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().
                HasMany(x=>x.DepartmentEmployees).
                WithOne(y => y.Employee)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Department>().
                HasMany(x => x.DepartmentEmployees).
                WithOne(y => y.Department)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
