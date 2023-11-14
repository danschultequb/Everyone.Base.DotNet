namespace Everyone
{
    public static class MonitorMutexConditionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<MonitorMutexCondition>(() =>
            {
                MutexConditionTests.Test(runner, MonitorMutex.Create);
            });
        }
    }
}
