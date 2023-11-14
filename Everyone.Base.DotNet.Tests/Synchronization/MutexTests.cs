using System;
using System.Collections.Generic;
using System.Threading;

namespace Everyone
{
    public static class MutexTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<Mutex>(() =>
            {
                runner.TestMethod("Create(Clock)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => Mutex.Create(null!),
                            new PreConditionFailure(
                                "Expression: clock",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        SystemClock clock = SystemClock.Create();
                        Mutex mutex = Mutex.Create(clock);
                        test.AssertNotNull(mutex);
                    });
                });

                MutexTests.Test(runner, Mutex.Create);
            });
        }

        public static void Test(TestRunner runner, Func<Clock, Mutex> creator)
        {
            Pre.Condition.AssertNotNull(runner, nameof(runner));
            Pre.Condition.AssertNotNull(creator, nameof(creator));

            runner.TestType<Mutex>(() =>
            {
                Clock[] clocks = new Clock[] { FakeClock.Create(), SystemClock.Create() };

                foreach (Clock clock in clocks)
                {
                    runner.TestGroup($"with {Types.GetName(clock.GetType())}", () =>
                    {
                        runner.TestMethod("Acquire()", () =>
                        {
                            runner.Test("when not acquired", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertNotNull(mutex);

                                Result acquireResult = mutex.Acquire();
                                test.AssertNotNull(acquireResult);
                                acquireResult.Await();
                            });

                            runner.Test("when acquired by current thread", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertNotNull(mutex);

                                using (mutex.CriticalSection().Await())
                                {
                                    test.AssertThrows(() => mutex.Acquire(),
                                        new PreConditionFailure(
                                            "Expression: this.IsOwnedByCurrentThread().Await()",
                                            "Expected: False",
                                            "Actual:   True"));
                                }
                            });

                            runner.Test("when acquired by a different thread", (Test test) =>
                            {
                                List<string> events = new List<string>();

                                Mutex mutex = creator.Invoke(clock);
                                events.Add("1. acquiring mutex");
                                mutex.Acquire().Await();

                                Thread secondThread = new Thread(() =>
                                {
                                    events.Add("2. acquiring mutex");
                                    mutex.Acquire().Await();

                                    events.Add("2. releasing mutex");
                                    mutex.Release().Await();
                                });
                                secondThread.Start();

                                while (events.Count == 1)
                                {
                                    // Wait for second thread to attempt to acquire the mutex.
                                }

                                test.AssertEqual(
                                    new[]
                                    {
                                "1. acquiring mutex",
                                "2. acquiring mutex",
                                    },
                                    events);

                                events.Add("1. releasing mutex");
                                mutex.Release().Await();

                                secondThread.Join();

                                test.AssertEqual(
                                    new[]
                                    {
                                "1. acquiring mutex",
                                "2. acquiring mutex",
                                "1. releasing mutex",
                                "2. releasing mutex",
                                    },
                                    events);
                            });
                        });

                        runner.TestMethod("Acquire(DateTime)", () =>
                        {
                            runner.Test("with not acquired mutex and time before now", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(clock.GetCurrentTime().AddSeconds(-1)).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and time at now", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(clock.GetCurrentTime()).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and time after now", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                mutex.Acquire(clock.GetCurrentTime().AddSeconds(1)).Await();
                                mutex.Release().Await();
                            });
                        });

                        runner.TestMethod("Acquire(TimeSpan)", () =>
                        {
                            runner.Test("with not acquired mutex and negative TimeSpan", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(TimeSpan.FromSeconds(-1)).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and zero TimeSpan", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertThrows(() => mutex.Acquire(TimeSpan.Zero).Await(),
                                    new TimeoutException());
                            });

                            runner.Test("with not acquired mutex and positive TimeSpan", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                mutex.Acquire(TimeSpan.FromSeconds(1)).Await();
                                mutex.Release().Await();
                            });
                        });

                        runner.TestMethod("Release()", () =>
                        {
                            runner.Test("when not acquired", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertNotNull(mutex);

                                test.AssertThrows(() => mutex.Release(),
                                    new PreConditionFailure(
                                            "Expression: this.IsOwnedByCurrentThread().Await()",
                                            "Expected: True",
                                            "Actual:   False"));
                            });

                            runner.Test("when acquired", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);
                                test.AssertNotNull(mutex);

                                mutex.Acquire().Await();

                                Result releaseResult = mutex.Release();
                                test.AssertNotNull(releaseResult);
                                releaseResult.Await();
                            });
                        });

                        runner.TestMethod("CreateCondition()", (Test test) =>
                        {
                            Mutex mutex = creator.Invoke(clock);

                            MutexCondition condition = mutex.CreateCondition();
                            test.AssertNotNull(condition);
                        });

                        runner.TestMethod("CriticalSection()", (Test test) =>
                        {
                            runner.Test("validate functionality", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);

                                Disposable disposable = mutex.CriticalSection().Await();
                                test.AssertNotNull(disposable);
                                test.AssertTrue(disposable.IsNotDisposed());

                                disposable.Dispose().Await();
                                test.AssertTrue(disposable.IsDisposed());
                            });

                            runner.Test("race condition", (Test test) =>
                            {
                                int counter = 0;
                                Mutex mutex = creator.Invoke(clock);

                                const int threadCount = 10;
                                const int increments = 50;
                                Thread[] threads = new Thread[threadCount];
                                for (int i = 0; i < threadCount; i++)
                                {
                                    threads[i] = new Thread(() =>
                                    {
                                        for (int j = 0; j < increments; j++)
                                        {
                                            using (mutex.CriticalSection().Await())
                                            {
                                                ++counter;
                                            }
                                        }
                                    });
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Start();
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Join();
                                }
                                test.AssertEqual(threadCount * increments, counter);
                            });
                        });

                        runner.TestMethod("CriticalSection(Action)", () =>
                        {
                            runner.Test("with null", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);

                                test.AssertThrows(() => mutex.CriticalSection((Action)null!),
                                    new PreConditionFailure(
                                        "Expression: action",
                                        "Expected: not null",
                                        "Actual:   null"));
                            });

                            runner.Test("with non-null", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);

                                int counter = 0;
                                Result result = mutex.CriticalSection(() => { counter++; });
                                test.AssertNotNull(result);
                                test.AssertEqual(0, counter);

                                for (int i = 0; i < 3; i++)
                                {
                                    result.Await();
                                    test.AssertEqual(1, counter);
                                }
                            });

                            runner.Test("race condition", (Test test) =>
                            {
                                int counter = 0;
                                Mutex mutex = creator.Invoke(clock);

                                const int threadCount = 10;
                                const int increments = 50;
                                Thread[] threads = new Thread[threadCount];
                                for (int i = 0; i < threadCount; i++)
                                {
                                    threads[i] = new Thread(() =>
                                    {
                                        for (int j = 0; j < increments; j++)
                                        {
                                            mutex.CriticalSection(() => { ++counter; }).Await();
                                        }
                                    });
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Start();
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Join();
                                }
                                test.AssertEqual(threadCount * increments, counter);
                            });
                        });

                        runner.TestMethod("CriticalSection(Func<T>)", () =>
                        {
                            runner.Test("with null", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);

                                test.AssertThrows(() => mutex.CriticalSection((Func<int>)null!),
                                    new PreConditionFailure(
                                        "Expression: function",
                                        "Expected: not null",
                                        "Actual:   null"));
                            });

                            runner.Test("with non-null", (Test test) =>
                            {
                                Mutex mutex = creator.Invoke(clock);

                                int counter = 0;
                                Result<int> result = mutex.CriticalSection(() => { return ++counter; });
                                test.AssertNotNull(result);
                                test.AssertEqual(0, counter);

                                for (int i = 0; i < 3; i++)
                                {
                                    test.AssertEqual(1, result.Await());
                                    test.AssertEqual(1, counter);
                                }
                            });

                            runner.Test("race condition", (Test test) =>
                            {
                                int counter = 0;
                                Mutex mutex = creator.Invoke(clock);

                                const int threadCount = 10;
                                const int increments = 50;
                                Thread[] threads = new Thread[threadCount];
                                for (int i = 0; i < threadCount; i++)
                                {
                                    threads[i] = new Thread(() =>
                                    {
                                        for (int j = 0; j < increments; j++)
                                        {
                                            int result = mutex.CriticalSection(() => { ++counter; return j; }).Await();
                                            test.AssertEqual(j, result);
                                        }
                                    });
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Start();
                                }
                                foreach (Thread thread in threads)
                                {
                                    thread.Join();
                                }
                            });
                        });
                    });
                }
            });
        }
    }
}
