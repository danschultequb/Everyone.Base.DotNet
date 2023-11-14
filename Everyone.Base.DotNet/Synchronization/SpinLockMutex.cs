using System;
using Everyone.Threading;

namespace Everyone
{
    public class SpinLockMutex : Mutex
    {
        private const int NotOwned = 0;
        private const int Owned = 1;

        private const int NoOwnerId = -1;

        private readonly Clock clock;

        private int owned;
        private int ownerThreadId;

        protected SpinLockMutex(Clock clock)
        {
            Pre.Condition.AssertNotNull(clock, nameof(clock));

            this.owned = SpinLockMutex.NotOwned;
            this.ownerThreadId = SpinLockMutex.NoOwnerId;
            this.clock = clock;
        }

        public static SpinLockMutex Create(Clock clock)
        {
            return new SpinLockMutex(clock);
        }

        MutableMutexCondition Mutex.CreateCondition()
        {
            return this.CreateCondition();
        }

        MutableMutexCondition Mutex.CreateCondition(Func<bool> condition)
        {
            return this.CreateCondition(condition);
        }

        public SpinLockMutexCondition CreateCondition()
        {
            return SpinLockMutexCondition.Create(this, this.clock);
        }

        public SpinLockMutexCondition CreateCondition(Func<bool> condition)
        {
            return SpinLockMutexCondition.Create(this, this.clock, condition);
        }

        public Result<bool> IsOwned()
        {
            return Result.Create(() =>
            {
                return this.owned != SpinLockMutex.NotOwned;
            });
        }

        public Result<bool> IsOwnedByCurrentThread()
        {
            return Result.Create(() =>
            {
                return this.IsOwnedByThread(CurrentThread.GetId()).Await();
            });
        }

        public Result<bool> IsOwnedByThread(int threadId)
        {
            return Result.Create(() =>
            {
                return this.IsOwned().Await() && this.ownerThreadId == threadId;
            });
        }

        public Result<bool> TryAcquire()
        {
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

                bool result = Atomic.CompareAndSet(ref this.owned, expectedValue: SpinLockMutex.NotOwned, newValue: SpinLockMutex.Owned);
                if (result)
                {
                    this.ownerThreadId = CurrentThread.GetId();
                }
                return result;
            });
        }

        public Result Acquire()
        {
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

                int currentThreadId = CurrentThread.GetId();
                while (!this.IsOwnedByThread(currentThreadId).Await())
                {
                    while (this.IsOwned().Await())
                    {
                        CurrentThread.Yield();
                    }

                    this.TryAcquire().Await();
                }
            });
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
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

                int currentThreadId = CurrentThread.GetId();
                while (!this.IsOwnedByThread(currentThreadId).Await())
                {
                    this.ThrowIfTimedOut(timeout);

                    while (this.IsOwned().Await())
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
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

                this.Acquire(this.clock.GetCurrentTime().Add(timeout)).Await();
            });
        }

        public Result Release()
        {
            Pre.Condition.AssertTrue(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.IsOwnedByCurrentThread().Await(), $"this.{nameof(IsOwnedByCurrentThread)}().Await()");

                this.ownerThreadId = SpinLockMutex.NoOwnerId;
                this.owned = SpinLockMutex.NotOwned;
            });
        }
    }
}
