using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public DbSet<Domain.Entities.TaskItem> Tasks { get; set; }
    public DbSet<WorkLog> WorkLogs { get; set; }
}
