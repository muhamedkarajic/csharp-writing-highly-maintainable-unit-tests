using Collections;

namespace DomainTests
{
    internal class ElementCountingList: IMyList
    {
        public int Length { get; private set; }

        public void Append(int value)
        {
            this.Length += 1;
        }

        public void AppendMany(int[] points)
        {
            this.Length += points.Length;
        }
    }
}
