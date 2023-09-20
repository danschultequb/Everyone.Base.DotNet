using System;

namespace Everyone
{
    public static class AwaitExceptionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<AwaitException>(() =>
            {
                runner.TestMethod("Constructor(Exception)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => new AwaitException(innerException: null!),
                            new PreConditionFailure(
                                "Expression: innerException",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with a single-layer Exception", (Test test) =>
                    {
                        Exception innerException = new Exception("abc");
                        AwaitException exception = new AwaitException(innerException);
                        Exception exceptionInnerException = exception.InnerException;
                        test.AssertSame(innerException, exceptionInnerException);
                        test.AssertEqual("abc", exception.Message);
                    });

                    runner.Test("with a two-layer Exception", (Test test) =>
                    {
                        Exception innerException = new Exception("abc", new Exception("def"));
                        AwaitException exception = new AwaitException(innerException);
                        Exception exceptionInnerException = exception.InnerException;
                        test.AssertSame(innerException, exceptionInnerException);
                        test.AssertEqual("abc", exception.Message);
                    });

                    runner.Test("with a three-layer Exception", (Test test) =>
                    {
                        Exception innerException = new Exception("abc", new Exception("def", new Exception("ghi")));
                        AwaitException exception = new AwaitException(innerException);
                        Exception exceptionInnerException = exception.InnerException;
                        test.AssertSame(innerException, exceptionInnerException);
                        test.AssertEqual("abc", exception.Message);
                    });
                });
            });
        }
    }
}
