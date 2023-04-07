using System.Collections.Generic;

namespace DomainTests.EqualityTests.Rules
{
    class IsTypeSealed<T> : ITestRule
    {
        public IEnumerable<string> GetErrorMessages()
        {
            if (!typeof(T).IsSealed)
                yield return $"{typeof(T).Name} should be sealed.";
        }
    }
}
