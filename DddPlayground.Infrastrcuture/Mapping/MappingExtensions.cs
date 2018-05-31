namespace DddPlayground.Infrastrcuture
{
    public static class MappingExtensions
    {
        public static void Populate<T>(this IPopulatableFrom<T> populatable, T source)
        {
            populatable.Populate(source);
        }

        public static T Map<T>(this IMappableTo<T> mappable)
        {
            return mappable.Map();
        }
    }
}
