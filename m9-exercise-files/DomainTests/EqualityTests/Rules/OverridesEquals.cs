namespace DomainTests.EqualityTests.Rules
{
    class OverridesEquals<T> : ImplementsMethod<T>
    {
        public OverridesEquals() : base("Equals", typeof(object))
        {
        }
    }
}
