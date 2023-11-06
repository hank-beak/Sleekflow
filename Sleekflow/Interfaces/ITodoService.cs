using Sleekflow.Models;

namespace Sleekflow.Interfaces
{
    public interface ITodoService
    {
        Todo GetTodoById(int id);
        IEnumerable<Todo> GetTodos(TodoFilter filter, TodoSort sort);
        Todo CreateTodo(Todo todo);
        Todo UpdateTodo(int id, Todo todo);
        bool DeleteTodo(int id);
    }
}
