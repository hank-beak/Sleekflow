using Microsoft.EntityFrameworkCore;
using Sleekflow.Models.DTOs;

namespace Sleekflow.DbContexts
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public DbSet<ApplicationUserDTO> Users { get; set; }

    }
}
