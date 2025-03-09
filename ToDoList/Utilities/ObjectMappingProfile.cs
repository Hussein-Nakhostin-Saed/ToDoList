using AutoMapper;
using System.Reflection;

namespace ToDoList.Utilities
{
    public class ObjectMappingProfile : Profile
    {
        public ObjectMappingProfile()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<ObjectMapperAttribute>(false);
                if (attr != null)
                {
                    Activator.CreateInstance(type, this);
                }
            }
        }
    }

}
