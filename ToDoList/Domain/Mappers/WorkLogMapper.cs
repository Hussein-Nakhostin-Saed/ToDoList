using AutoMapper;
using ToDoList.Domain.Dtos.WorkLog;
using ToDoList.Domain.Entities;
using ToDoList.Utilities;

namespace ToDoList.Domain.Mappers;

[ObjectMapper]
public class WorkLogMapper : Profile
{
    public WorkLogMapper()
    {
        CreateMap<WorkLog, WorkLogDto>();
        CreateMap<WorkLogAddDto, WorkLog>();
        CreateMap<WorkLogEditDto, WorkLog>();
    }
}
