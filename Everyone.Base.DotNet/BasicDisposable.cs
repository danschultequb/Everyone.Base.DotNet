using System;

namespace Everyone
{
    public class BasicDisposable : Disposable
    {
        private readonly Action action;

        public BasicDisposable(Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public bool Disposed { get; private set; }

        public bool Dispose()
        {
            bool result = !this.Disposed;
            if (result)
            {
                this.Disposed = true;
                this.action.Invoke();
            }
            return result;
        }
    }
}
