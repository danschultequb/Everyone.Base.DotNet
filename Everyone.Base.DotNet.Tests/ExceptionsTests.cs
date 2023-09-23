using System;

namespace Everyone
{
    public static class ExceptionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Exceptions), () =>
            {
                runner.TestMethod("UnwrapTo<T>(object?)", () =>
                {
                    void UnwrapToTest<T>(object? value, T? expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { typeof(T), value }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Exceptions.UnwrapTo<T>(value));
                        });
                    }

                    UnwrapToTest<PreConditionFailure>(
                        value: null,
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: "hello",
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: new[] { new PreConditionFailure("abc") },
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: new PreConditionFailure("abc"),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest<PreConditionFailure>(
                        value: new AwaitException(new PreConditionFailure("abc")),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest<PreConditionFailure>(
                        value: new Exception("fake-message", new PreConditionFailure("abc")),
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: UncaughtExceptionError.Create(new PreConditionFailure("abc")),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest<PreConditionFailure>(
                        value: new Exception("abc"),
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: new AwaitException(new Exception("abc")),
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: UncaughtExceptionError.Create(new Exception("abc")),
                        expected: null);
                });

                runner.TestMethod("UnwrapTo(Type,object?)", () =>
                {
                    void UnwrapToTest(Type targetType, object? value, object? expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { targetType, value }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Exceptions.UnwrapTo(targetType, value));
                        });
                    }

                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: null,
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: "hello",
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new[] { new PreConditionFailure("abc") },
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new PreConditionFailure("abc"),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new AwaitException(new PreConditionFailure("abc")),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new Exception("fake-message", new PreConditionFailure("abc")),
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: UncaughtExceptionError.Create(new PreConditionFailure("abc")),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new Exception("abc"),
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: new AwaitException(new Exception("abc")),
                        expected: null);
                    UnwrapToTest(
                        targetType: typeof(PreConditionFailure),
                        value: UncaughtExceptionError.Create(new Exception("abc")),
                        expected: null);
                });

                runner.TestGroup("Exception StackTraces", () =>
                {
                    runner.Test("with non-thrown Exception", (Test test) =>
                    {
                        Exception e1 = new Exception();
                        test.AssertNull(e1.StackTrace);
                    });

                    runner.Test("with thrown Exception", (Test test) =>
                    {
                        try
                        {
                            throw new Exception();
                        }
                        catch (Exception e)
                        {
                            test.AssertNotNull(e.StackTrace);
                        }
                    });

                    runner.Test("with created and then thrown Exception", (Test test) =>
                    {
                        Exception e = new Exception();
                        test.AssertNull(e.StackTrace);

                        try
                        {
                            throw e;
                        }
                        catch (Exception caughtException)
                        {
                            test.AssertSame(e, caughtException);
                            test.AssertNotNull(e.StackTrace);
                            test.AssertContains(e.StackTrace, "ExceptionsTests.cs:line 132");
                        }

                        test.AssertNotNull(e.StackTrace);
                    });
                });
            });
        }
    }
}
