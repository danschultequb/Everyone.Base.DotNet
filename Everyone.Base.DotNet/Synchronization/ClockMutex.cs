using System;

namespace Everyone
{
    /// <summary>
    /// A <see cref="Mutex"/> type that can use a timeout when attempting to locking access to a
    /// critical section.
    /// </summary>
    public interface ClockMutex : Mutex
    {
        public static new ClockMutex Create()
        {
            return MonitorMutex.Create();
        }

        public static ClockMutex Create(Clock clock)
        {
            Pre.Condition.AssertNotNull(clock, nameof(clock));

            return MonitorMutex.Create(clock);
        }

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
    }

    /// <summary>
    /// A collection of extension methods for <see cref="ClockMutex"/>es.
    /// </summary>
    public static class ClockMutexes
    {
        /// <summary>
        /// Acquire access to the critical section and return a <see cref="Disposable"/> that can
        /// be disposed to release the access.
        /// </summary>
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The <see cref="DateTime"/> at which to timeout this operation.</param>
        public static Result<Disposable> CriticalSection(this ClockMutex mutex, DateTime timeout)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));

            return Result.Create(() =>
            {
                mutex.Acquire(timeout).Await();
                return Disposable.Create(() =>
                {
                    mutex.Release().Await();
                });
            });
        }

        /// <summary>
        /// Acquire access to the critical section and return a <see cref="Disposable"/> that can
        /// be disposed to release the access.
        /// </summary>
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The amount of time to wait before timing out.</param>
        public static Result<Disposable> CriticalSection(this ClockMutex mutex, TimeSpan timeout)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));

            return Result.Create(() =>
            {
                mutex.Acquire(timeout).Await();
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
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The <see cref="DateTime"/> at which to timeout this operation.</param>
        /// <param name="action">The <see cref="Action"/> to run within the critical section.</param>
        public static Result CriticalSection(this ClockMutex mutex, DateTime timeout, Action action)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                mutex.Acquire(timeout).Await();
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
        /// Acquire access to the critical section and then run the provided <see cref="Action"/>.
        /// When the <see cref="Action"/> is completed, release access to the critical section.
        /// </summary>
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The amount of time to wait before timing out.</param>
        /// <param name="action">The <see cref="Action"/> to run within the critical section.</param>
        public static Result CriticalSection(this ClockMutex mutex, TimeSpan timeout, Action action)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                mutex.Acquire(timeout).Await();
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
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The <see cref="DateTime"/> at which to timeout this operation.</param>
        /// <param name="function">The <see cref="Func{T}"/> to run within the critical section.</param>
        public static Result<T> CriticalSection<T>(this ClockMutex mutex, DateTime timeout, Func<T> function)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T result;
                mutex.Acquire(timeout).Await();
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

        /// <summary>
        /// Acquire access to the critical section and then run the provided <see cref="Func{T}"/>.
        /// When the <see cref="Func{T}"/> is completed, release access to the critical section and
        /// return the value returned by the <see cref="Func{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of value returned by the <see cref="Func{T}"/>.</typeparam>
        /// <param name="mutex">The <see cref="ClockMutex"/> that restricts access to the critical
        /// section.</param>
        /// <param name="timeout">The amount of time to wait before timing out.</param>
        /// <param name="function">The <see cref="Func{T}"/> to run within the critical section.</param>
        public static Result<T> CriticalSection<T>(this ClockMutex mutex, TimeSpan timeout, Func<T> function)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T result;
                mutex.Acquire(timeout).Await();
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
