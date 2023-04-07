﻿using Domain;
using Xunit;

namespace DomainTests
{
    public class DateTests
    {
        [Fact]
        public void Date_ImplementsObjectEquality()
        {
            EqualityTests.EqualityTests.For(new Date(2017, 1, 27))
                .EqualTo(new Date(2017, 1, 27))
                .NotEqualTo(new Date(2012, 1, 27), "different year")
                .NotEqualTo(new Date(2017, 2, 27), "different month")
                .NotEqualTo(new Date(2017, 1, 28), "different day")
                .Assert();
        }
    }
}
