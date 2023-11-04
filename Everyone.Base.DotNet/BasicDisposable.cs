using System;

namespace Everyone
{
    public class BasicDisposable : Disposable
    {
        private readonly Action action;
        private DisposableState state;

        protected BasicDisposable(Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.state = DisposableState.NotDisposed;
        }

        public static BasicDisposable Create()
        {
            return BasicDisposable.Create(() => { });
        }

        public static BasicDisposable Create(Action action)
        {
            return new BasicDisposable(action);
        }

        public Result<bool> Dispose()
        {
            return Result.Create(() =>
            {
                bool result = (this.state == DisposableState.NotDisposed);
                if (result)
                {
                    this.state = DisposableState.Disposing;
                    try
                    {
                        this.action.Invoke();
                    }
                    finally
                    {
                        this.state = DisposableState.Disposed;
                    }
                }
                return result;
            });
        }

        public DisposableState GetDisposableState()
        {
            return this.state;
        }
    }
}
