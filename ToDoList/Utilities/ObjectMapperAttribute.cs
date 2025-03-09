namespace ToDoList.Utilities;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ObjectMapperAttribute : Attribute
{
    public ObjectMapperAttribute()
    {
    }
}
