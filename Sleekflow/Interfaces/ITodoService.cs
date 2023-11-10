using Sleekflow.Models.DTOs;
using Sleekflow.Models.RequestModels;

namespace Sleekflow.Interfaces
{
    public interface ITodoService
    {
        TodoDTO GetTodoById(int id);
        IEnumerable<TodoDTO> GetTodos(TodoFilter filter, TodoSort sort);
        TodoDTO CreateTodo(TodoDTO todo);
        TodoDTO UpdateTodo(int id, TodoDTO todo);
        bool DeleteTodo(int id);
    }
}


