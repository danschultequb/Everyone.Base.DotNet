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
                runner.TestMethod("Create()", (Test test) =>
                {
                    Mutex mutex = Mutex.Create();
                    test.AssertNotNull(mutex);
                });

                MutexTests.Test(runner, Mutex.Create);
            });
        }

        public static void Test(TestRunner runner, Func<Mutex> creator)
        {
            Pre.Condition.AssertNotNull(runner, nameof(runner));
            Pre.Condition.AssertNotNull(creator, nameof(creator));

            runner.TestType<Mutex>(() =>
            {
                runner.TestMethod("Acquire()", () =>
                {
                    runner.Test("when not acquired", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();
                        test.AssertNotNull(mutex);

                        Result acquireResult = mutex.Acquire();
                        test.AssertNotNull(acquireResult);
                        acquireResult.Await();
                    });

                    runner.Test("when acquired by current thread", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();
                        test.AssertNotNull(mutex);

                        using (mutex.CriticalSection().Await())
                        {
                            test.AssertThrows(() => mutex.Acquire(),
                                new PreConditionFailure(
                                    "Expression: this.IsOwnedByCurrentThread()",
                                    "Expected: False",
                                    "Actual:   True"));
                        }
                    });
                });

                runner.TestMethod("Release()", () =>
                {
                    runner.Test("when not acquired", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();
                        test.AssertNotNull(mutex);

                        test.AssertThrows(() => mutex.Release(),
                            new PreConditionFailure(
                                    "Expression: this.IsOwnedByCurrentThread()",
                                    "Expected: True",
                                    "Actual:   False"));
                    });

                    runner.Test("when acquired", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();
                        test.AssertNotNull(mutex);

                        mutex.Acquire().Await();

                        Result releaseResult = mutex.Release();
                        test.AssertNotNull(releaseResult);
                        releaseResult.Await();
                    });
                });

                runner.TestMethod("CriticalSection()", (Test test) =>
                {
                    runner.Test("validate functionality", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();

                        Disposable disposable = mutex.CriticalSection().Await();
                        test.AssertNotNull(disposable);
                        test.AssertTrue(disposable.IsNotDisposed());

                        disposable.Dispose().Await();
                        test.AssertTrue(disposable.IsDisposed());
                    });

                    runner.Test("race condition", (Test test) =>
                    {
                        int counter = 0;
                        Mutex mutex = creator.Invoke();

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
                        Mutex mutex = creator.Invoke();

                        test.AssertThrows(() => mutex.CriticalSection((Action)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();

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
                        Mutex mutex = creator.Invoke();

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
                        Mutex mutex = creator.Invoke();

                        test.AssertThrows(() => mutex.CriticalSection((Func<int>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Mutex mutex = creator.Invoke();

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
                        Mutex mutex = creator.Invoke();

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
    }
}
