using AutoMapper;

namespace ToDoList.Utilities;

public static class MapperHelper
{
    public static IEnumerable<TDestination> MapCollection<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource>? enumerable)
    {
        if (enumerable is null)
            return null!;

        return enumerable.Select(item => mapper.Map<TDestination>(item)).ToList();
    }
}
