using System;
using System.Linq;

namespace Everyone
{
    public static class BasicAssertMessageFunctionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<BasicAssertMessageFunctions>(() =>
            {
                runner.TestMethod("ExpectedTrue(bool?,AssertMessageParameters?)", () =>
                {
                    void ExpectedTrueTest(bool? value, AssertParameters? parameters, string expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, parameters }.Select(runner.ToString))}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expected, functions.ExpectedTrue(value, parameters));
                        });
                    }

                    ExpectedTrueTest(
                        value: null,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: True",
                            "Actual:   null"));
                    ExpectedTrueTest(
                        value: true,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: True",
                            "Actual:   True"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: True",
                            "Actual:   False"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: new AssertParameters {},
                        expected: string.Join(Environment.NewLine,
                            "Expected: True",
                            "Actual:   False"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expected: True",
                            "Actual:   False"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: new AssertParameters { Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Expression: a.Value",
                            "Expected: True",
                            "Actual:   False"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!", Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expression: a.Value",
                            "Expected: True",
                            "Actual:   False"));
                    ExpectedTrueTest(
                        value: false,
                        parameters: new AssertParameters { NewLine = "\n" },
                        expected: string.Join("\n",
                            "Expected: True",
                            "Actual:   False"));
                });

                runner.TestMethod("ExpectedFalse(bool?,AssertMessageParameters?)", () =>
                {
                    void ExpectedFalseTest(bool? value, AssertParameters? parameters, string expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, parameters }.Select(runner.ToString))}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expected, functions.ExpectedFalse(value, parameters));
                        });
                    }

                    ExpectedFalseTest(
                        value: null,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: False",
                            "Actual:   null"));
                    ExpectedFalseTest(
                        value: true,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: False",
                            "Actual:   True"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: False",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { },
                        expected: string.Join(Environment.NewLine,
                            "Expected: False",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expected: False",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Expression: a.Value",
                            "Expected: False",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!", Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expression: a.Value",
                            "Expected: False",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { NewLine = "\n" },
                        expected: string.Join("\n",
                            "Expected: False",
                            "Actual:   False"));
                });

                runner.TestMethod("ExpectedNull(object?,AssertMessageParameters?)", () =>
                {
                    void ExpectedFalseTest(object? value, AssertParameters? parameters, string expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, parameters }.Select(runner.ToString))}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expected, functions.ExpectedNull(value, parameters));
                        });
                    }

                    ExpectedFalseTest(
                        value: null,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   null"));
                    ExpectedFalseTest(
                        value: true,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   True"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: null,
                        expected: string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { },
                        expected: string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expected: null",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Expression: a.Value",
                            "Expected: null",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { Message = "Hello!", Expression = "a.Value" },
                        expected: string.Join(Environment.NewLine,
                            "Message: Hello!",
                            "Expression: a.Value",
                            "Expected: null",
                            "Actual:   False"));
                    ExpectedFalseTest(
                        value: false,
                        parameters: new AssertParameters { NewLine = "\n" },
                        expected: string.Join("\n",
                            "Expected: null",
                            "Actual:   False"));
                });

                runner.TestMethod("ExpectedSame(T?,U?,string?,string?)", () =>
                {
                    void ExpectedSameTest<T,U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedSame(expected, actual, new AssertParameters
                            {
                                Message = message,
                                NewLine = newLine,
                            }));
                        });
                    }

                    ExpectedSameTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: same as 1",
                            "Actual:   1"));
                    ExpectedSameTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: same as 1",
                            "Actual:   2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:   2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:   2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:   2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:   2"));
                    ExpectedSameTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: same as 1",
                            "Actual:   2"));
                });

                runner.TestMethod("ExpectedNotSame(T?,U?,string?,string?)", () =>
                {
                    void ExpectedNotSameTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedNotSame(expected, actual, new AssertParameters
                            {
                                Message = message,
                                NewLine = newLine
                            }));
                        });
                    }

                    ExpectedNotSameTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not same as 1",
                            "Actual:   1"));
                    ExpectedNotSameTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not same as 1",
                            "Actual:   2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:   2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:   2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:   2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:   2"));
                    ExpectedNotSameTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: not same as 1",
                            "Actual:   2"));
                });

                runner.TestMethod("ExpectedEqual(T?,U?,string?,string?)", () =>
                {
                    void ExpectedEqualTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedEqual(expected, actual,
                                new AssertParameters
                                {
                                    Message = message,
                                    NewLine = newLine
                                }));
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

                runner.TestMethod("ExpectedNotEqual(T?,U?,string?,string?)", () =>
                {
                    void ExpectedNotEqualTest<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string expectedText = "")
                    {
                        runner.Test($"with {new object?[] { expected, actual, message, newLine }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            BasicAssertMessageFunctions functions = BasicAssertMessageFunctions.Create();
                            test.AssertEqual(expectedText, functions.ExpectedNotEqual(expected, actual, new AssertParameters
                            {
                                Message = message,
                                NewLine = newLine,
                            }));
                        });
                    }

                    ExpectedNotEqualTest(1, 1, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:   1"));
                    ExpectedNotEqualTest(1, 2, message: null, newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "   ", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message:    ",
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: null,
                        expectedText: string.Join(Environment.NewLine,
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "",
                        expectedText: string.Join(separator: "",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "\r\n",
                        expectedText: string.Join("\r\n",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "\n",
                        expectedText: string.Join("\n",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:   2"));
                    ExpectedNotEqualTest(1, 2, message: "hello there!", newLine: "_",
                        expectedText: string.Join("_",
                            "Message: hello there!",
                            "Expected: not 1",
                            "Actual:   2"));
                });
            });
        }
    }
}
