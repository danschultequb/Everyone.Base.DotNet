using System;

namespace Everyone
{
    /// <summary>
    /// An object that can be used to lock access to a critical section.
    /// </summary>
    public interface Mutex
    {
        public static Mutex Create(Clock clock)
        {
            return MonitorMutex.Create(clock);
        }

        /// <summary>
        /// Get whether this <see cref="Mutex"/> is owned by the current thread.
        /// </summary>
        public Result<bool> IsOwnedByCurrentThread();

        /// <summary>
        /// Try to acquire access to the critical section. Immediately return whether access was
        /// acquired.
        /// </summary>
        public Result<bool> TryAcquire();

        /// <summary>
        /// Acquire access to the critical section.
        /// </summary>
        public Result Acquire();

        /// <summary>
        /// Acquire access to the critical section.
        /// </summary>
        /// <param name="timeout">The <see cref="DateTime"/> at which to timeout.</param>
        public Result Acquire(DateTime timeout);

        /// <summary>
        /// Acquire access to the critical section.
        /// </summary>
        /// <param name="timeout">The amount of time to wait before timing out.</param>
        public Result Acquire(TimeSpan timeout);

        /// <summary>
        /// Release the acquired access to the critical section.
        /// </summary>
        public Result Release();

        /// <summary>
        /// Create a <see cref="MutableMutexCondition"/> that can be used to wait for a condition within a
        /// critical section.
        /// </summary>
        public MutableMutexCondition CreateCondition();

        /// <summary>
        /// Create a <see cref="MutableMutexCondition"/> that can be used to wait for a condition within a
        /// critical section.
        /// </summary>
        /// <param name="condition">The condition that must be true before the condition will
        /// finish watching.</param>
        public MutableMutexCondition CreateCondition(Func<bool> condition);
    }

    /// <summary>
    /// A collection of extension methods for <see cref="Mutex"/>es.
    /// </summary>
    public static class Mutexes
    {
        /// <summary>
        /// Acquire access to the critical section and return a <see cref="Disposable"/> that can
        /// be disposed to release the access.
        /// </summary>
        public static Result<Disposable> CriticalSection(this Mutex mutex)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));

            return Result.Create(() =>
            {
                mutex.Acquire().Await();
                return Disposable.Create(() =>
                {
                    mutex.Release().Await();
                });
            });
        }

        /// <summary>
        /// Acquire access to the critical section and then run the provided <see cref="Action"/>.
        /// When the <see cref="Action"/> is completed, release access to the critical section.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to run within the critical section.</param>
        public static Result CriticalSection(this Mutex mutex, Action action)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                mutex.Acquire().Await();
                try
                {
                    action.Invoke();
                }
                finally
                {
                    mutex.Release().Await();
                }
            });
        }

        /// <summary>
        /// Acquire access to the critical section and then run the provided <see cref="Func{T}"/>.
        /// When the <see cref="Func{T}"/> is completed, release access to the critical section and
        /// return the value returned by the <see cref="Func{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of value returned by the <see cref="Func{T}"/>.</typeparam>
        /// <param name="function">The <see cref="Func{T}"/> to run within the critical section.</param>
        public static Result<T> CriticalSection<T>(this Mutex mutex, Func<T> function)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T result;
                mutex.Acquire().Await();
                try
                {
                    result = function.Invoke();
                }
                finally
                {
                    mutex.Release().Await();
                }
                return result;
            });
        }
    }
}
