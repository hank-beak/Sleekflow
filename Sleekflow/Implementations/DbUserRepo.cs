using Sleekflow.Interfaces;
using Sleekflow.DbContexts;
using Sleekflow.Models.DTOs;

namespace Sleekflow.Implementations
{
    public class DbUserRepo: IDbUserRepo
	{
		private readonly UsersDbContext _context;

		public DbUserRepo(UsersDbContext context) 
		{ 
			_context = context;
		}

		public ApplicationUserDTO GetUserById(int id)
		{
			return _context.Users.FirstOrDefault(u => u.Id == id);
		}

		public ApplicationUserDTO GetUserByName(string userName)
		{
			return _context.Users.FirstOrDefault(u => u.UserName.Equals(userName));
		}

		public ApplicationUserDTO CreateUser(ApplicationUserDTO user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();

			return user;
		}
	}
}
