using AutoMapper;
using ToDoList.Domain.Dtos.Task;

namespace ToDoList.Domain.Mappers;

public class TaskMapper : Profile
{
    public TaskMapper()
    {
        CreateMap<Entities.Task, TaskDto>();
        CreateMap<TaskAddDto, Entities.Task>();
        CreateMap<TaskEditDto, Entities.Task>();
    }
}
