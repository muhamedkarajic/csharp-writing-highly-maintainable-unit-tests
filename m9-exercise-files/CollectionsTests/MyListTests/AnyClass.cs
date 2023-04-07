using System.Collections.Generic;

namespace CollectionsTests.MyListTests
{
    public class AnyClass
    {
        public static IEnumerable<AnyClass> SampleData
        {
            get
            {
                while (true)
                {
                    yield return new AnyClass();
                }
            }
        }

        public static AnyClass MemberwiseClone(AnyClass obj) => obj;
            
    }
}
