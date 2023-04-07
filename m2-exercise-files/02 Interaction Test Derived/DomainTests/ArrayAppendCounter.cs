using Collections;

namespace DomainTests
{
    public class ArrayAppendCounter : MyArray
    {
        public int AppendCallsCount { get; set; }

        public override void Append(int value)
        {
            base.Append(value);
            this.AppendCallsCount += 1;
        }
    }
}