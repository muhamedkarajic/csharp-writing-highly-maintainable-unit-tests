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
                this.AddMore(repo);

                try
                {
                    repo.Save();
                }
                catch
                {
                }
            }
        }

        private void AddMore(IRepository<FinancialTarget> repo)
        {
            // Do stuff
        }
    }
}
