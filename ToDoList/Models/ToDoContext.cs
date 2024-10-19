using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Models.Status { StatusId = "to do", Name = "To do" },
                new Models.Status { StatusId = "in progress", Name = "In Progress" },
                new Models.Status { StatusId = "quality assurance", Name = "Quality Assurance" },
                new Models.Status { StatusId = "done", Name = "Done" });
        }
    }
}
