using Castle.DynamicProxy;

namespace DomainTests.DisposableTests
{
    class DisposeInterceptor : IInterceptor
    {
        public bool IsDisposed { get; private set; }

        public void Intercept(IInvocation invocation)
        {
            System.Diagnostics.Debug.Assert(
                invocation.Method.Name == "Dispose" || !this.IsDisposed,
                "Member invoked on a disposed object.");

            if (invocation.Method.Name == "Dispose")
            {
                this.IsDisposed = true;
            }

            invocation.Proceed();
        }
    }
}
