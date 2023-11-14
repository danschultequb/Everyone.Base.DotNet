using System;

namespace Everyone
{
    public class MonitorMutexCondition : MutableMutexCondition
    {
        private readonly MonitorMutex mutex;
        private readonly Clock clock;
        private readonly object lockObject;
        private readonly Func<bool>? condition;

        protected MonitorMutexCondition(MonitorMutex mutex, Clock clock, object lockObject, Func<bool>? condition)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(clock, nameof(clock));
            Pre.Condition.AssertNotNull(lockObject, nameof(lockObject));

            this.mutex = mutex;
            this.clock = clock;
            this.lockObject = lockObject;
            this.condition = condition;
        }

        public static MonitorMutexCondition Create(MonitorMutex mutex, Clock clock, object lockObject)
        {
            return new MonitorMutexCondition(mutex, clock, lockObject, condition: null);
        }

        public static MonitorMutexCondition Create(MonitorMutex mutex, Clock clock, object lockObject, Func<bool> condition)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));
            Pre.Condition.AssertNotNull(clock, nameof(clock));
            Pre.Condition.AssertNotNull(lockObject, nameof(lockObject));
            Pre.Condition.AssertNotNull(condition, nameof(condition));

            return new MonitorMutexCondition(mutex, clock, lockObject, condition);
        }

        public Result Signal()
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                System.Threading.Monitor.PulseAll(this.lockObject);

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        public Result Watch()
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                if (this.condition == null)
                {
                    System.Threading.Monitor.Wait(this.lockObject);
                }
                else
                {
                    while (!this.condition.Invoke())
                    {
                        System.Threading.Monitor.Wait(this.lockObject);
                    }
                }

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        public Result Watch(DateTime timeout)
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                bool? conditionResult = this.condition?.Invoke();
                while (conditionResult != true)
                {
                    TimeSpan timeoutDuration = timeout.Subtract(this.clock.GetCurrentTime());
                    System.Threading.Monitor.Wait(this.lockObject, timeoutDuration);

                    conditionResult = this.condition?.Invoke();
                    if (conditionResult != false)
                    {
                        break;
                    }
                }

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }

        public Result Watch(TimeSpan timeout)
        {
            Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

            return Result.Create(() =>
            {
                Pre.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");

                this.Watch(this.clock.GetCurrentTime().Add(timeout)).Await();

                Post.Condition.AssertTrue(this.mutex.IsOwnedByCurrentThread().Await(), "this.mutex.IsOwnedByCurrentThread().Await()");
            });
        }
    }
}
