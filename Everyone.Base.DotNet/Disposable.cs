using System;

namespace everyone
{
    /// <summary>
    /// An <see cref="IDisposable"/> that keeps track of whether it's been disposed.
    /// </summary>
    public interface Disposable : IDisposable
    {
        /// <summary>
        /// Whether this <see cref="Disposable"/> has been disposed.
        /// </summary>
        public bool Disposed { get; }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose of this <see cref="Disposable"/>. This function will return true if this was
        /// the first successful disposal of this object, and false if it wasn't.
        /// </summary>
        public new bool Dispose();

        /// <summary>
        /// Create a new <see cref="Disposable"/> that will invoke the provided
        /// <see cref="Action"/> when it is disposed.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke when the returned
        /// <see cref="Disposable"/> is disposed.</param>
        /// <returns></returns>
        public static Disposable Create(Action action)
        {
            return new BasicDisposable(action);
        }
    }
}
