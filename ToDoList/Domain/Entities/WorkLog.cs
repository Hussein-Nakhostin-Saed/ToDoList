namespace ToDoList.Domain.Entities;

public class WorkLog : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDateTime { get; set; }
}
