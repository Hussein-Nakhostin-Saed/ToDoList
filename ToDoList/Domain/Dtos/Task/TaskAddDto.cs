﻿namespace ToDoList.Domain.Dtos.Task;

public class TaskAddDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Important { get; set; }
    public bool Urgent { get; set; }
}
