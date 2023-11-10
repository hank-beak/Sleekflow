using Sleekflow.Models.DTOs;
using Sleekflow.Models.RequestModels;

namespace Sleekflow.Interfaces
{
    public interface IDbTodoRepo
	{
        TodoDTO Create(TodoDTO todo);
        TodoDTO GetById(int id);
        IEnumerable<TodoDTO> GetTodos(TodoFilter todoFilter, TodoSort todoSort);
        void Update(TodoDTO todo);
        bool Delete(int id);
    }
}
