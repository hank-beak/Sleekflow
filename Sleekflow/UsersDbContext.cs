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
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<ApplicationUser>().ToTable("Users");
			modelBuilder.Entity<ApplicationUser>(e =>
			{
				e.Property(e => e.UserName).HasColumnName("UserName");
				e.Property(e => e.Id).HasColumnName("Id");
				e.Property(e => e.Role).HasColumnName("Role");
				e.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
			});
			/*
			var userProperties = typeof(ApplicationUser).GetProperties();

			// Ignore specific properties you don't want to map
			var propertiesToMap =  typeof(RegisterUser).GetProperties().Select(p => p.Name) ;

			foreach (var property in userProperties)
			{
				if (!propertiesToMap.Contains(property.Name))
				{
					modelBuilder.Entity<ApplicationUser>().Ignore(property.Name);
				}
			}*/
		}
	}
}
