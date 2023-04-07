using System;

namespace Domain
{
    public class SystemTime : ITimeServer
    {
        public int GetCurrentMonth()
        {
            return DateTime.Today.Month;
        }
    }
}