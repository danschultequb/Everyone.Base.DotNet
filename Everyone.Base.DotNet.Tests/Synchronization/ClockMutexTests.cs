using System;

namespace Everyone
{
    public static class ClockMutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<ClockMutex>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    ClockMutex mutex = ClockMutex.Create();
                    test.AssertNotNull(mutex);
                });

                runner.TestMethod("Create(Clock)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => ClockMutex.Create(null!),
                            new PreConditionFailure(
                                "Expression: clock",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        SystemClock clock = SystemClock.Create();
                        ClockMutex mutex = ClockMutex.Create(clock);
                        test.AssertNotNull(mutex);
                    });
                });
            });
        }

        public static void Test(TestRunner runner, Func<Clock,ClockMutex> creator)
        {
            runner.TestType<ClockMutex>(() =>
            {
                Clock[] clocks = new Clock[] { FakeClock.Create(), SystemClock.Create() };

                foreach (Clock clock in clocks)
                {
                    runner.TestGroup($"with {Types.GetName(clock.GetType())}", () =>
                    {
                        MutexTests.Test(runner, () => creator.Invoke(clock));

                        runner.TestMethod("Acquire(DateTime)", () =>
                        {
                            runner.Test("with not acquired mutex and time before now", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(clock.GetCurrentTime().AddSeconds(-1)).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and time at now", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(clock.GetCurrentTime()).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and time after now", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                mutex.Acquire(clock.GetCurrentTime().AddSeconds(1)).Await();
                                mutex.Release().Await();
                            });
                        });

                        runner.TestMethod("Acquire(TimeSpan)", () =>
                        {
                            runner.Test("with not acquired mutex and negative TimeSpan", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(TimeSpan.FromSeconds(-1)).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and zero TimeSpan", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(TimeSpan.Zero).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and positive TimeSpan", (Test test) =>
                            {
                                ClockMutex mutex = creator.Invoke(clock);
                                mutex.Acquire(TimeSpan.FromSeconds(1)).Await();
                                mutex.Release().Await();
                            });
                        });
                    });
                }
            });
        }
    }
}
