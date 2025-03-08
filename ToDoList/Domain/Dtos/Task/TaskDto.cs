namespace ToDoList.Domain.Dtos.Task;

public class TaskDto
{
    public string Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public bool Important { get; set; }
    public bool Urgent { get; set; }
}
