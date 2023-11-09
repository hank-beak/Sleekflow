using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sleekflow.Models;
using System.Collections.Generic;

namespace Sleekflow
{
    public class UsersDbContext: DbContext
	{
		public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

		public DbSet<ApplicationUser> Users { get; set; }
		
	}
}
