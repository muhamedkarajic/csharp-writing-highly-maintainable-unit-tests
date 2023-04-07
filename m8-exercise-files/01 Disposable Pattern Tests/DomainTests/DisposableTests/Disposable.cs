using System;

namespace DomainTests.DisposableTests
{
    public class Disposable<TDisposable> where TDisposable : class, IDisposable
    {
        public TDisposable Object { get; }
        private Func<bool> IsDisposed { get; }

        public Disposable(TDisposable obj, Func<bool> isDisposed)
        {
            this.Object = obj;
            this.IsDisposed = isDisposed;
        }

        public void VerifyDisposed()
        {
            if (!this.IsDisposed())
                throw new DisposablePatternException("Object not disposed when expected.");
        }
    }
}