using System;

namespace Domain
{
    public class FinancialService
    {
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
