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
            EnlargeBy(1);
            this.Data[this.Data.Length - 1] = value;
        }

        public void AppendMany(int[] values)
        {
            int count = this.Data.Length;
            EnlargeBy(values.Length);
            Array.Copy(values, 0, this.Data, count, values.Length);
        }

        private void EnlargeBy(int count)
        {
            int[] data = this.Data;
            Array.Resize(ref data, data.Length + count);
            this.Data = data;
        }

        public int GetElementAt(int index) => this.Data[index];
    }
}
