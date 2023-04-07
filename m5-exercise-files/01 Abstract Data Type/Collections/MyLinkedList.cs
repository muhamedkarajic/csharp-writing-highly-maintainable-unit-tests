namespace Collections
{
    public class MyLinkedList : IMyList
    {
        public int Count { get; private set; }

        private ListElement Head { get; set; }
        private ListElement Tail { get; set; }

        public void Append(int value)
        {
            ListElement element = new ListElement(value);

            if (this.Tail == null)
            {
                this.Head = this.Tail = element;
            }
            else
            {
                this.Tail.Next = element;
                this.Tail = element;
            }

            this.Count += 1;
        }

        public int GetFirst() => this.Head.Value;

    }
}
