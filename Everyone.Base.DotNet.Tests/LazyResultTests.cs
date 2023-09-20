using System;

namespace Everyone
{
    public static class LazyResultTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<LazyResult>(() =>
            {
                runner.TestMethod("Create(Action)", () =>
                {
                    runner.Test("with null Action", (Test test) =>
                    {
                        test.AssertThrows(() => LazyResult.Create((Action)null!),
                            new PreConditionFailure(
                                "Expression: action",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-throwing Action", (Test test) =>
                    {
                        int counter = 0;
                        
                        LazyResult result = LazyResult.Create(() => { counter++; });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            result.Await();
                            test.AssertEqual(1, counter);
                        }
                    });

                    runner.Test("with throwing Action", (Test test) =>
                    {
                        int counter = 0;

                        LazyResult result = LazyResult.Create(() =>
                        {
                            counter++;
                            throw new InvalidOperationException($"error message {counter}");
                        });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => result.Await(),
                                new AwaitException(new InvalidOperationException("error message 1")));
                            test.AssertEqual(1, counter);
                        }
                    });
                });

                runner.TestMethod("Create<T>(Func<T>)", () =>
                {
                    runner.Test("with null Action", (Test test) =>
                    {
                        test.AssertThrows(() => LazyResult.Create((Func<bool>)null!),
                            new PreConditionFailure(
                                "Expression: function",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-throwing Func<int>", (Test test) =>
                    {
                        int counter = 0;

                        LazyResult<int> result = LazyResult.Create(() => ++counter);
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertEqual(1, result.Await());
                            test.AssertEqual(1, counter);
                        }
                    });

                    runner.Test("with throwing Func", (Test test) =>
                    {
                        int counter = 0;

                        LazyResult<string> result = LazyResult.Create<string>(() =>
                        {
                            counter++;
                            throw new InvalidOperationException($"error message {counter}");
                        });
                        test.AssertNotNull(result);
                        test.AssertEqual(0, counter);

                        for (int i = 0; i < 3; i++)
                        {
                            test.AssertThrows(() => result.Await(),
                                new AwaitException(new InvalidOperationException("error message 1")));
                            test.AssertEqual(1, counter);
                        }
                    });
                });
            });
        }
    }
}
