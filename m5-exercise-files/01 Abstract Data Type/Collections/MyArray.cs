using System;

namespace Collections
{
    public class MyArray : IMyList
    {
        public int Count => this.Data.Length;
        private int[] Data { get; set; }

        public MyArray()
        {
            Data = new int[0];
        }

        public void Append(int value)
        {
            EnlargeBy(1);
            this.Data[this.Data.Length - 1] = value;
        }

        private void EnlargeBy(int count)
        {
            int[] data = this.Data;
            Array.Resize(ref data, data.Length + count);
            this.Data = data;
        }

        public int GetFirst() => this.Data[0];

    }
}
