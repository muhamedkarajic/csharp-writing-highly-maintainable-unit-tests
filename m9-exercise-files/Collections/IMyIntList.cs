namespace Collections
{
    public interface IMyIntList
    {
        int Count { get; }
        void Append(int value);
        int GetFirst();
        int GetElementAt(int index);
    }
}