namespace DomainTests.EqualityTests.Rules
{
    class OverridesGetHashCode<T> : ImplementsMethod<T>
    {
        public OverridesGetHashCode() : base("GetHashCode")
        {
        }
    }
}
