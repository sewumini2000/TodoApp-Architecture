using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.DTOs;
using Todo.Models;
using Todo.Repositories.Interfaces;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repo;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodos()
        { 
            var todos = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TodoItemDTO>>(todos));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoById(int id)
        {
            var todo = await _repo.GetByIdAsync(id);
            if (todo == null)
                return NotFound();
            return Ok(_mapper.Map<TodoItemDTO>(todo));
        }
        [HttpPost]
        public async Task<ActionResult> CreateTodo(TodoItemDTO todoDTO)
        { 
            var todo = _mapper.Map<TodoItem>(todoDTO);
            await _repo.AddAsync(todo);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, _mapper.Map<TodoItemDTO>(todo));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, TodoItemDTO todoDTO)
        {
            if (id != todoDTO.Id) return BadRequest();
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(todoDTO, existing);
            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todo = await _repo.GetByIdAsync(id);
            if (todo == null) return NotFound();
            await _repo.DeleteAsync(todo);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
