using System.Collections.Generic;
using Collections;

namespace CollectionsTests
{
    public class MyLinkedListTests: MyListTests.MyListTests
    {
        protected override IMyList<T> CreateSut<T>() =>
            new MyLinkedList<T>();

        protected override IMyList<T> CreateSut<T>(IEnumerable<T> initialData) =>
            new MyLinkedList<T>(initialData);
    }
}
