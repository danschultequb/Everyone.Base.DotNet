using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyone
{
    public static class ErrorTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Error), () =>
            {
                runner.TestMethod("Create(string,StackTrace?)", () =>
                {
                    void CreateTest(string message, StackTrace? stackTrace, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(runner.ToString(message), stackTrace == null ? "null" : "non-null")}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Error error = Error.Create(message, stackTrace);
                                test.AssertNotNull(error);
                                test.AssertEqual(message, error.Message);
                                test.AssertSame(stackTrace, error.StackTrace);
                            });
                        });
                    }

                    CreateTest(
                        message: null!,
                        stackTrace: null,
                        expectedException: new PreConditionFailure(
                            "Expression: message",
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    CreateTest(
                        message: "",
                        stackTrace: null,
                        expectedException: new PreConditionFailure(
                            "Expression: message",
                            "Expected: not null and not empty",
                            "Actual:   \"\""));
                    CreateTest(
                        message: "hello",
                        stackTrace: null);
                    CreateTest(
                        message: "hello",
                        stackTrace: new StackTrace());
                });
            });
        }
    }
}
