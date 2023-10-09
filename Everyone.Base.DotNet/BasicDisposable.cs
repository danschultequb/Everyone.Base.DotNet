using System;

namespace Everyone
{
    public class BasicDisposable : Disposable
    {
        private readonly Action action;
        private bool disposed;

        protected BasicDisposable(Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public static BasicDisposable Create()
        {
            return BasicDisposable.Create(() => { });
        }

        public static BasicDisposable Create(Action action)
        {
            return new BasicDisposable(action);
        }

        public bool IsDisposed()
        {
            return this.disposed;
        }

        public Result<bool> Dispose()
        {
            return Result.Create(() =>
            {
                bool result = !this.disposed;
                if (result)
                {
                    this.disposed = true;
                    this.action.Invoke();
                }
                return result;
            });
        }
    }
}
