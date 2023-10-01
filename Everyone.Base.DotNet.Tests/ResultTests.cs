using System;

namespace Everyone
{
    public static class ResultTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Result), () =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    Result result = Result.Create();
                    test.AssertNotNull(result);

                    result.Await();
                });

                runner.TestMethod("Create(Exception)", () =>
                {
                    void CreateTest(Exception exception, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(exception)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Result result = Result.Create(exception: exception);
                                test.AssertNotNull(result);

                                result.Await();
                            });
                        });
                    }

                    CreateTest(
                        exception: null!,
                        expectedException: new PreConditionFailure(
                            "Expression: exception",
                            "Expected: not null",
                            "Actual:   null"));
                    CreateTest(
                        exception: new Exception("error message"),
                        expectedException: new AwaitException(new Exception("error message")));
                    CreateTest(
                        exception: new Exception("error message"),
                        expectedException: new Exception("error message"));
                });

                runner.TestMethod("Create<T>(T)", () =>
                {
                    void CreateTest<T>(T value)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            Result<T> result = Result.Create(value);
                            test.AssertNotNull(result);

                            for (int i = 0; i < 2; i++)
                            {
                                test.AssertEqual(value, result.Await());

                                ((Result)result).Await();
                            }
                        });
                    }

                    CreateTest(5);
                    CreateTest(false);
                    CreateTest((string?)null);
                    CreateTest("hello");
                    CreateTest(new Exception("error message"));
                });

                runner.TestMethod("Create<T>(Exception)", () =>
                {
                    void CreateTest<T>(Exception exception, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(exception)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Result<T> result = Result.Create<T>(exception: exception);
                                test.AssertNotNull(result);

                                result.Await();
                            });
                        });
                    }

                    CreateTest<int>(
                        exception: null!,
                        expectedException: new PreConditionFailure(
                            "Expression: exception",
                            "Expected: not null",
                            "Actual:   null"));
                    CreateTest<int>(
                        exception: new Exception("error message"),
                        expectedException: new AwaitException(new Exception("error message")));
                    CreateTest<int>(
                        exception: new Exception("error message"),
                        expectedException: new Exception("error message"));
                });

                runner.TestMethod("Create(Action)", () =>
                {
                    runner.Test($"with null {nameof(Action)}", (Test test) =>
                    {
                        test.AssertThrows(() => Result.Create((Action)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test($"with non-null {nameof(Action)}", (Test test) =>
                    {
                        int counter = 0;
                        Result result = Result.Create(() => { counter++; });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);
                    });

                    runner.Test($"with non-null {nameof(Action)} that throws an {nameof(Exception)}", (Test test) =>
                    {
                        int counter = 0;
                        Result result = Result.Create(() =>
                        {
                            counter++;
                            throw new Exception("abc");
                        });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);
                        
                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => { result.Await(); },
                                new AwaitException(new Exception("abc")));

                            test.AssertThrows(() => { result.Await(); },
                                new Exception("abc"));

                            test.AssertEqual(1, counter);
                        }
                    });
                });

                runner.TestMethod("Create<T>(Func<T>)", () =>
                {
                    runner.Test($"with null Func<T>", (Test test) =>
                    {
                        test.AssertThrows(() => Result.Create((Func<int>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test($"with non-null Func<T>", (Test test) =>
                    {
                        int counter = 0;
                        Result<int> result = Result.Create(() => { return ++counter; });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(1, result.Await());
                            test.AssertEqual(1, counter);
                        }
                    });

                    runner.Test($"with non-null Func<int> that throws an {nameof(Exception)}", (Test test) =>
                    {
                        int counter = 0;
                        Result<int> result = Result.Create<int>(() =>
                        {
                            counter++;
                            throw new Exception("abc");
                        });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => result.Await(),
                                new AwaitException(new Exception("abc")));

                            test.AssertThrows(() => result.Await(),
                                new Exception("abc"));

                            test.AssertEqual(1, counter);
                        }
                    });
                });

                runner.TestMethod("Then(Action)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result result = Result.Create();
                        test.AssertThrows(() => result.Then((Action)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result r1 = Result.Create(new Exception("abc"));
                        Result r2 = r1.Then(() => { });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result r1 = Result.Create();
                        Result r2 = r1.Then(() => { throw new Exception("abc"); });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result r1 = Result.Create(() => counter1++);
                        test.AssertEqual(0, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then(() => counter2++);
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result r1 = Result.Create(() => counter1++);
                        test.AssertEqual(0, counter1);
                        r1.Await();
                        test.AssertEqual(1, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then(() => counter2++);
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });
                });

                runner.TestMethod("Then(Func<T>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result result = Result.Create();
                        test.AssertThrows(() => result.Then((Func<bool>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result r1 = Result.Create(new Exception("abc"));
                        Result<int> r2 = r1.Then(() => { return 3; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result r1 = Result.Create();
                        Result r2 = r1.Then<bool>(() => { throw new Exception("abc"); });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result r1 = Result.Create(() => { counter1++; });
                        test.AssertEqual(0, counter1);

                        int counter2 = 0;
                        Result<int> r2 = r1.Then(() => { return ++counter2; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(1, r2.Await());
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result r1 = Result.Create(() => { counter1++; });
                        test.AssertEqual(0, counter1);
                        r1.Await();
                        test.AssertEqual(1, counter1);

                        int counter2 = 0;
                        Result<int> r2 = r1.Then(() => { return ++counter2 * 10; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(10, r2.Await());
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });
                });
            });

            runner.TestType("Result<T>", () =>
            {
                runner.TestMethod("Then(Action)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result<int> result = Result.Create(5);
                        test.AssertThrows(() => result.Then((Action)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(new Exception("abc"));
                        Result r2 = r1.Then(() => { });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create(2);
                        Result r2 = r1.Then(() => { throw new Exception("abc"); });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then(() => { counter2++; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(1, r1.Await());
                        test.AssertEqual(1, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then(() => counter2++);
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });
                });

                runner.TestMethod("Then(Action<T>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result<int> result = Result.Create(5);
                        test.AssertThrows(() => result.Then((Action<int>)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(new Exception("abc"));
                        int value2 = 0;
                        Result r2 = r1.Then((int r1Value) => { value2 += r1Value; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, value2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                            test.AssertEqual(0, value2);
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create(2);
                        int value2 = 0;
                        Result r2 = r1.Then((int r1Value) =>
                        {
                            value2 += r1Value;
                            throw new Exception("abc");
                        });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, value2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                            test.AssertEqual(2, value2);
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then((int r1Value) => { counter2 += r1Value; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(1, r1.Await());
                        test.AssertEqual(1, counter1);

                        int counter2 = 0;
                        Result r2 = r1.Then((int r1Value) => { counter2 += r1Value; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            r2.Await();
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });
                });

                runner.TestMethod("Then(Func<T>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result<int> result = Result.Create(2);
                        test.AssertThrows(() => result.Then((Func<bool>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(new Exception("abc"));
                        Result<int> r2 = r1.Then(() => { return 3; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(3);
                        Result<bool> r2 = r1.Then<bool>(() => { throw new Exception("abc"); });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);

                        int counter2 = 0;
                        Result<int> r2 = r1.Then(() => { return ++counter2; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(1, r2.Await());
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(1, r1.Await());
                        test.AssertEqual(1, counter1);

                        int counter2 = 0;
                        Result<int> r2 = r1.Then(() => { return ++counter2 * 10; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);
                        test.AssertEqual(0, counter2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(10, r2.Await());
                            test.AssertEqual(1, counter1);
                            test.AssertEqual(1, counter2);
                        }
                    });
                });

                runner.TestMethod("Then<U>(Func<T,U>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        Result<int> result = Result.Create(2);
                        test.AssertThrows(() => result.Then((Func<int,bool>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with parent Result that contains an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(new Exception("abc"));
                        Result<bool> r2 = r1.Then((int r1Value) => r1Value % 2 == 0);
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with Action that throws an exception", (Test test) =>
                    {
                        Result<int> r1 = Result.Create<int>(2);
                        Result<bool> r2 = r1.Then<bool>((int r1Value) => { throw new Exception("abc"); });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => r2.Await(),
                                new Exception("abc"));
                        }
                    });

                    runner.Test("with unawaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);

                        Result<bool> r2 = r1.Then((int r1Value) => { return r1Value % 2 == 0; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(0, counter1);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(false, r2.Await());
                            test.AssertEqual(1, counter1);
                        }
                    });

                    runner.Test("with awaited parent Result and Action that don't throw", (Test test) =>
                    {
                        int counter1 = 0;
                        Result<int> r1 = Result.Create(() => { return ++counter1; });
                        test.AssertEqual(0, counter1);
                        test.AssertEqual(1, r1.Await());
                        test.AssertEqual(1, counter1);

                        Result<bool> r2 = r1.Then((int r1Value) => { return r1Value % 2 == 0; });
                        test.AssertNotNull(r2);
                        test.AssertNotSame(r1, r2);
                        test.AssertEqual(1, counter1);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(false, r2.Await());
                            test.AssertEqual(1, counter1);
                        }
                    });
                });
            });
        }
    }
}
