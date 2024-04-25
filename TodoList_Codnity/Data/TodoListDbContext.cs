using Microsoft.EntityFrameworkCore;
using TodoList_Codnity.Models;

namespace TodoList_Codnity.Data
{
    public class TodoListDbContext : DbContext
    {
        public IConfiguration _context { get; set; }
        public TodoListDbContext(IConfiguration context)
        {
            _context = context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_context.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<TodoList> Lists { get; set; }
    }
}
