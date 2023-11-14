namespace Everyone
{
    public static class SpinLockMutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<SpinLockMutex>(() =>
            {
                runner.TestMethod("Create(Clock)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => SpinLockMutex.Create(null!),
                            new PreConditionFailure(
                                "Expression: clock",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        SystemClock clock = SystemClock.Create();
                        SpinLockMutex mutex = SpinLockMutex.Create(clock);
                        test.AssertNotNull(mutex);
                    });
                });

                MutexTests.Test(runner, SpinLockMutex.Create);
            });
        }
    }
}
