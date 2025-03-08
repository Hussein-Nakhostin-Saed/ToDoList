namespace ToDoList.Domain.Dtos.WorkLog;

public class WorkLogAddDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDateTime { get; set; }
}
