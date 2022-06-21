using BlazorServer.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServer.Context
{
    public partial class BlazorServerDBContext : DbContext
    {

        public BlazorServerDBContext(DbContextOptions<BlazorServerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeesBlazor> EmployeesBlazors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesBlazor>(entity =>
            {
                entity.ToTable("Employees_Blazor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
