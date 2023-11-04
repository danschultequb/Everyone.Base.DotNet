namespace Everyone
{
    public static class SpinLockClockMutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<SpinLockClockMutex>(() =>
            {
                runner.TestMethod("Create(Clock)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => SpinLockClockMutex.Create(null!),
                            new PreConditionFailure(
                                "Expression: clock",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        FakeClock clock = FakeClock.Create();
                        SpinLockClockMutex mutex = SpinLockClockMutex.Create(clock);
                        test.AssertNotNull(mutex);
                    });
                });

                ClockMutexTests.Test(runner, SpinLockClockMutex.Create);
            });
        }
    }
}
