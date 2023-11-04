using System;

namespace Everyone
{
    public static class ClockTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<Clock>(() =>
            {
                ClockTests.Test(runner, Clock.Create);

                runner.TestMethod("Create()", (Test test) =>
                {
                    SystemClock clock = Clock.Create();
                    test.AssertNotNull(clock);
                    test.AssertNotSame(clock, Clock.Create());
                });
            });
        }

        public static void Test(TestRunner runner, Func<Clock> creator)
        {
            runner.TestType<Clock>(() =>
            {
                runner.TestMethod("GetCurrentTime()", (Test test) =>
                {
                    Clock clock = creator.Invoke();
                    
                    DateTime currentTime = clock.GetCurrentTime();
                    test.AssertNotNull(currentTime);
                    test.AssertLessThan(1, (clock.GetCurrentTime() - currentTime).TotalMilliseconds);
                });
            });
        }
    }
}
