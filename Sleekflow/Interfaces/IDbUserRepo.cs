using Sleekflow.Models.DTOs;

namespace Sleekflow.Interfaces
{
    public interface IDbUserRepo
	{
		ApplicationUserDTO GetUserById(int id);
		ApplicationUserDTO GetUserByName(string userName);
		ApplicationUserDTO CreateUser(ApplicationUserDTO user);
	}
}
