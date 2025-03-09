namespace ToDoList.Domain.Entities;

public class WorkLog : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDateTime { get; set; }
    public int TaskId { get; set; }
    public Domain.Entities.TaskItem Task { get; set; }
}
