using System;

namespace DomainTests.DisposableTests
{
    public class DisposablePatternException : Exception
    {
        public DisposablePatternException()
        {
            
        }

        public DisposablePatternException(string message) 
            : base(message)
        {
        }

        public DisposablePatternException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}