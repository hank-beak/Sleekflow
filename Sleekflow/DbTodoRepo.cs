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
		private readonly string _connectionString;
        private const string SQL_INSERT = @"INSERT INTO Todos (Name, Description, DueDate, Status) VALUES (@Name, @Description, @DueDate, @Status)
SELECT @@IDENTITY
";
        private const string SQL_SELECT = "SELECT Id, Name, Description, DueDate, Status FROM Todos"; 
        private const string SQL_ID_FILTER = " WHERE Id = @Id";
        private const string SQL_GET_BY_ID = SQL_SELECT + SQL_ID_FILTER;
        private const string SQL_UPDATE = "UPDATE Todos SET Name = @Name, Description = @Description, DueDate = @DueDate, Status = @Status" + SQL_ID_FILTER;
        private const string SQL_DELETE = "DELETE FROM Todos" + SQL_ID_FILTER;

        public DbTodoRepo(string connectionString)
		{
			_connectionString = connectionString;
		}

		public Todo Create(Todo todo)
		{
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                

                using (var command = new SqlCommand(SQL_INSERT, connection))
                {
                    command.Parameters.AddWithValue("@Name", todo.Name);
                    command.Parameters.AddWithValue("@Description", todo.Description);
                    command.Parameters.AddWithValue("@DueDate", todo.DueDate);
                    command.Parameters.AddWithValue("@Status", todo.Status.ToString());
               
                    int newId = Convert.ToInt32(command.ExecuteScalar());
                    todo.Id = newId;
                }

				return todo;
            }
        }

		public Todo GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
          
                using (var command = new SqlCommand(SQL_GET_BY_ID, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return GetTodoViaReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<Todo> GetTodos(TodoFilter filter, TodoSort sort)
        {
            var todos = new List<Todo>();
            var sql = SQL_SELECT;
            if (filter != null)
            {
                if (filter.DueDate.HasValue)
                {
                    sql += SQL_SELECT.Contains("WHERE", StringComparison.InvariantCultureIgnoreCase)? " AND DueDate = @DueDate": " WHERE DueDate = @DueDate";
                }

                if (filter.Status.HasValue)
                {
                    sql += SQL_SELECT.Contains("WHERE", StringComparison.InvariantCultureIgnoreCase) ? " AND Status = @Status" : " WHERE Status = @Status";
                }
            }

            if(sort != null)
            {
                var order = sort.TodoSortOrder == SortOrder.Ascending ? "ASC" : "DESC";
                switch (sort.TodoSortBy)
                {
                    case SortBy.DueDate:
                        sql += $" ORDER BY DueDate {order}";
                        break;
                    case SortBy.Status:
                        sql += $" ORDER BY Status {order}";
                        break;
                    case SortBy.Name:
                        sql += $" ORDER BY Name {order}";
                        break;
                }
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(sql, connection))
                {
                    if (filter != null && filter.DueDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@DueDate", filter?.DueDate);
                    }
                    if (filter != null && filter.Status.HasValue)
                    {
                        command.Parameters.AddWithValue("@Status", filter?.Status.ToString());
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todos.Add(GetTodoViaReader(reader));
                        }
                    }
                }
            }

            return todos;
        }

        public void Update(Todo todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(SQL_UPDATE, connection))
                {
                    command.Parameters.AddWithValue("@Id", todo.Id);
                    command.Parameters.AddWithValue("@Name", todo.Name);
                    command.Parameters.AddWithValue("@Description", todo.Description);
                    command.Parameters.AddWithValue("@DueDate", todo.DueDate);
                    command.Parameters.AddWithValue("@Status", todo.Status);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();                

                using (var command = new SqlCommand(SQL_DELETE, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int deleted = command.ExecuteNonQuery();
                    return deleted > 0;
                }
            }
        }

        private Todo GetTodoViaReader(IDataReader reader)
        {
            return new Todo
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                DueDate = (DateTime)reader["DueDate"],
                Status = (string)reader["Status"]
            };
        }

    }
}
