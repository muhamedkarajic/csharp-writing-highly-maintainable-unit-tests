using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class MyLinkedList<T> : IMyList<T>
    {
        public int Count
        {
            get
            {
                int count = 0;
                for (ListElement<T> cur = this.Head.Next; cur != null; cur = cur.Next)
                    count += 1;
                return count;
            }
        }

        private ListElement<T> Head { get; }
        private ListElement<T> Tail { get; set; }

        private Func<T, T, bool> Equal { get; }
        private bool AreElementsEquatable => typeof(IEquatable<T>).IsAssignableFrom(typeof(T));

        public MyLinkedList()
        {
            if (this.AreElementsEquatable)
                this.Equal = this.EquatableEqual;
            else
                this.Equal = this.CommonEqual;

            this.Head = new ListElement<T>(default(T));
            this.Tail = this.Head;
        }

        public MyLinkedList(IEnumerable<T> initialContent) : this()
        {
            foreach (T value in initialContent.Take(1))
                this.Head.Next = this.Tail = new ListElement<T>(value);

            foreach (T value in initialContent.Skip(1))
            {
                this.Tail.Next = new Collections.ListElement<T>(value);
                this.Tail = this.Tail.Next;
            }
        }

        public void Add(T value)
        {
            this.Tail.Next = new ListElement<T>(value);
            this.Tail = this.Tail.Next;
        }

        public bool Remove(T value)
        {
            for (ListElement<T> predecessor = this.Head; predecessor.Next != null; predecessor = predecessor.Next)
                if (this.Equal(predecessor.Next.Content, value))
                {
                    this.RemoveAfter(predecessor);
                    return true;
                }
            return false;
        }

        private bool CommonEqual(T a, T b) => object.Equals(a, b);

        private bool EquatableEqual(T a, T b) => 
            this.EquatableEquals(a as IEquatable<T>, b);

        private bool EquatableEquals(IEquatable<T> a, T b) =>
            a?.Equals(b) ?? object.ReferenceEquals(b, null);

        private void RemoveAfter(ListElement<T> el)
        {
            el.Next = el.Next.Next;
        }
    }
}
