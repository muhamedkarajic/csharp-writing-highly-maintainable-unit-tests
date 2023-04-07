using Domain;
using Moq;
using Xunit;

namespace DomainTests
{
    public class FinancialServiceTests
    {
        [Fact(Skip = "Temporary solution June 9, 2011")]
        public void GenerateData_Parameterless_DoesNotDisposeRepository()
        {
            Mock<IRepository<FinancialTarget>> repo = new Mock<IRepository<FinancialTarget>>();
            repo.Setup(r => r.Dispose()).Verifiable();

            FinancialService svc = new FinancialService(repo.Object);

            svc.GenerateData();

            repo.Verify(r => r.Dispose(), Times.Never);
        }
    }
}
