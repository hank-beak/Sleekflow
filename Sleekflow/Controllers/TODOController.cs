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

		/// <summary>
		/// Gets a list of all <see cref="Todo"/>
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="sort"></param>
		/// <returns>A list of <see cref="Todo"/></returns>
		/// <response code="200">Returns a list of Todos</response>
		[AllowAnonymous]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
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

		/// <summary>
		/// Gets the <see cref="Todo"/> that matches the id
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     GET /api/Todos/1
		///     
		/// </remarks>
		/// <param name="id"></param>
		/// <returns></returns>
		[AllowAnonymous]
        [HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
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


		/// <summary>
		/// Creates a new <see cref="Todo"/> item
		/// </summary>
		/// <param name="todo"></param>
		/// <remarks>
		/// Sample request:
		///
		///     POST /api/Todos
		///     {
		///			"name": "Do your laundry",
		///			"dueDate": "2023-11-20T12:30:00",
		///			"description": "You're out of clothes. Do you laundry.",
		///			"status": "NotStarted"
		///     }
		///
		/// </remarks>
		/// <returns>A newly created <see cref="Todo"/></returns>
		/// <response code="201">Returns the newly created item</response>
		/// <response code="400">If the JSON is invalid</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateTodo([FromBody]Todo todo)
		{
			try
			{
				if (!ModelState.IsValid)
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

		/// <summary>
		/// Updates the existing <see cref="Todo"/> if it exists
		/// </summary>
		/// <param name="id"></param>
		/// <param name="todo"></param>
		/// <remarks>
		/// Sample request:
		///
		///     PUT /api/Todos/1
		///     {
		///			"name": "Did my laundry",
		///			"dueDate": "2023-11-18T12:30:00",
		///			"description": "You're out of clothes. Do you laundry.",
		///			"status": "NotStarted"
		///     }
		///
		/// </remarks>
		/// <returns>The updated <see cref="Todo"/></returns>
		/// <response code="200">Returns the updated item</response>
		/// <response code="400">If the JSON is invalid</response>
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

		/// <summary>
		/// Deletes the <see cref="Todo"/> that matches the id
		/// </summary>
		/// <param name="id"></param>
		/// <remarks>
		/// Sample request:
		///
		///     DELETE /api/todos/1
		///
		/// </remarks>
		/// <returns>Nothing</returns>
		/// <response code="204">Returns nothing since its deleted</response>
		/// <response code="404">If the id that needs to be deleted does not exist</response>
		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteTodo(int id)
		{
			try
			{
				var deleted = _todoService.DeleteTodo(id);
				if (!deleted)
				{
					return NotFound("Nothing to delete");
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
