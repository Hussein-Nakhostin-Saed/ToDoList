using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Dtos;
using ToDoList.Infrastructure;

namespace ToDoList.Services;

public class TaskService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public TaskService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task Insert(TaskAddDto dto)
    {
        var task = _mapper.Map<Domain.Entities.Task>(dto);
        await _appDbContext.Tasks.AddAsync(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(TaskEditDto dto)
    {
        var task = await _appDbContext.Tasks.SingleAsync(x => x.Id == dto.Id);
        _mapper.Map(task, dto);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<TaskDto> Get(int id)
    {
        var task = await _appDbContext.Tasks.SingleAsync(x => x.Id == id);
        return _mapper.Map<Domain.Entities.Task, TaskDto>(task);
    }

    //public async Task<IEnumerable<TaskDto>> GetAll()
    //{

    //}

    public async Task Delete()
    {

    }
}
