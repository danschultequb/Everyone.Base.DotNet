using System;

namespace Everyone
{
    /// <summary>
    /// A <see cref="MutexCondition"/> that is associated with a <see cref="SpinLockMutex"/>.
    /// </summary>
    public class SpinLockMutexCondition : MutableMutexCondition
    {
        private readonly SpinLockMutex mutex;
        private readonly Clock clock;
        private readonly Func<bool>? condition;
        private int signalValue;

        private SpinLockMutexCondition(SpinLockMutex mutex, Clock clock, Func<bool>? condition)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(clock, nameof(clock));

            this.mutex = mutex;
            this.clock = clock;
            this.condition = condition;
            this.signalValue = 0;
        }

        public static SpinLockMutexCondition Create(SpinLockMutex mutex, Clock clock)
        {
            return new SpinLockMutexCondition(mutex, clock, condition: null);
        }

        public static SpinLockMutexCondition Create(SpinLockMutex mutex, Clock clock, Func<bool> condition)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(clock, nameof(clock));
            Pre.Condition.AssertNotNull(condition, nameof(condition));

            return new SpinLockMutexCondition(mutex, clock, condition);
        }

        private int GetSignalValue()
        {
            return this.signalValue;
        }

        public Result Signal()
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                this.signalValue++;

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        public Result Watch()
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                bool? conditionResult = this.condition?.Invoke();
                while (conditionResult != true)
                {
                    int currentSignalValue = this.GetSignalValue();
                    this.mutex.Release().Await();

                    while (this.GetSignalValue() == currentSignalValue)
                    {
                        CurrentThread.Yield();
                    }

                    this.mutex.Acquire().Await();

                    conditionResult = this.condition?.Invoke();
                    if (conditionResult != false)
                    {
                        break;
                    }
                }

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        private bool HasTimedOut(DateTime timeout)
        {
            return timeout <= this.clock.GetCurrentTime();
        }

        private void ThrowIfTimedOut(DateTime timeout)
        {
            if (this.HasTimedOut(timeout))
            {
                throw new TimeoutException();
            }
        }

        public Result Watch(DateTime timeout)
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                int currentSignalValue = this.GetSignalValue();

                this.ThrowIfTimedOut(timeout);
                this.mutex.Release().Await();

                while (this.GetSignalValue() == currentSignalValue && !this.HasTimedOut(timeout))
                {
                    CurrentThread.Yield();
                }

                this.mutex.Acquire().Await();
                this.ThrowIfTimedOut(timeout);

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        public Result Watch(TimeSpan timeout)
        {
            Pre.Condition.AssertNotNull(timeout);
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                this.Watch(this.clock.GetCurrentTime().Add(timeout)).Await();
            });
        }
    }
}
