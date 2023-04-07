using System;
using Collections;

namespace Demo
{
    public class MyArrayTests
    {
        // Method under test
        // Scenario which is tested
        // Expected behavior/state/result
        public void Maximum_ArrayContainsOneValue_ReturnsThatValue()
        {
            MyArray array = new MyArray(new[] {5});
            int max = array.Maximum();
            if (max != 5) throw new Exception();
        }
    }
}
