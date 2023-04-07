using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class MyArray<T> : IMyList<T>
    {
        public int Count => this.Data.Length;

        private T[] Data { get; set; }

        public MyArray()
        {
            this.Data = new T[0];
        }

        public MyArray(IEnumerable<T> initialData)
        {
            this.Data = initialData.ToArray();
        }

        public void Add(T value)
        {
            EnlargeBy(1);
            this.Data[this.Data.Length - 1] = value;
        }

        public bool Remove(T value)
        {
            for (int i = 0; i < this.Data.Length; i++)
            {
                if (AreEqual(this.Data[i], value))
                {
                    this.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        private bool AreEqual(T a, T b)
        {
            if (object.ReferenceEquals(a, null))
                return object.ReferenceEquals(b, null);

            if (typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
                return this.AreNonNullsEqual(a as IEquatable<T>, b);

            return a.Equals(b);
        }

        private bool AreNonNullsEqual(IEquatable<T> equatable, T obj) =>
            equatable.Equals(obj);

        private void EnlargeBy(int count)
        {
            T[] data = this.Data;
            Array.Resize(ref data, data.Length + count);
            this.Data = data;
        }

        private void RemoveAt(int index)
        {
            T[] data = this.Data;
            Array.Copy(data, index + 1, data, index, data.Length - index - 1);
            Array.Resize(ref data, data.Length - 1);
            this.Data = data;
        }
    }
}
