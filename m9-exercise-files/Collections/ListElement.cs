namespace Collections
{
    internal class ListElement<T>
    {
        public T Content { get; }
        public ListElement<T> Next { get; set; }

        public ListElement(T content)
        {
            this.Content = content;
        }
    }
}