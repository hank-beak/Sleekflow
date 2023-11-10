using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sleekflow.Interfaces;
using System.Security.Claims;
using Sleekflow.Models.ViewModels;
using Sleekflow.Models;
using Sleekflow.Models.DTOs;

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

		/// <summary>
		/// Registers a user 
		/// </summary>
		/// <param name="user"></param>
		/// <remarks>
		/// Sample request:
		/// 
		///		POST /api/account/register
		///		
		///		{
		///			"userName": "hank",
		///			"password": "Password123++",
		///			"role": "Admin"
		///		}
		///		
		/// </remarks>
		/// <returns>A newly created <see cref="ApplicationUserDTO"/></returns>
		/// <response code="200">Returns the newly created item</response>
		/// <response code="400">If the username/account already exists</response>
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Register([FromBody] RegisterViewModel user)
		{
			var foundUser = _dbUserRepo.GetUserByName(user.UserName);
			if(foundUser != null) 
			{
				return BadRequest($"Username already created/existed. Registration unsuccessful");
			}
			var applicationUser = new ApplicationUserDTO { UserName = user.UserName, Role = user.Role, PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password) };
			_dbUserRepo.CreateUser(applicationUser);

			return Ok("User registered successfully");
		}

		/// <summary>
		/// Logs in the user
		/// </summary>
		/// <param name="loggingInUser"></param>
		/// <remarks>
		/// Sample request:
		/// 
		///		POST /api/account/login
		///		
		///		{
		///			"userName": "hank",
		///			"password": "Password123++",
		///			"role": "Admin"
		///		}
		///		
		/// </remarks>
		/// <returns>Nothing</returns>
		/// <response code="200">Returns the newly created item</response>
		/// <response code="401">If the credentials don't match</response>
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
