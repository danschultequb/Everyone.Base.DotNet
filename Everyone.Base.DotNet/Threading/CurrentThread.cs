namespace Everyone
{
    /// <summary>
    /// A collection of methods for interacting with the currently executing thread.
    /// </summary>
    public static class CurrentThread
    {
        /// <summary>
        /// Get the unique identifier of the current thread.
        /// </summary>
        public static int GetId()
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Yield the current thread's execution to allow other threads to execute.
        /// </summary>
        public static void Yield()
        {
            System.Threading.Thread.Yield();
        }
    }
}
