namespace Everyone
{
    /// <summary>
    /// An <see cref="System.IDisposable"/> that keeps track of whether it's been disposed.
    /// </summary>
    public interface Disposable : System.IDisposable
    {
        /// <summary>
        /// Get the <see cref="DisposableState"/> that this <see cref="Disposable"/> is in.
        /// </summary>
        public DisposableState GetDisposableState();

        void System.IDisposable.Dispose()
        {
            this.Dispose().Await();
        }

        /// <summary>
        /// Dispose of this <see cref="Disposable"/>. This function will return true if this was
        /// the first successful disposal of this object, and false if it wasn't.
        /// </summary>
        public new Result<bool> Dispose();

        /// <summary>
        /// Create a new <see cref="Disposable"/>.
        /// </summary>
        public static Disposable Create()
        {
            return BasicDisposable.Create();
        }

        /// <summary>
        /// Create a new <see cref="Disposable"/> that will dispose of the provided
        /// <see cref="System.IDisposable"/> when it is disposed.
        /// </summary>
        /// <param name="disposable">The <see cref="System.IDisposable"/> to dispose of.</param>
        public static Disposable Create(System.IDisposable disposable)
        {
            Pre.Condition.AssertNotNull(disposable, nameof(disposable));

            return Disposable.Create(disposable.Dispose);
        }

        /// <summary>
        /// Create a new <see cref="Disposable"/> that will invoke the provided
        /// <see cref="Action"/> when it is disposed.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke when the returned
        /// <see cref="Disposable"/> is disposed.</param>
        public static Disposable Create(System.Action action)
        {
            return BasicDisposable.Create(action);
        }
    }

    /// <summary>
    /// A collection of extension methods for <see cref="Disposable"/> types.
    /// </summary>
    public static class Disposables
    {
        /// <summary>
        /// Get whether this <see cref="Disposable"/> is in the
        /// <see cref="DisposableState.NotDisposed"/> state.
        /// </summary>
        public static bool IsNotDisposed(this Disposable disposable)
        {
            return disposable.GetDisposableState() == DisposableState.NotDisposed;
        }

        /// <summary>
        /// Get whether this <see cref="Disposable"/> is in the
        /// <see cref="DisposableState.Disposed"/> state.
        /// </summary>
        public static bool IsDisposed(this Disposable disposable)
        {
            return disposable.GetDisposableState() == DisposableState.Disposed;
        }
    }
}
