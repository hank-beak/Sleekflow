using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sleekflow.Interfaces;
using Sleekflow.Models;
using System.Security.Claims;
using Sleekflow.ViewModels;

namespace Sleekflow.Controllers
{
    [Route("api/account")]
	public class AccountController: ControllerBase
	{
		private readonly IDbUserRepo _dbUserRepo;

		public AccountController(IDbUserRepo dbUserRepo)
		{
			_dbUserRepo = dbUserRepo;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterViewModel user)
		{
			var foundUser = _dbUserRepo.GetUserByName(user.UserName);
			if(foundUser != null) 
			{
				return BadRequest($"Username already created/existed. Registration unsuccessful");
			}
			var applicationUser = new ApplicationUser { UserName = user.UserName, Role = user.Role, PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password) };
			_dbUserRepo.CreateUser(applicationUser);

			return Ok("User registered successfully");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel loggingInUser)
		{
			var foundUser = loggingInUser.Id > 0 ? _dbUserRepo.GetUserById(loggingInUser.Id) : _dbUserRepo.GetUserByName(loggingInUser.UserName);
			if(foundUser != null && BCrypt.Net.BCrypt.Verify(loggingInUser.Password, foundUser.PasswordHash))
			{
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name, loggingInUser.UserName),
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				return Ok("User logged in");	
			}

			return Unauthorized("Error loggin in");
		}
	}
}
