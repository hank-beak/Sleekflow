using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sleekflow.Models
{
	public class RegisterUser
	{
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
