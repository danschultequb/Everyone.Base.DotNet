namespace Everyone
{
    /// <summary>
    /// An enum that contains the different states that a <see cref="Disposable"/> can be in.
    /// </summary>
    public enum DisposableState
    {
        /// <summary>
        /// A <see cref="Disposable"/> that has not been disposed.
        /// </summary>
        NotDisposed,

        /// <summary>
        /// A <see cref="Disposable"/> that is in the state of being disposed.
        /// </summary>
        Disposing,

        /// <summary>
        /// A <see cref="Disposable"/> that has been disposed.
        /// </summary>
        Disposed,
    }
}
