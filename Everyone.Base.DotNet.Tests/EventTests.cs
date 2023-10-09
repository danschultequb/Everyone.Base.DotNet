using System;

namespace Everyone
{
    public static class EventTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<Event>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    RunnableEvent e = Event.Create();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Create<T1>()", (Test test) =>
                {
                    RunnableEvent<int> e = Event.Create<int>();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Create<T1,T2>()", (Test test) =>
                {
                    RunnableEvent<int,bool> e = Event.Create<int,bool>();
                    test.AssertNotNull(e);
                });
            });
        }

        public static void Test(TestRunner runner, Func<Event> creator)
        {
            runner.TestType<Event>(() =>
            {
                runner.TestMethod("Subscribe(Action)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Event e = creator.Invoke();
                        test.AssertNotNull(e);

                        test.AssertThrows(new ArgumentNullException("action"),
                            () => e.Subscribe(null!));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Event e = creator.Invoke();
                        test.AssertNotNull(e);

                        using (Disposable subscription = e.Subscribe(() => { }))
                        {
                            test.AssertNotNull(subscription);
                            test.AssertFalse(subscription.IsDisposed());

                            test.AssertTrue(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());

                            test.AssertFalse(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());
                        }
                    });
                });
            });
        }

        public static void Test(TestRunner runner, Func<Event<int>> creator)
        {
            runner.TestType("Event<T1>", () =>
            {
                runner.TestMethod("Subscribe(Action<T1>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Event<int> e = creator.Invoke();
                        test.AssertNotNull(e);

                        test.AssertThrows(new ArgumentNullException("action"),
                            () => e.Subscribe(null!));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Event<int> e = creator.Invoke();
                        test.AssertNotNull(e);

                        using (Disposable subscription = e.Subscribe((int arg1) => { }))
                        {
                            test.AssertNotNull(subscription);
                            test.AssertFalse(subscription.IsDisposed());

                            test.AssertTrue(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());

                            test.AssertFalse(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());
                        }
                    });
                });
            });
        }

        public static void Test(TestRunner runner, Func<Event<int,bool>> creator)
        {
            runner.TestType("Event<T1,T2>", () =>
            {
                runner.TestMethod("Subscribe(Action<T1,T2>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Event<int,bool> e = creator.Invoke();
                        test.AssertNotNull(e);

                        test.AssertThrows(new ArgumentNullException("action"),
                            () => e.Subscribe(null!));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Event<int,bool> e = creator.Invoke();
                        test.AssertNotNull(e);

                        using (Disposable subscription = e.Subscribe((int arg1, bool arg2) => { }))
                        {
                            test.AssertNotNull(subscription);
                            test.AssertFalse(subscription.IsDisposed());

                            test.AssertTrue(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());

                            test.AssertFalse(subscription.Dispose().Await());
                            test.AssertTrue(subscription.IsDisposed());
                        }
                    });
                });
            });
        }
    }
}
