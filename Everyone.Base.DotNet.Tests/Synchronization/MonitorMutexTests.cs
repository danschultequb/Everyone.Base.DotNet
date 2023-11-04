namespace Everyone
{
    public static class MonitorMutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<MonitorMutex>(() =>
            {
                ClockMutexTests.Test(runner, MonitorMutex.Create);

                runner.TestMethod("Create()", (Test test) =>
                {
                    MonitorMutex mutex = MonitorMutex.Create();
                    test.AssertNotNull(mutex);
                });

                runner.TestMethod("Create(Clock)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => MonitorMutex.Create(null!),
                            new PreConditionFailure(
                                "Expression: clock",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        SystemClock clock = SystemClock.Create();
                        MonitorMutex mutex = MonitorMutex.Create(clock);
                        test.AssertNotNull(mutex);
                    });
                });
            });
        }
    }
}
