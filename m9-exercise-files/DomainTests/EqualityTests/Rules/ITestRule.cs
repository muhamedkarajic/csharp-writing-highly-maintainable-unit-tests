using System.Collections.Generic;

namespace DomainTests.EqualityTests.Rules
{
    interface ITestRule
    {
        IEnumerable<string> GetErrorMessages();
    }
}
