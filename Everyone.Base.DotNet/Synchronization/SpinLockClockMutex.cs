using System;

namespace Everyone
{
    /// <summary>
    /// A <see cref="SpinLockMutex"/> that can use timeouts when it attempts to acquire the lock.
    /// </summary>
    public class SpinLockClockMutex : SpinLockMutex, ClockMutex
    {
        private readonly Clock clock;

        protected SpinLockClockMutex(Clock clock)
        {
            Pre.Condition.AssertNotNull(clock, nameof(clock));

            this.clock = clock;
        }

        public static SpinLockClockMutex Create(Clock clock)
        {
            return new SpinLockClockMutex(clock);
        }

        private void ThrowIfTimedOut(DateTime timeout)
        {
            if (timeout <= this.clock.GetCurrentTime())
            {
                throw new TimeoutException();
            }
        }

        public Result Acquire(DateTime timeout)
        {
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                int currentThreadId = CurrentThread.GetId();
                while (!this.IsOwnedByThread(currentThreadId))
                {
                    this.ThrowIfTimedOut(timeout);

                    while (this.IsOwned())
                    {
                        CurrentThread.Yield();

                        this.ThrowIfTimedOut(timeout);
                    }

                    this.TryAcquire().Await();
                }
            });
        }

        public Result Acquire(TimeSpan timeout)
        {
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                this.Acquire(this.clock.GetCurrentTime().Add(timeout)).Await();
            });
        }
    }
}
