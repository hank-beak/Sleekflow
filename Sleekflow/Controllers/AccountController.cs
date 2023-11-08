using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sleekflow.Models;

namespace Sleekflow.Controllers
{
	[Route("api/account")]
	public class AccountController: ControllerBase
	{
		private readonly UsersDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UsersDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUser user)
		{

			var applicationUser = new ApplicationUser { UserName = user.UserName };
			
			var result = await _userManager.CreateAsync(applicationUser, user.Password);

			// Store the user in the database
			if (result.Succeeded)
			{
				_context.Users.Add(applicationUser);
				_context.SaveChanges();
			}
			else
			{
				return BadRequest("Error registering user");
			}

			return Ok("User registered");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUser loggingInUser)
		{
			var result = await _signInManager.PasswordSignInAsync(loggingInUser.UserName, loggingInUser.Password, false,lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return Ok("User logged in");
			}

			return BadRequest("Error loggin in");
		}
	}
}
