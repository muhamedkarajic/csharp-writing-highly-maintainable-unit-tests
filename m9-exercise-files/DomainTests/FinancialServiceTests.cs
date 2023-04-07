using Domain;
using DomainTests.DisposableTests;
using Moq;
using Xunit;

namespace DomainTests
{
    public class FinancialServiceTests
    {
        [Fact]
        public void GenerateData_AddsFinancialTarget()
        {
            FinancialService svc = new FinancialService();

            Mock<IRepository<FinancialTarget>> repoMock = new Mock<IRepository<FinancialTarget>>();

            Disposable<IRepository<FinancialTarget>> repo = DisposablePattern.For(repoMock.Object);

            svc.GenerateData(() => repo.Object);

            repoMock.Verify(r => r.Add(It.IsAny<FinancialTarget>()));
        }
    }
}
