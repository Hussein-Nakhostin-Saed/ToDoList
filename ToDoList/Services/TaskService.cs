using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ToDoList.Domain.Dtos;
using ToDoList.Domain.Dtos.Task;
using ToDoList.Infrastructure;
using ToDoList.Utilities;

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
        var task = _mapper.Map<Domain.Entities.TaskItem>(dto);
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
        return _mapper.Map<Domain.Entities.TaskItem, TaskDto>(task);
    }

    //public async Task<ObservableCollection<Card>> GetAllToDoCards()
    //{
    //    var tasks = await _appDbContext.Tasks.Where(x => x.Status == Domain.Entities.TaskStatus.ToDo).ToListAsync();
    //    return _mapper.MapCollection<Domain.Entities.TaskItem, Card>(tasks);
    //}

    //public async Task<ObservableCollection<Card>> GetAllInProgressCards()
    //{
    //    var tasks = await _appDbContext.Tasks.Where(x => x.Status == Domain.Entities.TaskStatus.InProgress).ToListAsync();
    //    return _mapper.MapCollection<Domain.Entities.TaskItem, Card>(tasks);
    //}

    //public async Task<ObservableCollection<Card>> GetAllDoneCards()
    //{
    //    var tasks = await _appDbContext.Tasks.Where(x => x.Status == Domain.Entities.TaskStatus.Done).ToListAsync();
    //    return _mapper.MapCollection<Domain.Entities.TaskItem, Card>(tasks);
    //}

    public async Task Delete(int id)
    {
        var task = await _appDbContext.Tasks.SingleAsync(x => x.Id == id);
        _appDbContext.Tasks.Remove(task);
        await _appDbContext.SaveChangesAsync();
    }
}
