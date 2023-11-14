namespace Everyone
{
    /// <summary>
    /// A <see cref="MutexCondition"/> that can be signaled to indicate that the condition changed.
    /// </summary>
    public interface MutableMutexCondition : MutexCondition
    {
        /// <summary>
        /// Signal all threads waiting on this <see cref="MutableMutexCondition"/> that the
        /// condition has changed.
        /// </summary>
        public Result Signal();
    }
}
