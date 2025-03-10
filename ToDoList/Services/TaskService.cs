using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure;

namespace ToDoList.Services;

public class TaskService
{
    private readonly AppDbContext _appDbContext;
    public TaskService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Insert(TaskItem task)
    {
        await _appDbContext.Tasks.AddAsync(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(TaskItem task)
    {
        _appDbContext.Tasks.Update(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetAll()
    {
        return await _appDbContext.Tasks.ToListAsync();
    }

    public async Task Delete(TaskItem task)
    {
        _appDbContext.Tasks.Remove(task);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task ExportToPdf()
    {
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12);

        var tasks = await _appDbContext.Tasks.ToListAsync();
        int yPoint = 40;

        foreach (var task in tasks)
        {
            gfx.DrawString($"{task.Title} - {task.DueDate} - {(task.IsCompleted ? "Done" : "To Do")}",
                font, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
        }

        document.Save("Tasks.pdf");
    }
}
