using Collections;

namespace Domain
{
    public class FinancialTarget
    {
        private readonly int SummerSeasonFirstMonth = 7;
        private readonly int SummerSeasonLastMonth = 8;
        private ITimeServer TimeServer { get; }

        public FinancialTarget(ITimeServer timeServer)
        {
            this.TimeServer = timeServer;
        }

        public void AddTargetPoints(IMyList toList, int count)
        {
            int factor = CalculateScalingFactor();

            int[] points = new int[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = 3 + factor*i;
            }
            toList.AppendMany(points);
        }

        private int CalculateScalingFactor()
        {
            if (IsSummerSeason())
                return 1;
            return 2;
        }

        private bool IsSummerSeason()
        {
            int month = TimeServer.GetCurrentMonth();
            return month >= this.SummerSeasonFirstMonth &&
                    month <= this.SummerSeasonLastMonth;
        }
    }
}
