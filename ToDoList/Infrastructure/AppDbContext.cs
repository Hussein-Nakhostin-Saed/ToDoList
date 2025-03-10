using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True\r\n");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //if (System.Diagnostics.Debugger.IsAttached == false)
        //{
        //	System.Diagnostics.Debugger.Launch();
        //}
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
