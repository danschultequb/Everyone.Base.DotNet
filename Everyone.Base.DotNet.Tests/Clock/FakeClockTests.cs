using System;

namespace Everyone
{
    public static class FakeClockTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<FakeClock>(() =>
            {
                ClockTests.Test(runner, () => FakeClock.Create(new DateTime(2000, 1, 2)));

                runner.TestMethod("Create(DateTime)", () =>
                {
                    void CreateTest(DateTime currentTime, Exception? expectedException = null)
                    {
                        runner.Test($"with {runner.ToString(currentTime)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                FakeClock clock = FakeClock.Create(currentTime);
                                test.AssertNotNull(clock);
                                test.AssertEqual(currentTime, clock.GetCurrentTime());
                            });
                        });
                    }

                    CreateTest(new DateTime(2000, 1, 2));
                });

                runner.TestMethod("Advance(TimeSpan)", () =>
                {
                    void AdvanceTest(FakeClock clock, TimeSpan duration, DateTime? expectedCurrentTime = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object[] { clock, duration }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                FakeClock advanceResult = clock.Advance(duration);
                                test.AssertSame(clock, advanceResult);
                                test.AssertEqual(expectedCurrentTime, clock.GetCurrentTime());
                            });
                        });
                    }

                    AdvanceTest(
                        clock: FakeClock.Create(new DateTime(2000, 1, 2)),
                        duration: TimeSpan.FromSeconds(-1),
                        expectedException: new PreConditionFailure(
                            "Expression: duration",
                            "Expected: greater than or equal to 00:00:00",
                            "Actual:   -00:00:01"));
                    AdvanceTest(
                        clock: FakeClock.Create(new DateTime(2000, 1, 2)),
                        duration: TimeSpan.FromSeconds(0),
                        expectedCurrentTime: new DateTime(2000, 1, 2));
                    AdvanceTest(
                        clock: FakeClock.Create(new DateTime(2000, 1, 2)),
                        duration: TimeSpan.FromSeconds(1),
                        expectedCurrentTime: new DateTime(2000, 1, 2, 0, 0, 1));
                    AdvanceTest(
                        clock: FakeClock.Create(new DateTime(2000, 1, 2)),
                        duration: TimeSpan.FromSeconds(2),
                        expectedCurrentTime: new DateTime(2000, 1, 2, 0, 0, 2));
                });
            });
        }
    }
}
