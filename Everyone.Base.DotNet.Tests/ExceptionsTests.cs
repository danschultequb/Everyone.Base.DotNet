using System;
using System.IO;

namespace Everyone
{
    public static class ExceptionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Exceptions), () =>
            {
                runner.TestMethod("UnwrapTo<T>(Exception)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => Exceptions.UnwrapTo<IOException>(null!),
                            new PreConditionFailure(
                                "Expression: value",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    void UnwrapToTest<T>(Exception value, T? expected) where T : Exception
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, typeof(T) }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Exceptions.UnwrapTo<T>(value!));
                        });
                    }

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
                        value: new Exception("abc"),
                        expected: null);
                    UnwrapToTest<PreConditionFailure>(
                        value: new AwaitException(new Exception("abc")),
                        expected: null);
                });

                runner.TestMethod("UnwrapTo(Exception,Type)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => Exceptions.UnwrapTo(null!, typeof(IOException)),
                            new PreConditionFailure(
                                "Expression: value",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    void UnwrapToTest(Exception value, Type targetType, Exception? expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, targetType }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Exceptions.UnwrapTo(value, targetType));
                        });
                    }

                    UnwrapToTest(
                        value: new PreConditionFailure("abc"),
                        targetType: typeof(PreConditionFailure),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest(
                        value: new AwaitException(new PreConditionFailure("abc")),
                        targetType: typeof(PreConditionFailure),
                        expected: new PreConditionFailure("abc"));
                    UnwrapToTest(
                        value: new Exception("fake-message", new PreConditionFailure("abc")),
                        targetType: typeof(PreConditionFailure),
                        expected: null);
                    UnwrapToTest(
                        value: new Exception("abc"),
                        targetType: typeof(PreConditionFailure),
                        expected: null);
                    UnwrapToTest(
                        value: new AwaitException(new Exception("abc")),
                        targetType: typeof(PreConditionFailure),
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
                            test.AssertContains(e.StackTrace, "ExceptionsTests.cs:line 116");
                        }

                        test.AssertNotNull(e.StackTrace);
                    });
                });
            });
        }
    }
}
