namespace Everyone
{
    public static class SpinLockMutexConditionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<SpinLockMutexCondition>(() =>
            {
                MutexConditionTests.Test(runner, SpinLockMutex.Create);
            });
        }
    }
}
