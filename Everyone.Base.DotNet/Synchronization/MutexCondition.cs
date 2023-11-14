using System;

namespace Everyone
{
    /// <summary>
    /// A condition associated with a <see cref="Mutex"/> that can be watched.
    /// </summary>
    public interface MutexCondition
    {
        /// <summary>
        /// Release this <see cref="MutexCondition"/>'s mutex and watch for this
        /// <see cref="MutexCondition"/>'s signal.
        /// </summary>
        public Result Watch();

        /// <summary>
        /// Release this <see cref="ClockMutexCondition"/>'s mutex and watch for this
        /// <see cref="ClockMutexCondition"/>'s signal.
        /// </summary>
        /// <param name="timeout">The <see cref="DateTime"/> at which to timeout this operation.</param>
        public Result Watch(DateTime timeout);

        /// <summary>
        /// Release this <see cref="ClockMutexCondition"/>'s mutex and watch for this
        /// <see cref="ClockMutexCondition"/>'s signal.
        /// </summary>
        /// <param name="timeout">The amount of time to wait before timing out.</param>
        public Result Watch(TimeSpan timeout);
    }
}
