using Everyone.Threading;

namespace Everyone
{
    public class SpinLockMutex : Mutex
    {
        private const int NotOwned = 0;
        private const int Owned = 1;

        private const int NoOwnerId = -1;

        private int owned;
        private int ownerThreadId;

        protected SpinLockMutex()
        {
            this.owned = SpinLockMutex.NotOwned;
            this.ownerThreadId = SpinLockMutex.NoOwnerId;
        }

        public static SpinLockMutex Create()
        {
            return new SpinLockMutex();
        }

        protected bool IsOwned()
        {
            return this.owned != SpinLockMutex.NotOwned;
        }

        protected bool IsOwnedByCurrentThread()
        {
            return this.IsOwnedByThread(CurrentThread.GetId());
        }

        protected bool IsOwnedByThread(int threadId)
        {
            return this.IsOwned() && this.ownerThreadId == threadId;
        }

        public Result<bool> TryAcquire()
        {
            return Result.Create(() =>
            {
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
            Pre.Condition.AssertFalse(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                int currentThreadId = CurrentThread.GetId();
                while (!this.IsOwnedByThread(currentThreadId))
                {
                    while (this.IsOwned())
                    {
                        CurrentThread.Yield();
                    }

                    this.TryAcquire().Await();
                }
            });
        }

        public Result Release()
        {
            Pre.Condition.AssertTrue(this.IsOwnedByCurrentThread(), $"this.{nameof(IsOwnedByCurrentThread)}()");

            return Result.Create(() =>
            {
                this.ownerThreadId = SpinLockMutex.NoOwnerId;
                this.owned = SpinLockMutex.NotOwned;
            });
        }
    }
}
