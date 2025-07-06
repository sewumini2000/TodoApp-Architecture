using Todo.Data;
using Todo.Models;
using Todo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Todo.Repositories.Implementations
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task AddAsync(TodoItem todo)
        {
            await _context.TodoItems.AddAsync(todo);
        }
        public async Task DeleteAsync(TodoItem todo)
        {
            _context.TodoItems.Remove(todo);
        }
        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async Task<TodoItem> GetByIdAsync(int id)
        {
           return await _context.TodoItems.FindAsync(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
          return  await _context.SaveChangesAsync() > 0;
        }
        public async Task UpdateAsync(TodoItem todo)
        {
            _context.TodoItems.Update(todo);
        }
    }
}
