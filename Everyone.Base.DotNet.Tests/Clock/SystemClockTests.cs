namespace Everyone
{
    public static class SystemClockTests
    {
        public static void Test(TestRunner runner)
        {
            ClockTests.Test(runner, SystemClock.Create);
        }
    }
}
