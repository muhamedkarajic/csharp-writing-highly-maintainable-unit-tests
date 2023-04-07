namespace Collections
{
    public interface IMyList
    {
        int Count { get; }
        void Append(int value);
        int GetFirst();
    }
}