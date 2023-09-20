using System;
using System.Diagnostics;

namespace Everyone
{
    public static class UncaughtExceptionErrorTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<UncaughtExceptionError>(() =>
            {
                runner.TestMethod("Create(Exception)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => UncaughtExceptionError.Create(exception: null!),
                            new PreConditionFailure(
                                "Expression: exception",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        Exception uncaughtException = new Exception("abc");
                        UncaughtExceptionError error = UncaughtExceptionError.Create(uncaughtException);
                        test.AssertNotNull(error);
                        test.AssertSame(uncaughtException, error.UncaughtException);
                        test.AssertEqual("abc", uncaughtException.Message);
                        test.AssertNull(error.StackTrace);
                    });
                });
            });
        }
    }
}
