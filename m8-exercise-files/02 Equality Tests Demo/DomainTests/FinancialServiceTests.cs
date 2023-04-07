using Domain;
using DomainTests.DisposableTests;
using Moq;
using Xunit;

namespace DomainTests
{
    public class FinancialServiceTests
    {
        [Fact]
        public void GenerateData_MethodReceivesRepoFactory_DisposesRepository()
        {
            FinancialService svc = new FinancialService();
            Mock<IRepository<FinancialTarget>> repo = new Mock<IRepository<FinancialTarget>>();

            svc.GenerateData(() => repo.Object);

            repo.Verify(r => r.Dispose());
        }

        [Fact]
        public void GenerateData_SatisfiesDisposablePattern()
        {
            FinancialService svc = new FinancialService();
            Disposable<IRepository<FinancialTarget>> repo =
                DisposablePattern.For(new Mock<IRepository<FinancialTarget>>().Object);

            svc.GenerateData(() => repo.Object);

            repo.VerifyDisposed();
        }
    }
}
