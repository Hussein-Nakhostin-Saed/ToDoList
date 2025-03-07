namespace ToDoList.Domain.Entities;

public class Task : Entity
{
    public string Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public bool Important { get; set; }
    public bool Urgent { get; set; }
    public string Description { get; set; }
    public ICollection<WorkLog> WorkLogs { get; set; }

    public Task(string title, bool important, bool urgent)
    {
        Title = title;
        Important = important;
        Urgent = urgent;
        CreationDateTime = DateTime.Now;
    }

    public void IsInProgress()
    {
        Status = TaskStatus.InProgress;
        LastModifiedDateTime = DateTime.Now;
    }
    public void Done()
    {
        Status = TaskStatus.Done;
        LastModifiedDateTime = DateTime.Now;
    }
}
