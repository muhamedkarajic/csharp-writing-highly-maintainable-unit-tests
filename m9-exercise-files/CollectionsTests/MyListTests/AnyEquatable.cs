using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsTests.MyListTests
{
    public class AnyEquatable : IEquatable<AnyEquatable>
    {
        public int Content { get; set; }

        public bool Equals(AnyEquatable other) =>
            !object.ReferenceEquals(other, null) && other.Content == this.Content;

        public static IEnumerable<AnyEquatable> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(k => new AnyEquatable() {Content = k});

        public static AnyEquatable MemberwiseClone(AnyEquatable obj) => 
            new AnyEquatable() {Content = obj.Content};
    }
}
