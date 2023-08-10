using System;
using System.Linq;

namespace everyone
{
    public static class BasicAssertMessageFunctionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(BasicAssertMessageFunctions), () =>
            {
                runner.TestGroup("ExpectedSame(T?,U?,string?,string?)", () =>
                {
                    void ExpectedSameTest<T,U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedSame(expected, actual, message, newLine));
                        });
                    }

                    ExpectedSameTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: same as 1",
                            "Actual:           1"));
                    ExpectedSameTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: same as 1",
                            "Actual:           2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:           2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:           2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:           2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:           2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:           2"));
                });

                runner.TestGroup("ExpectedNotSame(T?,U?,string?,string?)", () =>
                {
                    void ExpectedNotSameTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedNotSame(expected, actual, message, newLine));
                        });
                    }

                    ExpectedNotSameTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not same as 1",
                            "Actual:               1"));
                    ExpectedNotSameTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not same as 1",
                            "Actual:               2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:               2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:               2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:               2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:               2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:               2"));
                });

                runner.TestGroup("ExpectedEqual(T?,U?,string?,string?)", () =>
                {
                    void ExpectedEqualTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedEqual(expected, actual, message, newLine));
                        });
                    }

                    ExpectedEqualTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: 1",
                            "Actual:   1"));
                    ExpectedEqualTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "   ", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message:    ",
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: 1",
                            "Actual:   2"));
                    ExpectedEqualTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: 1",
                            "Actual:   2"));
                });

                runner.TestGroup("ExpectedNotEqual(T?,U?,string?,string?)", () =>
                {
                    void ExpectedNotEqualTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedNotEqual(expected, actual, message, newLine));
                        });
                    }

                    ExpectedNotEqualTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:       1"));
                    ExpectedNotEqualTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "   ", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message:    ",
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:       2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:       2"));
                });
            });
        }
    }
}
