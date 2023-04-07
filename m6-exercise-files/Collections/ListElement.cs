namespace Collections
{
    internal class ListElement
    {
        public int Value { get; }
        public ListElement Next { get; set; }

        public ListElement(int value)
        {
            this.Value = value;
        }
    }
}