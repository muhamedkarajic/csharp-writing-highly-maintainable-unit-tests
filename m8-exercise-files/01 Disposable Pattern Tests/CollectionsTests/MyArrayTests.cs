using System.Collections.Generic;
using Collections;

namespace CollectionsTests
{
    public class MyArrayTests: MyListTests.MyListTests
    {
        protected override IMyList<T> CreateSut<T>() =>
            new MyArray<T>();

        protected override IMyList<T> CreateSut<T>(IEnumerable<T> initialData) =>
            new MyArray<T>(initialData);
    }
}
