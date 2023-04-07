namespace DomainTests.EqualityTests
{
    public static class EqualityTests
    {
        public static EqualityTester<T> For<T>(T obj) =>
            new EqualityTester<T>(obj);
    }
}