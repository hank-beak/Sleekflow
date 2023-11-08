using Sleekflow.Interfaces;
using Sleekflow.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static Sleekflow.Models.TodoSort;
using SortOrder = Sleekflow.Models.TodoSort.SortOrder;

namespace Sleekflow
{
	public class DbTodoRepo: IDbTodoRepo
	{
		private readonly TodoDbContext _context;
        private const string SQL_INSERT = @"INSERT INTO Todos (Name, Description, DueDate, Status) VALUES (@Name, @Description, @DueDate, @Status)
SELECT @@IDENTITY
";
        private const string SQL_SELECT = "SELECT Id, Name, Description, DueDate, Status FROM Todos"; 
        private const string SQL_ID_FILTER = " WHERE Id = @Id";
        private const string SQL_GET_BY_ID = SQL_SELECT + SQL_ID_FILTER;
        private const string SQL_UPDATE = "UPDATE Todos SET Name = @Name, Description = @Description, DueDate = @DueDate, Status = @Status" + SQL_ID_FILTER;
        private const string SQL_DELETE = "DELETE FROM Todos" + SQL_ID_FILTER;

        public DbTodoRepo(TodoDbContext context)
		{
			_context = context;
		}

		public Todo Create(Todo todo)
		{
            _context.Todos.Add(todo);

            return todo;
        }

		public Todo GetById(int id)
        {
            return _context.Todos.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Todo> GetTodos(TodoFilter filter, TodoSort sort)
		{
			var todos = _context.Todos.AsQueryable();

			if (filter != null)
			{
				if (filter.DueDate.HasValue)
				{
					todos = todos.Where(t => t.DueDate == filter.DueDate);
				}

				if (filter.Status.HasValue)
				{
					todos = todos.Where(t => t.Status == filter.Status.ToString());
				}
			}

			if (sort != null)
			{
				switch (sort.TodoSortBy)
				{
					case SortBy.DueDate:
						todos = (sort.TodoSortOrder == SortOrder.Ascending) ?
								todos.OrderBy(t => t.DueDate) :
								todos.OrderByDescending(t => t.DueDate);
						break;

					case SortBy.Status:
						todos = (sort.TodoSortOrder == SortOrder.Ascending) ?
								todos.OrderBy(t => t.Status) :
								todos.OrderByDescending(t => t.Status);
						break;

					case SortBy.Name:
						todos = (sort.TodoSortOrder == SortOrder.Ascending) ?
								todos.OrderBy(t => t.Name) :
								todos.OrderByDescending(t => t.Name);
						break;
				}
			}

			return todos.ToList();
		}

		public void Update(Todo todo)
		{
			_context.Todos.Update(todo);
			_context.SaveChanges();
		}

		public bool Delete(int id)
        {
			var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
			if (todo != null)
			{
				_context.Todos.Remove(todo);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

    }
}
