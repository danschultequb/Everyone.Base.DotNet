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

                runner.TestMethod("Create(Error)", () =>
                {
                    void CreateTest(Error error, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(error)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Result result = Result.Create(error: error);
                                test.AssertNotNull(result);
                                result.Await();
                            });
                        });
                    }

                    CreateTest(
                        error: null!,
                        expectedException: new PreConditionFailure(
                            "Expression: error",
                            "Expected: not null",
                            "Actual:   null"));
                    CreateTest(
                        error: Error.Create("error message"),
                        expectedException: new AwaitErrorException(
                            error: Error.Create("error message")));
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
                    CreateTest(Error.Create("error message"));
                });

                runner.TestMethod("Create<T>(Error)", () =>
                {
                    void CreateTest<T>(Error error, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(error)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Result<T> result = Result.Create<T>(error: error);
                                test.AssertNotNull(result);
                                result.Await();
                            });
                        });
                    }

                    CreateTest<int>(
                        error: null!,
                        expectedException: new PreConditionFailure(
                            "Expression: error",
                            "Expected: not null",
                            "Actual:   null"));
                    CreateTest<int>(
                        error: Error.Create("error message"),
                        expectedException: new AwaitErrorException(
                            error: Error.Create("error message")));
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
                                new AwaitErrorException(UncaughtExceptionError.Create(new Exception("abc"))));
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

                    runner.Test($"with non-null {nameof(Action)} that throws an {nameof(Exception)}", (Test test) =>
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
                            test.AssertEqual(1, counter);
                        }
                    });
                });
            });
        }
    }
}
