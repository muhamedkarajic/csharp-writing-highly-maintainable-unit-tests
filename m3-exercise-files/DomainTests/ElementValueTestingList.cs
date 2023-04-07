using System;
using System.Collections.Generic;
using System.Linq;
using Collections;

namespace DomainTests
{
    internal class ElementValueTestingList: IMyList
    {
        private int TargetIndex { get; }
        public IEnumerable<int> ElementValue { get; private set; } = Enumerable.Empty<int>();
        private int CurrentIndex { get; set; }

        public ElementValueTestingList(int targetIndex)
        {
            this.TargetIndex = targetIndex;
        }

        public void Append(int value)
        {
            if (this.CurrentIndex == this.TargetIndex)
                this.ElementValue = new[] {value};
            this.CurrentIndex += 1;
        }

        public void AppendMany(int[] points)
        {
            foreach (int point in points)
                this.Append(point);
        }
    }
}
