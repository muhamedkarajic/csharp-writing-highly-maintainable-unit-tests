using System.Collections.Generic;
using System.Linq;

namespace CollectionsTests.MyListTests
{
    public class AnyWithEquals
    {
        public int Content { get; set; }

        public override bool Equals(object obj) =>
            this.Equals(obj as AnyWithEquals);

        public override int GetHashCode() => this.Content;

        private bool Equals(AnyWithEquals any) =>
            !object.ReferenceEquals(any, null) && any.Content == this.Content;

        public static IEnumerable<AnyWithEquals> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(k => new AnyWithEquals() {Content = k});

        public static AnyWithEquals MemberwiseClone(AnyWithEquals obj) =>
            new AnyWithEquals() {Content = obj.Content};
    }
}
