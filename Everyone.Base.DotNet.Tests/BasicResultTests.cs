using System;

namespace Everyone
{
    public static class BasicResultTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<BasicResult>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    BasicResult result = BasicResult.Create();
                    test.AssertNotNull(result);

                    result.Await();
                });

                runner.TestMethod("Create(Exception)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => BasicResult.Error(exception: null!),
                            new PreConditionFailure(
                                "Expression: exception",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    void CreateTest(Exception exception, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(exception)}", (Test test) =>
                        {
                            BasicResult result = BasicResult.Error(exception: exception);
                            test.AssertNotNull(result);

                            for (int i = 0; i < 2; i++)
                            {
                                test.AssertThrows(exception, () => result.Await());
                            }
                        });
                    }

                    CreateTest(
                        exception: new Exception("error message"),
                        expectedException: new AwaitException(new Exception("error message")));
                    CreateTest(
                        exception: new Exception("error message"),
                        expectedException: new Exception("error message"));
                });
            });

            runner.TestType("BasicResult<T>", () =>
            {
                runner.TestMethod("Create(T)", () =>
                {
                    void CreateTest<T>(T value)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicResult<T> result = BasicResult<T>.Create(value);
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

                runner.TestMethod("Create(Exception)", () =>
                {
                    void CreateTest<T>(Exception exception, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(exception)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                BasicResult<T> result = BasicResult<T>.Error(exception: exception);
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
            });
        }
    }
}
