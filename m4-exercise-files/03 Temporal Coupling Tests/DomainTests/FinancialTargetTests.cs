using System;
using System.Linq;
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
            list.Setup(l => l.AppendMany(It.IsAny<int[]>())).Callback<int[]>(
                array =>
                {
                    foreach (int value in array)
                        list.Object.Append(value);
                });

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

            list
                .Setup(l => l.AppendMany(It.IsAny<int[]>()))
                .Callback<int[]>(array =>
                {
                    foreach (int value in array)
                        list.Object.Append(value);
                });

            target.AddTargetPoints(list.Object, count);

            Assert.Equal(expectedValue, (int)valueAtPosition);  // May throw
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

        [Fact]
        public void AddTargetPoints_UsesTimeServer()
        {
            Mock<ITimeServer> timeServer = new Mock<ITimeServer>();
            timeServer.Setup(server => server.GetCurrentMonth()).Verifiable();
            FinancialTarget target = new FinancialTarget(timeServer.Object);

            target.AddTargetPoints(new Mock<IMyList>().Object, 3);

            timeServer.Verify(server => server.GetCurrentMonth(), Times.Once);
        }
    }
}
