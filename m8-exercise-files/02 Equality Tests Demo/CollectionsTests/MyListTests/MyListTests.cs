using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using Xunit;

namespace CollectionsTests.MyListTests
{
    public abstract class MyListTests
    {
        [Fact]
        public void Count_NewList_ReturnsZero()
        {
            Assert.Equal(0, this.CreateSut<int>().Count);
        }

        private void Add_InvokedOnce_CountReturnsValueByOneLarger<T>(int initialCount, IEnumerable<T> sampleData)
        {
            IEnumerable<T> data = sampleData.Take(initialCount + 1).ToList();
            T newObject = data.ElementAt(initialCount);

            IMyList<T> list = this.CreateSut(data.Take(initialCount));
            list.Add(newObject);

            int expectedCount = initialCount + 1;
            Assert.Equal(expectedCount, list.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void AddInt_InvokedOnce_CountReturnsValueByOneLarger(int initialCount) =>
            this.Add_InvokedOnce_CountReturnsValueByOneLarger(initialCount, this.SampleIntSequence);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void AddClass_InvokedOnce_CountReturnsValueByOneLarger(int initialCount) =>
            this.Add_InvokedOnce_CountReturnsValueByOneLarger(initialCount, AnyClass.SampleData);

        private void Add_InvokedNTimesOnEmptyList_CountReturnsN<T>(int count, IEnumerable<T> sampleData)
        {
            IMyList<T> list = this.CreateSut<T>();

            foreach (T value in sampleData.Take(count))
                list.Add(value);

            Assert.Equal(count, list.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(19)]
        public void AddInt_InvokedNTimesOnEmptyList_CountReturnsN<T>(int count) =>
            this.Add_InvokedNTimesOnEmptyList_CountReturnsN(count, this.SampleIntSequence);

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(19)]
        public void AddClass_InvokedNTimesOnEmptyList_CountReturnsN<T>(int count) =>
            this.Add_InvokedNTimesOnEmptyList_CountReturnsN(count, AnyClass.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void AddOfRefType_AcceptsNull_CountReturnsValueByOneLarger(int initialCount)
        {
            IMyList<AnyClass> list = this.CreateSut<AnyClass>(AnyClass.SampleData.Take(initialCount));

            list.Add(null);

            int expectedCount = initialCount + 1;
            Assert.Equal(expectedCount, list.Count);
        }

        private void Remove_ContainedInList_CountReturnsValueByOneSmaller<T>(
            int initialCount, int indexToRemove, 
            IEnumerable<T> sampleData, Func<T, T> memberwiseClone)
        {
            IEnumerable<T> data = sampleData.Take(initialCount).ToList();
            T containedObject = data.ElementAt(indexToRemove);
            T objectToRemove = memberwiseClone(containedObject);

            IMyList<T> list = this.CreateSut(data);

            list.Remove(objectToRemove);

            int expectedCount = initialCount - 1;
            Assert.Equal(expectedCount, list.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveInt_ContainedInList_CountReturnsValueByOneSmaller(
                int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_CountReturnsValueByOneSmaller(
                initialCount, indexToRemove, this.SampleIntSequence, x => x);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveStruct_ContainedInList_CountReturnsValueByOneSmaller(
                int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_CountReturnsValueByOneSmaller(
                initialCount, indexToRemove, AnyStruct.SampleData, AnyStruct.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveClass_ContainedInList_CountReturnsValueByOneSmaller(
                int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_CountReturnsValueByOneSmaller(
                initialCount, indexToRemove, AnyClass.SampleData, AnyClass.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveWithEquals_ContainedInList_CountReturnsValueByOneSmaller(
                int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_CountReturnsValueByOneSmaller(
                initialCount, indexToRemove, AnyWithEquals.SampleData, AnyWithEquals.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveEquatable_ContainedInList_CountReturnsValueByOneSmaller(
                int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_CountReturnsValueByOneSmaller(
                initialCount, indexToRemove, AnyEquatable.SampleData, AnyEquatable.MemberwiseClone);

        private void Remove_ContainedInList_ReturnsTrue<T>(
            int initialCount, int indexToRemove,
            IEnumerable<T> sampleData, Func<T, T> memberwiseClone)
        {
            IEnumerable<T> data = sampleData.Take(initialCount).ToList();
            T containedObject = data.ElementAt(indexToRemove);
            T objectToRemove = memberwiseClone(containedObject);

            IMyList<T> list = this.CreateSut(data);

            bool actualResult = list.Remove(objectToRemove);

            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveInt_ContainedInList_ReturnsTrue(int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_ReturnsTrue(
                initialCount, indexToRemove, this.SampleIntSequence, x => x);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveStruct_ContainedInList_ReturnsTrue(int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_ReturnsTrue(
                initialCount, indexToRemove, AnyStruct.SampleData, AnyStruct.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveClass_ContainedInList_ReturnsTrue(int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_ReturnsTrue(
                initialCount, indexToRemove, AnyClass.SampleData, AnyClass.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveWithEquals_ContainedInList_ReturnsTrue(int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_ReturnsTrue(
                initialCount, indexToRemove, AnyWithEquals.SampleData, AnyWithEquals.MemberwiseClone);

        [Theory]
        [InlineData(1, 0)]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveEquatable_ContainedInList_ReturnsTrue(int initialCount, int indexToRemove) =>
            this.Remove_ContainedInList_ReturnsTrue(
                initialCount, indexToRemove, AnyEquatable.SampleData, AnyEquatable.MemberwiseClone);

        private void Remove_ArgumentNotContainedInList_ReturnsFalse<T>(
            int initialCount, IEnumerable<T> sampleData)
        {
            IEnumerable<T> data = sampleData.Take(initialCount + 1);
            T objectToRemove = data.ElementAt(initialCount);

            IMyList<T> list = this.CreateSut(data.Take(initialCount));

            bool actualResult = list.Remove(objectToRemove);

            Assert.False(actualResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveInt_ArgumentNotContainedInList_ReturnsFalse<T>(int initialCount) =>
            this.Remove_ArgumentNotContainedInList_ReturnsFalse(initialCount, this.SampleIntSequence);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveStruct_ArgumentNotContainedInList_ReturnsFalse<T>(int initialCount) =>
            this.Remove_ArgumentNotContainedInList_ReturnsFalse(initialCount, AnyStruct.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveClass_ArgumentNotContainedInList_ReturnsFalse<T>(int initialCount) =>
            this.Remove_ArgumentNotContainedInList_ReturnsFalse(initialCount, AnyClass.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveWithEquals_ArgumentNotContainedInList_ReturnsFalse<T>(int initialCount) =>
            this.Remove_ArgumentNotContainedInList_ReturnsFalse(initialCount, AnyWithEquals.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveEquatable_ArgumentNotContainedInList_ReturnsFalse<T>(int initialCount) =>
            this.Remove_ArgumentNotContainedInList_ReturnsFalse(initialCount, AnyEquatable.SampleData);

        private void Remove_ArgumentContainedTwice_CountReturnsValueByOneLess<T>(
            IEnumerable<T> sampleData, Func<T, T> memberwiseClone)
        {
            T obj = sampleData.First();
            T[] data = new[] {obj, memberwiseClone(obj)};

            IMyList<T> list = this.CreateSut(data);
            T objToRemove = memberwiseClone(obj);

            list.Remove(objToRemove);

            int expectedCount = 1;
            Assert.Equal(expectedCount, list.Count);
        }

        private void RemoveOfRefType_ListContainsNullAndArgumentNotContained_ReturnsFalse<T>(
            int initialCount, int positionOfNull, IEnumerable<T> sampleData) 
            where T:class
        {
            T[] data = sampleData.Take(initialCount + 1).ToArray();
            data[positionOfNull] = null;
            T objToRemove = data[initialCount];

            IMyList<T> list = this.CreateSut(data.Take(initialCount));

            bool actualResult = list.Remove(objToRemove);

            Assert.False(actualResult);
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveClass_ListContainsNullAndArgumentNotContained_ReturnsFalse(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndArgumentNotContained_ReturnsFalse(
                initialCount, positionOfNull, AnyClass.SampleData);

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveWithEquals_ListContainsNullAndArgumentNotContained_ReturnsFalse(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndArgumentNotContained_ReturnsFalse(
                initialCount, positionOfNull, AnyWithEquals.SampleData);

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveEquatable_ListContainsNullAndArgumentNotContained_ReturnsFalse(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndArgumentNotContained_ReturnsFalse(
                initialCount, positionOfNull, AnyEquatable.SampleData);

        private void RemoveOfRefType_ListContainsNullAndReceivesNull_ReturnsTrue<T>(
            int initialCount, int positionOfNull, IEnumerable<T> sampleData)
            where T : class
        {
            T[] data = sampleData.Take(initialCount).ToArray();
            data[positionOfNull] = null;

            IMyList<T> list = this.CreateSut(data);

            bool actualResult = list.Remove(null);

            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveClass_ListContainsNullAndReceivesNull_ReturnsTrue(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndReceivesNull_ReturnsTrue(
                initialCount, positionOfNull, AnyClass.SampleData);

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveWithEquals_ListContainsNullAndReceivesNull_ReturnsTrue(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndReceivesNull_ReturnsTrue(
                initialCount, positionOfNull, AnyWithEquals.SampleData);

        [Theory]
        [InlineData(17, 0)]
        [InlineData(17, 9)]
        [InlineData(17, 16)]
        public void RemoveEquatable_ListContainsNullAndReceivesNull_ReturnsTrue(int initialCount, int positionOfNull) =>
            this.RemoveOfRefType_ListContainsNullAndReceivesNull_ReturnsTrue(
                initialCount, positionOfNull, AnyEquatable.SampleData);

        private void RemoveOfRefType_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse<T>(
                    int initialCount, IEnumerable<T> sampleData)
                    where T : class
        {
            IEnumerable<T> data = sampleData.Take(initialCount).ToList();
            IMyList<T> list = this.CreateSut(data);

            bool actualResult = list.Remove(null);

            Assert.False(actualResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveClass_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(int initialCount) =>
            this.RemoveOfRefType_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(initialCount, AnyClass.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveWithEquals_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(int initialCount) =>
            this.RemoveOfRefType_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(initialCount, AnyWithEquals.SampleData);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void RemoveEquatable_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(int initialCount) =>
            this.RemoveOfRefType_ListContainsNoNullsAndArgumentIsNull_ReturnsFalse(initialCount, AnyEquatable.SampleData);

        [Fact]
        public void RemoveInt_ArgumentContainedTwice_CountReturnsValueByOneLess() =>
            this.Remove_ArgumentContainedTwice_CountReturnsValueByOneLess(this.SampleIntSequence, x => x);

        [Fact]
        public void RemoveStruct_ArgumentContainedTwice_CountReturnsValueByOneLess() =>
            this.Remove_ArgumentContainedTwice_CountReturnsValueByOneLess(AnyStruct.SampleData, AnyStruct.MemberwiseClone);

        [Fact]
        public void RemoveClass_ArgumentContainedTwice_CountReturnsValueByOneLess() =>
            this.Remove_ArgumentContainedTwice_CountReturnsValueByOneLess(AnyClass.SampleData, AnyClass.MemberwiseClone);

        [Fact]
        public void RemoveWithEquals_ArgumentContainedTwice_CountReturnsValueByOneLess() =>
            this.Remove_ArgumentContainedTwice_CountReturnsValueByOneLess(AnyWithEquals.SampleData, AnyWithEquals.MemberwiseClone);

        [Fact]
        public void RemoveEquatable_ArgumentContainedTwice_CountReturnsValueByOneLess() =>
            this.Remove_ArgumentContainedTwice_CountReturnsValueByOneLess(AnyEquatable.SampleData, AnyEquatable.MemberwiseClone);

        private IEnumerable<int> SampleIntSequence => Enumerable.Range(1, int.MaxValue);

        protected abstract IMyList<T> CreateSut<T>();
        protected abstract IMyList<T> CreateSut<T>(IEnumerable<T> initialData);

    }
}
