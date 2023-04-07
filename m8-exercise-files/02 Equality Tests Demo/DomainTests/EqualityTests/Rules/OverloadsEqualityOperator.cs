﻿namespace DomainTests.EqualityTests.Rules
{
    class OverloadsEqualityOperator<T> : ImplementsMethod<T>
    {
        public OverloadsEqualityOperator() : base("op_Equality", "operator ==", typeof(T), typeof(T))
        {
        }
    }
}
