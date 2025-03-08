using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Dtos.Task;
using ToDoList.Domain.Dtos.WorkLog;
using ToDoList.Infrastructure;
using ToDoList.Utilities;

namespace ToDoList.Services;

public class WorkLogService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public WorkLogService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task Insert(WorkLogAddDto dto)
    {
        var task = _mapper.Map<Domain.Entities.WorkLog>(dto);
        await _appDbContext.WorkLogs.AddAsync(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(WorkLogEditDto dto)
    {
        var task = await _appDbContext.WorkLogs.SingleAsync(x => x.Id == dto.Id);
        _mapper.Map(task, dto);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<WorkLogDto> Get(int id)
    {
        var task = await _appDbContext.WorkLogs.SingleAsync(x => x.Id == id);
        return _mapper.Map<Domain.Entities.WorkLog, WorkLogDto>(task);
    }

    public async Task<WorkLogDto> GetByTask(int taskId)
    {
        var task = await _appDbContext.WorkLogs.SingleAsync(x => x.TaskId == taskId);
        return _mapper.Map<Domain.Entities.WorkLog, WorkLogDto>(task);
    }

    public async Task<IEnumerable<WorkLogDto>> GetAll()
    {
        var tasks = await _appDbContext.WorkLogs.ToListAsync();
        return _mapper.MapCollection<Domain.Entities.WorkLog, WorkLogDto>(tasks);
    }

    public async Task<IEnumerable<WorkLogDto>> GetAll(int taskId)
    {
        var tasks = await _appDbContext.WorkLogs.Where(x => x.TaskId == taskId).ToListAsync();
        return _mapper.MapCollection<Domain.Entities.WorkLog, WorkLogDto>(tasks);
    }

    public async Task Delete(int id)
    {
        var task = await _appDbContext.WorkLogs.SingleAsync(x => x.Id == id);
        _appDbContext.WorkLogs.Remove(task);
        await _appDbContext.SaveChangesAsync();
    }
}
