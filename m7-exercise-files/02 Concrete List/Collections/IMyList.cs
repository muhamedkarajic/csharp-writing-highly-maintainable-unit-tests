namespace Collections
{
    public interface IMyList<T>
    {
        int Count { get; }
        void Add(T value);
        bool Remove(T value);
    }
}
