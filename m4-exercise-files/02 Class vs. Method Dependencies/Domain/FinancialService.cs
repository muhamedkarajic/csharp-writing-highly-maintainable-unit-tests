using System;

namespace Domain
{
    public class FinancialService
    {
        private IRepository<FinancialTarget> TargetsRepository { get; }

        public FinancialService(IRepository<FinancialTarget> targetsRepository)
        {
            this.TargetsRepository = targetsRepository;
        }

        public void GenerateData()
        {
            // use this.TargetsRepository
            // do not dispose this.TargetsRepository
        }

        public void GenerateData(IRepository<FinancialTarget> repo)
        {
        }

        public void GenerateData(Func<IRepository<FinancialTarget>> repoFactory)
        {
            using (IRepository<FinancialTarget> repo = repoFactory())
            {
                repo.Add(new Domain.FinancialTarget(new SystemTime()));
                repo.Save();
            }
        }
    }
}
