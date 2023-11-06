using Sleekflow.Models;

namespace Sleekflow.Interfaces
{
	public interface IDbTodoRepo
	{
        Todo Create(Todo todo);
        Todo GetById(int id);
        IEnumerable<Todo> GetTodos(TodoFilter todoFilter, TodoSort todoSort);
        void Update(Todo todo);
        bool Delete(int id);
    }
}
