using System;

namespace Everyone
{
    public class MonitorMutex : ClockMutex
    {
        private readonly Clock clock;
        private readonly bool isSystemClock;
        protected readonly object lockObject;

        protected MonitorMutex(Clock clock)
        {
            Pre.Condition.AssertNotNull(clock, nameof(clock));

            this.clock = clock;
            this.isSystemClock = (this.clock.GetType() == typeof(SystemClock));
            this.lockObject = new object();
        }

        public static MonitorMutex Create()
        {
            return MonitorMutex.Create(SystemClock.Create());
        }

        public static MonitorMutex Create(Clock clock)
        {
            return new MonitorMutex(clock);
        }

        private bool IsOwnedByCurrentThread()
        {
            return System.Threading.Monitor.IsEntered(this.lockObject);
        }

        public Result<bool> TryAcquire()
        {
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                return System.Threading.Monitor.TryEnter(this.lockObject);
            });
        }

        public Result Acquire()
        {
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                System.Threading.Monitor.Enter(this.lockObject);
            });
        }

        public Result Acquire(DateTime timeout)
        {
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                if (this.isSystemClock)
                {
                    this.Acquire(timeout.Subtract(this.clock.GetCurrentTime())).Await();
                }
                else
                {
                    while (true)
                    {
                        if (timeout <= this.clock.GetCurrentTime())
                        {
                            throw new TimeoutException();
                        }

                        if (this.TryAcquire().Await())
                        {
                            break;
                        }
                    }
                }
            });
        }

        public Result Acquire(TimeSpan timeout)
        {
            Pre.Condition.AssertNotNull(timeout, nameof(timeout));
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                if (this.isSystemClock)
                {
                    if (timeout <= TimeSpan.Zero)
                    {
                        throw new TimeoutException();
                    }

                    if (!System.Threading.Monitor.TryEnter(this.lockObject, timeout))
                    {
                        throw new TimeoutException();
                    }
                }
                else
                {
                    this.Acquire(this.clock.GetCurrentTime().Add(timeout)).Await();
                }
            });
        }

        public Result Release()
        {
            Pre.Condition.AssertTrue(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                System.Threading.Monitor.Exit(this.lockObject);
            });
        }
    }
}
