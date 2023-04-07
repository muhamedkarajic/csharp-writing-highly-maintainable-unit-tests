using Castle.DynamicProxy;

namespace DomainTests.DisposableTests
{
    class DisposeInterceptor : IInterceptor
    {
        public bool IsDisposed { get; private set; }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "Dispose")
            {
                this.IsDisposed = true;
            }
            else if (this.IsDisposed)
            {
                throw new DisposablePatternException("Member invoked on a disposed object.");
            }

            invocation.Proceed();
        }
    }
}
