using System;

namespace Everyone
{
    public static class BasicRunnableEventTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<BasicRunnableEvent>(() =>
            {
                runner.TestMethod("Constructor()", (Test test) =>
                {
                    BasicRunnableEvent e = new BasicRunnableEvent();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Subscribe(Action)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicRunnableEvent e = new BasicRunnableEvent();

                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            e.Subscribe(null!);
                        });

                        e.Invoke();
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        BasicRunnableEvent e = new BasicRunnableEvent();

                        int counter = 0;
                        Disposable subscription = e.Subscribe(() => counter++);
                        test.AssertNotNull(subscription);
                        test.AssertEqual(0, counter);

                        e.Invoke();
                        test.AssertEqual(1, counter);

                        test.AssertTrue(subscription.Dispose().Await());
                        test.AssertEqual(1, counter);

                        e.Invoke();
                        test.AssertEqual(1, counter);

                        test.AssertFalse(subscription.Dispose().Await());

                        e.Invoke();
                        test.AssertEqual(1, counter);
                    });
                });
            });

            runner.TestType($"{Types.GetFullName<BasicRunnableEvent>()}<T1>", () =>
            {
                runner.TestMethod("Constructor()", (Test test) =>
                {
                    BasicRunnableEvent<int> e = new BasicRunnableEvent<int>();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Subscribe(Action<T1>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicRunnableEvent<int> e = new BasicRunnableEvent<int>();

                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            e.Subscribe(null!);
                        });

                        e.Invoke(5);
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        BasicRunnableEvent<int> e = new BasicRunnableEvent<int>();

                        int counter = 0;
                        Disposable subscription = e.Subscribe((int value) => counter += value);
                        test.AssertNotNull(subscription);
                        test.AssertEqual(0, counter);

                        e.Invoke(2);
                        test.AssertEqual(2, counter);

                        test.AssertTrue(subscription.Dispose().Await());
                        test.AssertEqual(2, counter);

                        e.Invoke(3);
                        test.AssertEqual(2, counter);

                        test.AssertFalse(subscription.Dispose().Await());

                        e.Invoke(4);
                        test.AssertEqual(2, counter);
                    });
                });
            });

            runner.TestType($"{Types.GetFullName<BasicRunnableEvent>()}<T1,T2>", () =>
            {
                runner.TestMethod("Constructor()", (Test test) =>
                {
                    BasicRunnableEvent<int,bool> e = new BasicRunnableEvent<int,bool>();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Subscribe(Action<T1>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicRunnableEvent<int,bool> e = new BasicRunnableEvent<int, bool>();

                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            e.Subscribe(null!);
                        });

                        e.Invoke(5, false);
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        BasicRunnableEvent<int, bool> e = new BasicRunnableEvent<int, bool>();

                        int counter = 0;
                        Disposable subscription = e.Subscribe((int value, bool ignored) => counter += value);
                        test.AssertNotNull(subscription);
                        test.AssertEqual(0, counter);

                        e.Invoke(2, false);
                        test.AssertEqual(2, counter);

                        test.AssertTrue(subscription.Dispose().Await());
                        test.AssertEqual(2, counter);

                        e.Invoke(3, true);
                        test.AssertEqual(2, counter);

                        test.AssertFalse(subscription.Dispose().Await());

                        e.Invoke(4, false);
                        test.AssertEqual(2, counter);
                    });
                });
            });
        }
    }
}
