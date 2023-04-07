using System.Collections.Generic;
using System.Linq;

namespace CollectionsTests.MyListTests
{
    public struct AnyStruct
    {
        public int Content { get; set; }

        public static IEnumerable<AnyStruct> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(k => new AnyStruct() {Content = k});

        public static AnyStruct MemberwiseClone(AnyStruct obj) => obj;
    }
}
