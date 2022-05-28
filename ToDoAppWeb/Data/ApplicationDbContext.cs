using Microsoft.EntityFrameworkCore;
using ToDoAppWeb.Models;

namespace ToDoAppWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<TaskToDo> TasksToDo { get; set; }
        public DbSet<Priority> Priority { get; set; }
    }
}
