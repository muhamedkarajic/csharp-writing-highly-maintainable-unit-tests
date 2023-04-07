using Domain;
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

            Mock<IRepository<FinancialTarget>> repo =
                new Mock<IRepository<FinancialTarget>>();

            svc.GenerateData(() => repo.Object);

            repo.Verify(r => r.Dispose());
        }

        [Fact]
        public void GenerateDataOfRepoFactory_AddsNothingAfterSave()
        {
            Mock<IRepository<FinancialTarget>> repo =
                new Mock<IRepository<FinancialTarget>>();

            bool saveCalled = false;
            bool addCalledAfterSave = false;

            repo.Setup(r => r.Save()).Callback(() => saveCalled = true);
            repo.Setup(r => r.Add(It.IsAny<FinancialTarget>())).Callback(
                () => addCalledAfterSave = saveCalled);

            FinancialService svc = new FinancialService();

            svc.GenerateData(() => repo.Object);

            Assert.False(addCalledAfterSave);
        }

        [Fact]
        public void GenerateDataOfRepoFactory_AddCalled()
        {
            Mock<IRepository<FinancialTarget>> repo =
                new Mock<IRepository<FinancialTarget>>();

            FinancialService svc = new FinancialService();

            svc.GenerateData(() => repo.Object);

            repo.Verify(r => r.Add(It.IsAny<FinancialTarget>()));
        }

        [Fact]
        public void GenerateDataOfRepoFactory_SaveCalled()
        {
            Mock<IRepository<FinancialTarget>> repo =
                new Mock<IRepository<FinancialTarget>>();

            FinancialService svc = new FinancialService();

            svc.GenerateData(() => repo.Object);

            repo.Verify(r => r.Save());
        }
    }
}
