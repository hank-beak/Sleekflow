using Sleekflow.Models;

namespace Sleekflow.Interfaces
{
	public interface IDbUserRepo
	{
		ApplicationUser GetUserById(int id);
		ApplicationUser GetUserByName(string userName);
		ApplicationUser CreateUser(ApplicationUser user);
	}
}
