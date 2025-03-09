using AutoMapper;
using ToDoList.Domain.Dtos;
using ToDoList.Domain.Dtos.Task;

namespace ToDoList.Domain.Mappers;

public class TaskMapper : Profile
{
    public TaskMapper()
    {
        CreateMap<Entities.TaskItem, TaskDto>();
        CreateMap<Entities.TaskItem, Card>();
        CreateMap<TaskAddDto, Entities.TaskItem>();
        CreateMap<TaskEditDto, Entities.TaskItem>();
    }
}
