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

                runner.TestMethod("Create(Error)", () =>
                {
                    void CreateTest(Error error, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(error)}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                BasicResult result = BasicResult.Create(error: error);
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
                        expectedException: new AwaitErrorException(Error.Create("error message")));
                });

                runner.TestMethod("Create<T>(T)", () =>
                {
                    void CreateTest<T>(T value)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicResult<T> result = BasicResult.Create(value);
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
                                BasicResult<T> result = BasicResult.Create<T>(error: error);
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
                        expectedException: new AwaitErrorException(Error.Create("error message")));
                });
            });
        }
    }
}
