namespace Everyone
{
    public static class SpinLockMutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<SpinLockMutex>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    SpinLockMutex mutex = SpinLockMutex.Create();
                    test.AssertNotNull(mutex);
                });

                MutexTests.Test(runner, SpinLockMutex.Create);
            });
        }
    }
}
