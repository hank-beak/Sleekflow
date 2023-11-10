using Microsoft.EntityFrameworkCore;
using Sleekflow.Models.DTOs;

namespace Sleekflow.DbContexts
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoDTO> Todos { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
    }
}
