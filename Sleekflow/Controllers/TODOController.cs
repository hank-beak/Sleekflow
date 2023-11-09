using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sleekflow.Implementations;
using Sleekflow.Models;

namespace Sleekflow.Controllers
{
	[Authorize]
    [Route("api/todos")]
	[ApiController]
	public class TodoController: ControllerBase
	{
		private readonly TodoService _todoService;

		public TodoController(TodoService todoService)
		{
			_todoService = todoService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult GetTodos([FromQuery] TodoFilter filter, [FromQuery] TodoSort sort)
		{
			try
			{
				var todos = _todoService.GetTodos(filter, sort);
				return Ok(todos);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
			try
			{
				var todo = _todoService.GetTodoById(id);
				if (todo == null)
				{
					return NotFound();
				}
				return Ok(todo);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
        }

		
        [HttpPost]
		public IActionResult CreateTodo([FromBody]Todo todo)
		{
			try
			{
				if (!HttpContext.User.Identity.IsAuthenticated || !ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var created = _todoService.CreateTodo(todo);
				return CreatedAtAction("CreateTodo", created);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		
		[HttpPut("{id}")]
		public IActionResult UpdateTodo(int id,[FromBody] Todo todo)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var updated = _todoService.UpdateTodo(id, todo);

				return Ok(updated);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		public IActionResult DeleteTodo(int id)
		{
			try
			{
				var deleted = _todoService.DeleteTodo(id);
				if (!deleted)
				{
					return NotFound();
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
