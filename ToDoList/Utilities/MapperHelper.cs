using AutoMapper;
using System.Collections.ObjectModel;

namespace ToDoList.Utilities;

public static class MapperHelper
{
    public static ObservableCollection<TDestination> MapCollection<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource>? enumerable)
    {
        if (enumerable is null)
            return null!;

        return new ObservableCollection<TDestination>(enumerable.Select(item => mapper.Map<TDestination>(item)));
    }
}
