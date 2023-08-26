using System;
using System.Collections.Generic;

namespace Everyone
{
    public static class RunnableEventTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<RunnableEvent>(() =>
            {
                RunnableEventTests.Test(runner, RunnableEvent.Create);
                RunnableEventTests.Test(runner, RunnableEvent.Create<int>);
                RunnableEventTests.Test(runner, RunnableEvent.Create<int, bool>);

                runner.TestMethod("Create()", (Test test) =>
                {
                    RunnableEvent e = RunnableEvent.Create();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Create<T1>()", (Test test) =>
                {
                    RunnableEvent<int> e = RunnableEvent.Create<int>();
                    test.AssertNotNull(e);
                });

                runner.TestMethod("Create<T1,T2>()", (Test test) =>
                {
                    RunnableEvent<int,bool> e = RunnableEvent.Create<int,bool>();
                    test.AssertNotNull(e);
                });
            });
        }

        public static void Test(TestRunner runner, Func<RunnableEvent> creator)
        {
            runner.TestType<RunnableEvent>(() =>
            {
                EventTests.Test(runner, creator);

                runner.TestMethod("Invoke()", () =>
                {
                    runner.Test("with no subscribers", (Test test) =>
                    {
                        RunnableEvent e = creator.Invoke();
                        e.Invoke();
                    });

                    runner.Test("with one subscriber", (Test test) =>
                    {
                        RunnableEvent e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe(() => values.Add("a")))
                        {
                            e.Invoke();
                            test.AssertEqual(new[] { "a" }, values);
                        }

                        e.Invoke();
                        test.AssertEqual(new[] { "a" }, values);
                    });

                    runner.Test("with two subscribers", (Test test) =>
                    {
                        RunnableEvent e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe(() => values.Add("a")))
                        {
                            e.Subscribe(() => values.Add("1"));

                            e.Invoke();
                            test.AssertEqual(new[] { "a", "1" }, values);
                        }

                        e.Invoke();
                        test.AssertEqual(new[] { "a", "1", "1" }, values);
                    });
                });
            });
        }

        public static void Test(TestRunner runner, Func<RunnableEvent<int>> creator)
        {
            runner.TestType($"{Types.GetFullName<RunnableEvent>()}<T1>", () =>
            {
                EventTests.Test(runner, creator);

                runner.TestMethod("Invoke(T1)", () =>
                {
                    runner.Test("with no subscribers", (Test test) =>
                    {
                        RunnableEvent<int> e = creator.Invoke();
                        e.Invoke(5);
                    });

                    runner.Test("with one subscriber", (Test test) =>
                    {
                        RunnableEvent<int> e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe((int value) => values.Add(Characters.Escape((char)('a' + value)))))
                        {
                            e.Invoke(0);
                            test.AssertEqual(new[] { "a" }, values);
                        }

                        e.Invoke(1);
                        test.AssertEqual(new[] { "a" }, values);
                    });

                    runner.Test("with two subscribers", (Test test) =>
                    {
                        RunnableEvent<int> e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe((int value) => values.Add(Characters.Escape((char)('a' + value)))))
                        {
                            e.Subscribe((int value) => values.Add(Characters.Escape((char)('1' + value))));

                            e.Invoke(2);
                            test.AssertEqual(new[] { "c", "3" }, values);
                        }

                        e.Invoke(3);
                        test.AssertEqual(new[] { "c", "3", "4" }, values);
                    });
                });
            });
        }

        public static void Test(TestRunner runner, Func<RunnableEvent<int,bool>> creator)
        {
            runner.TestType($"{Types.GetFullName<RunnableEvent>()}<T1,T2>", () =>
            {
                EventTests.Test(runner, creator);

                runner.TestMethod("Invoke()", () =>
                {
                    runner.Test("with no subscribers", (Test test) =>
                    {
                        RunnableEvent<int,bool> e = creator.Invoke();
                        e.Invoke(5, false);
                    });

                    runner.Test("with one subscriber", (Test test) =>
                    {
                        RunnableEvent<int,bool> e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe((int value, bool value2) => values.Add(Characters.Escape((char)('a' + value)))))
                        {
                            e.Invoke(0, true);
                            test.AssertEqual(new[] { "a" }, values);
                        }

                        e.Invoke(1, false);
                        test.AssertEqual(new[] { "a" }, values);
                    });

                    runner.Test("with two subscribers", (Test test) =>
                    {
                        RunnableEvent<int,bool> e = creator.Invoke();

                        List<string> values = new List<string>();
                        using (e.Subscribe((int value, bool value2) => values.Add(Characters.Escape((char)('a' + value)))))
                        {
                            e.Subscribe((int value, bool value2) => values.Add(Characters.Escape((char)('1' + value))));

                            e.Invoke(2, false);
                            test.AssertEqual(new[] { "c", "3" }, values);
                        }

                        e.Invoke(3, true);
                        test.AssertEqual(new[] { "c", "3", "4" }, values);
                    });
                });
            });
        }
    }
}
