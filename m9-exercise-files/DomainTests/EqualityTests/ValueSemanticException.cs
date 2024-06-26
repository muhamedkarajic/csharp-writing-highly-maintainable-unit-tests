﻿using System;

namespace DomainTests.EqualityTests
{
    class ValueSemanticException : Exception
    {
        public ValueSemanticException()
        {

        }

        public ValueSemanticException(string message) 
            : base(message)
        {
        }

        public ValueSemanticException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
