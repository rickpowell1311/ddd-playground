namespace DddPlayground.Infrastrcuture
{
    public interface IPopulatableFrom<T>
    {
        void Populate(T source);
    }
}
