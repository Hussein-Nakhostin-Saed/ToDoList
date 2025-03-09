using AutoMapper;
using ToDoList.Domain.Dtos;
using ToDoList.Domain.Dtos.Task;

namespace ToDoList.Domain.Mappers;

public class TaskMapper : Profile
{
    public TaskMapper()
    {
        CreateMap<Entities.Task, TaskDto>();
        CreateMap<Entities.Task, Card>();
        CreateMap<TaskAddDto, Entities.Task>();
        CreateMap<TaskEditDto, Entities.Task>();
    }
}
