using Collections;
using Xunit;

namespace CollectionsTests
{
    public class MyArrayTests
    {
        [Fact]
        public void Count_NewList_ReturnsZero()
        {
            IMyList list = this.CreateSut();
            Assert.Equal(0, list.Count);
        }

        [Theory]
        [InlineData(-7)]
        [InlineData(0)]
        [InlineData(19)]
        public void GetFirst_OneValueAppended_ReturnsThatValue(int value)
        {
            IMyList list = this.CreateSut();
            list.Append(value);
            Assert.Equal(value, list.GetFirst());
        }

        [Theory]
        [InlineData(new[] {7})]
        [InlineData(new[] {7, 19, 214})]
        [InlineData(new[] {2, 1, 6, 9, 8, 40, 106, 11, 720, 4, 15})]
        public void Count_NValuesAppended_ReturnsN(int[] values)
        {
            IMyList list = this.CreateSut();

            foreach (int value in values)
                list.Append(value);

            Assert.Equal(values.Length, list.Count);
        }

        [Theory]
        [InlineData(new[] { 2, 1, 6, 9, 8, 40, 106, 11, 720, 4, 15 }, 0, 2)]
        [InlineData(new[] { 2, 1, 6, 9, 8, 40, 106, 11, 720, 4, 15 }, 7, 11)]
        [InlineData(new[] { 2, 1, 6, 9, 8, 40, 106, 11, 720, 4, 15 }, 10, 15)]
        public void GetElementAt_NValuesAppended_ValueAtPositionKIsKthAppendedValue(int[] values, int index, int expectedValue)
        {
            IMyList list = this.CreateSut();

            foreach (int value in values)
                list.Append(value);

            int actualValue = list.GetElementAt(index);

            Assert.Equal(expectedValue, actualValue);
        }

        private IMyList CreateSut() => new MyArray();
    }
}
