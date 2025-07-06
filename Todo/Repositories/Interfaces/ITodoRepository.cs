using Todo.Models;

namespace Todo.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task AddAsync(TodoItem todo);
        Task UpdateAsync(TodoItem todo);
        Task DeleteAsync(TodoItem todo);
        Task<bool> SaveChangesAsync();

    }
}
