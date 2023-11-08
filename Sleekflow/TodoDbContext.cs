using Microsoft.EntityFrameworkCore;
using Sleekflow.Models;

namespace Sleekflow
{
	public class TodoDbContext : DbContext
	{
		public DbSet<Todo> Todos { get; set; }
		
		public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
	}
}
