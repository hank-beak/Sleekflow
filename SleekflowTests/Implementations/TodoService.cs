using Moq;
using Sleekflow.Implementations;
using Sleekflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sleekflow.Models.DTOs;

namespace SleekflowTests.Implementations
{
    public class TodoServiceTests
	{
		private TodoDTO _todoSample1 = new TodoDTO()
		{
			Name = "Test",
			Description = "Test",
			DueDate = DateTime.Now.AddDays(1),
			Status = "NotStarted"
		};

		private TodoDTO _todoSample2 = new TodoDTO()
		{
			Name = "Test",
			Description = "Test",
			DueDate = DateTime.Now.AddDays(1),
			Status = "NotStarted"
		};

		[Fact]	
		public void CreateTodo_ReturnsTodo()
		{
			
			var createdTodo = _todoSample1;
			createdTodo.Id = 2;
			
			var mockRepo = new Mock<IDbTodoRepo>();
			mockRepo.Setup(r => r.Create(It.IsAny<TodoDTO>())).Returns(createdTodo);

			var service = new TodoService(mockRepo.Object);
			var result = service.CreateTodo(_todoSample1);

			Assert.Equal(createdTodo.Id, result.Id);
		}

		[Fact]
		public void UpdateTodo_ReturnsTodo()
		{
			var mockRepo = new Mock<IDbTodoRepo>();
			mockRepo.Setup(r => r.Update(It.IsAny<TodoDTO>()));

			var service = new TodoService(mockRepo.Object);
			var result = service.UpdateTodo(1, _todoSample1);
			Assert.Equal(_todoSample1, result);
		}

		[Fact]
		public void DeleteTodo_ReturnsBool()
		{
			var mockRepo = new Mock<IDbTodoRepo>();
			mockRepo.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);

			var service = new TodoService(mockRepo.Object);
			var result = service.DeleteTodo(1);
			Assert.True(result);
		}

		[Fact]
		public void GetTodoById_ReturnsTodo()
		{
			var mockRepo = new Mock<IDbTodoRepo>();
			mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(_todoSample1);

			var service = new TodoService(mockRepo.Object);
			var result = service.GetTodoById(1);
			Assert.Equal(_todoSample1, result);
		}

		[Fact]
		public void GetTodos_ReturnsListOfTodos()
		{
			var list = new List<TodoDTO> { _todoSample1, _todoSample2 };
			var mockRepo = new Mock<IDbTodoRepo>();
			mockRepo.Setup(r => r.GetTodos(null, null)).Returns(new List<TodoDTO> { _todoSample1, _todoSample2 });

			var service = new TodoService(mockRepo.Object);
			var result = service.GetTodos(null, null);
			Assert.Equal(list, result);
		}
	}
}
