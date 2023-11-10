using Sleekflow.Interfaces;
using Sleekflow.Models.RequestModels;
using Sleekflow.Models.DTOs;

namespace Sleekflow.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly IDbTodoRepo _dbTodoRepo;

        public TodoService(IDbTodoRepo dbTodoRepo)
        {
            _dbTodoRepo = dbTodoRepo;
        }

        public TodoDTO CreateTodo(TodoDTO todo)
        {
            return _dbTodoRepo.Create(todo);
        }

        public bool DeleteTodo(int id)
        {
            return _dbTodoRepo.Delete(id);
        }

        public TodoDTO GetTodoById(int id)
        {
            return _dbTodoRepo.GetById(id);
        }

        public IEnumerable<TodoDTO> GetTodos(TodoFilter filter, TodoSort sort)
        {
            return _dbTodoRepo.GetTodos(filter, sort);
        }

        public TodoDTO UpdateTodo(int id, TodoDTO todo)
        {
            todo.Id = id;
            _dbTodoRepo.Update(todo);

            return todo;
        }
    }
}
