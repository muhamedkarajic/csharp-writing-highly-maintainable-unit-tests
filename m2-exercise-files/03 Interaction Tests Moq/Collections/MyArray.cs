using System;

namespace Collections
{
    public class MyArray : IMyList
    {
        private int[] Data { get; set; }
        public int Length => this.Data.Length;

        public MyArray()
        {
            Data = new int[0];
        }

        public void Append(int value)
        {
            int[] data = this.Data;
            Array.Resize(ref data, data.Length + 1);
            data[data.Length - 1] = value;
            this.Data = data;
        }

        public int GetElementAt(int index) => this.Data[index];
    }
}
