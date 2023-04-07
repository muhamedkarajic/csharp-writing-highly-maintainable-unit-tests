using Collections;

namespace DomainTests
{
    public class ArrayAppendCounter : IMyList
    {
        public int AppendCallsCount { get; set; }

        public void Append(int value)
        {
            this.AppendCallsCount += 1;
        }

        public void AppendMany(int[] values) { }
    }
}