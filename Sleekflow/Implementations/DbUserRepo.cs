using Sleekflow.Interfaces;
using Sleekflow.Models;

namespace Sleekflow.Implementations
{
	public class DbUserRepo: IDbUserRepo
	{
		private readonly UsersDbContext _context;

		public DbUserRepo(UsersDbContext context) 
		{ 
			_context = context;
		}

		public ApplicationUser GetUserById(int id)
		{
			return _context.Users.FirstOrDefault(u => u.Id == id);
		}

		public ApplicationUser GetUserByName(string userName)
		{
			return _context.Users.FirstOrDefault(u => u.UserName.Equals(userName));
		}

		public ApplicationUser CreateUser(ApplicationUser user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();

			return user;
		}
	}
}
