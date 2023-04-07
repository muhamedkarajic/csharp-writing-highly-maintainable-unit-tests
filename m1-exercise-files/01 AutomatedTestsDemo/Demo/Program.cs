using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new MyArrayTests().Maximum_ArrayContainsOneValue_ReturnsThatValue();
                Console.WriteLine("Test passed.");
            }
            catch
            {
                Console.WriteLine("Test failed.");
            }

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}
