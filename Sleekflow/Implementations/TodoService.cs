using Sleekflow.Interfaces;
using Sleekflow.Models;

namespace Sleekflow.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly IDbTodoRepo _dbTodoRepo;

        public TodoService(IDbTodoRepo dbTodoRepo)
        {
            _dbTodoRepo = dbTodoRepo;
        }

        public Todo CreateTodo(Todo todo)
        {
            return _dbTodoRepo.Create(todo);
        }

        public bool DeleteTodo(int id)
        {
            return _dbTodoRepo.Delete(id);
        }

        public Todo GetTodoById(int id)
        {
            return _dbTodoRepo.GetById(id);
        }

        public IEnumerable<Todo> GetTodos(TodoFilter filter, TodoSort sort)
        {
            return _dbTodoRepo.GetTodos(filter, sort);
        }

        public Todo UpdateTodo(int id, Todo todo)
        {
            todo.Id = id;
            _dbTodoRepo.Update(todo);

            return todo;
        }
    }
}
