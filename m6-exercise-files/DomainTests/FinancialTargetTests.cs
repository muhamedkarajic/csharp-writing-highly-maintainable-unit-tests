using System;
using Collections;
using Domain;
using Moq;
using Xunit;

namespace DomainTests
{
    public class FinancialTargetTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(17)]
        public void AddTargetPoints_AddsSpecifiedNumberOfElements(int count)
        {
            FinancialTarget target = this.CreateFinancialTarget();
            Mock<IMyList> list = new Mock<IMyList>();

            list.Setup(l => l.Append(It.IsAny<int>()));

            target.AddTargetPoints(list.Object, count);

            list.Verify(l => l.Append(It.IsAny<int>()), Times.Exactly(count));

        }

        [Theory]
        [InlineData(216, 1, 0, 3)]
        [InlineData(216, 1, 9, 21)]
        [InlineData(216, 1, 71, 145)]
        [InlineData(216, 1, 215, 433)]
        [InlineData(216, 7, 71, 74)]
        [InlineData(216, 6, 71, 145)]
        [InlineData(216, 8, 71, 74)]
        [InlineData(216, 9, 71, 145)]
        [InlineData(216, 11, 71, 145)]
        public void AddTargetPoints_SpecificValueAtPosition(int count, int month, int index, int expectedValue)
        {
            FinancialTarget target = this.CreateFinancialTarget(month);

            Mock<IMyList> list = new Mock<IMyList>();
            int addedCount = 0;
            int? valueAtPosition = null;

            list
                .Setup(l => l.Append(It.IsAny<int>()))
                .Callback<int>(value =>
                {
                    if (addedCount++ == index)
                        valueAtPosition = value;
                });

            target.AddTargetPoints(list.Object, count);

            Assert.Equal(expectedValue, (int)valueAtPosition);  // May throw
        }

        [Theory]
        [InlineData(new[]{1, 2}, 1)]
        [InlineData(new[] {2, 4, 1, 7, 8}, 5)]
        [InlineData(new[] { 2, 1, 3, 9, 16, 7 }, 4)]
        public void GetGoldenTarget_ReceivesAtLeastTwoTargetPoints_ReturnsExpectedTarget(
            int[] points, int expectedTarget)
        {
            IMyList list = new MyArray();
            foreach (int point in points)
                list.Append(point);

            ITimeServer timeServer = new Mock<ITimeServer>().Object;

            FinancialTarget target = new FinancialTarget(timeServer);

            int targetPoint = target.GetGoldenTarget(list);

            Assert.Equal(expectedTarget, targetPoint);
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new[] {1})]
        public void GetGoldenTarget_ReceivesLessThanTwoPoints_Fails(int[] targetPoints)
        {
            IMyList list = new MyArray();
            foreach (int point in targetPoints)
                list.Append(point);

            ITimeServer timeServer = new Mock<ITimeServer>().Object;

            FinancialTarget target = new FinancialTarget(timeServer);

            Assert.ThrowsAny<Exception>(() => target.GetGoldenTarget(list));
        }

        private FinancialTarget CreateFinancialTarget()
        {
            Mock<ITimeServer> timeServer = new Mock<ITimeServer>();
            return new FinancialTarget(timeServer.Object);
        }

        private FinancialTarget CreateFinancialTarget(int month)
        {
            Mock<ITimeServer> timeServer = new Mock<ITimeServer>();
            timeServer.Setup(server => server.GetCurrentMonth()).Returns(month);
            return new FinancialTarget(timeServer.Object);
        }
    }
}
