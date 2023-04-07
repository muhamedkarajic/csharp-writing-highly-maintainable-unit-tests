using System;
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
            for (int i = 0; i < count; i++)
            {
                toList.Append(3 + factor*i);
            }
        }
            
        // targetPoints.Count >= 2
        public int GetGoldenTarget(IMyList targetPoints)
        {
            if (targetPoints.Count < 2)
                throw new ArgumentException("points.Count < 2", nameof(targetPoints));
            return (targetPoints.GetFirst() + targetPoints.GetElementAt(targetPoints.Count - 1))/2;
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
