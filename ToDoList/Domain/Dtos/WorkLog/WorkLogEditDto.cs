namespace ToDoList.Domain.Dtos.WorkLog;

public class WorkLogEditDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDateTime { get; set; }

}
