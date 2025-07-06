using AutoMapper;
using Todo.DTOs;
using Todo.Models;

namespace Todo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<TodoItem,TodoItemDTO>().ReverseMap();
        }
    }
}
