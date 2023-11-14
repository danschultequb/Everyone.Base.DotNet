using System;
using System.Collections.Generic;
using System.Threading;

namespace Everyone
{
    public static class MutexConditionTests
    {
        public static void Test(TestRunner runner, Func<Clock, Mutex> mutexCreator)
        {
            runner.TestType<MutexCondition>(() =>
            {
                runner.TestMethod("Watch()", (Test test) =>
                {
                    SystemClock clock = SystemClock.Create();

                    Mutex mutex = mutexCreator.Invoke(clock);
                    test.AssertNotNull(mutex);

                    MutableMutexCondition mutableCondition = mutex.CreateCondition();
                    test.AssertNotNull(mutableCondition);

                    List<string> events = List.Create<string>();
                    events.Add("1. creating other thread");

                    Thread otherThread = new Thread(() =>
                    {
                        MutexCondition condition = mutableCondition;

                        using (mutex.CriticalSection().Await())
                        {
                            events.Add("2. awaiting condition");
                            condition.Watch().Await();
                            events.Add("2. condition signaled");
                        }
                    });
                    otherThread.Start();

                    while (events.Count != 2)
                    {
                        CurrentThread.Yield();
                    }

                    using (mutex.CriticalSection().Await())
                    {
                        events.Add("1. signaling condition");
                        mutableCondition.Signal().Await();
                    }

                    otherThread.Join();

                    test.AssertEqual(
                        new[]
                        {
                            "1. creating other thread",
                            "2. awaiting condition",
                            "1. signaling condition",
                            "2. condition signaled",
                        },
                        events);
                });
            });
        }
    }
}
