using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sleekflow
{
	public class ApplicationUser: IdentityUser
	{
		public int Id { get; set; }
		public override string UserName { get; set; }
		public override string PasswordHash { get; set; }
		public string Role { get; set; }
	}
}
