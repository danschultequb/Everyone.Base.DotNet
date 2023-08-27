using System;
using System.Linq;

namespace Everyone
{
    public static class ConditionTests
    {
        public static Assertions CreateCondition()
        {
            return Assertions.Create((string message) => new PreConditionFailure(message));
        }

        public static void Test(TestRunner runner)
        {
            runner.TestType<Assertions>(() =>
            {
                runner.TestMethod("Create(Func<string,Exception>,AssertMessageFunctions?,CompareFunctions?)", () =>
                {
                    void CreateTest(Func<string,Exception>? createExceptionFunction, AssertMessageFunctions? assertMessageFunctions, CompareFunctions? compareFunctions, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { createExceptionFunction, assertMessageFunctions, compareFunctions }.Map(x => x == null ? "null" : "not null"))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                Assertions.Create(createExceptionFunction!, assertMessageFunctions, compareFunctions);
                            });
                        });
                    }

                    CreateTest(
                        createExceptionFunction: null,
                        assertMessageFunctions: null,
                        compareFunctions: null,
                        expectedException: new ArgumentNullException("createExceptionFunction"));
                    CreateTest(
                        createExceptionFunction: (string message) => new PreConditionFailure(message),
                        assertMessageFunctions: null,
                        compareFunctions: null);
                    CreateTest(
                        createExceptionFunction: (string message) => new PreConditionFailure(message),
                        assertMessageFunctions: AssertMessageFunctions.Create(),
                        compareFunctions: null);
                    CreateTest(
                        createExceptionFunction: (string message) => new PreConditionFailure(message),
                        assertMessageFunctions: null,
                        compareFunctions: CompareFunctions.Create());
                    CreateTest(
                        createExceptionFunction: (string message) => new PreConditionFailure(message),
                        assertMessageFunctions: AssertMessageFunctions.Create(),
                        compareFunctions: CompareFunctions.Create());
                });

                runner.TestMethod("Fail(string?)", () =>
                {
                    void FailTest(string? message, Exception expectedException)
                    {
                        runner.Test($"with {runner.ToString(message)}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            test.AssertThrows(expectedException, () => condition.Fail(message));
                        });
                    }

                    FailTest(
                        message: null,
                        expectedException: new PreConditionFailure());
                    FailTest(
                        message: "",
                        expectedException: new PreConditionFailure(""));
                    FailTest(
                        message: "hello",
                        expectedException: new PreConditionFailure("hello"));
                });

                runner.TestMethod("AssertTrue(bool?,string?,string?)", () =>
                {
                    void AssertTrueTest(bool? value, string? expression = null, string? message = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression, message }.Select(runner.ToString))}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            
                            test.AssertThrows(expectedException, () =>
                            {
                                condition.AssertTrue(
                                    value: value,
                                    expression: expression,
                                    message: message);
                            });
                        });
                    }

                    AssertTrueTest(
                        value: null,
                        expectedException: new PreConditionFailure(
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: null,
                        expression: "",
                        expectedException: new PreConditionFailure(
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: null,
                        expression: "abc",
                        expectedException: new PreConditionFailure(
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: null,
                        message: "",
                        expectedException: new PreConditionFailure(
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: null,
                        message: "def",
                        expectedException: new PreConditionFailure(
                            "Message: def",
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: null,
                        expression: "abc",
                        message: "def",
                        expectedException: new PreConditionFailure(
                            "Message: def",
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueTest(
                        value: false,
                        expectedException: new PreConditionFailure(
                            "Expected: True",
                            "Actual:   False"));

                    AssertTrueTest(
                        value: true);
                    AssertTrueTest(
                        value: true,
                        expression: "abc");
                    AssertTrueTest(
                        value: true,
                        message: "def");
                    AssertTrueTest(
                        value: true,
                        expression: "abc",
                        message: "def");
                });

                runner.TestMethod("AssertTrue(bool?,AssertParameters)", () =>
                {
                    void AssertTrueTest(bool? value, AssertParameters? parameters, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(runner.ToString(value), parameters)}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            test.AssertThrows(expectedException, () =>
                            {
                                condition.AssertTrue(
                                    value: value,
                                    parameters: parameters);
                            });
                        });
                    }

                    AssertTrueTest(
                        value: false,
                        parameters: null,
                        expectedException: new PreConditionFailure(
                            "Expected: True",
                            "Actual:   False"));
                });

                runner.TestMethod("AssertFalse(bool?,string?,string?)", () =>
                {
                    void AssertFalseTest(bool? value, string? expression = null, string? message = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression, message }.Select(runner.ToString))}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            test.AssertThrows(expectedException, () =>
                            {
                                condition.AssertFalse(
                                    value: value,
                                    expression: expression,
                                    message: message);
                            });
                        });
                    }

                    AssertFalseTest(
                        value: null,
                        expectedException: new PreConditionFailure(
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: null,
                        expression: "",
                        expectedException: new PreConditionFailure(
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: null,
                        expression: "abc",
                        expectedException: new PreConditionFailure(
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: null,
                        message: "",
                        expectedException: new PreConditionFailure(
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: null,
                        message: "def",
                        expectedException: new PreConditionFailure(
                            "Message: def",
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: null,
                        expression: "abc",
                        message: "def",
                        expectedException: new PreConditionFailure(
                            "Message: def",
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseTest(
                        value: true,
                        expectedException: new PreConditionFailure(
                            "Expected: False",
                            "Actual:   True"));

                    AssertFalseTest(
                        value: false);
                    AssertFalseTest(
                        value: false,
                        expression: "abc");
                    AssertFalseTest(
                        value: false,
                        message: "def");
                    AssertFalseTest(
                        value: false,
                        expression: "abc",
                        message: "def");
                });

                runner.TestMethod("AssertFalse(bool?,AssertParameters)", () =>
                {
                    void AssertFalseTest(bool? value, AssertParameters? parameters, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(runner.ToString(value), parameters)}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            test.AssertThrows(expectedException, () =>
                            {
                                condition.AssertFalse(
                                    value: value,
                                    parameters: parameters);
                            });
                        });
                    }

                    AssertFalseTest(
                        value: true,
                        parameters: null,
                        expectedException: new PreConditionFailure(
                            "Expected: False",
                            "Actual:   True"));
                });

                runner.TestMethod("AssertNull(object?)", () =>
                {
                    void AssertNullTest(object? value, string? expression = null, string? message = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression, message }.Select(runner.ToString))}", (Test test) =>
                        {
                            Assertions condition = ConditionTests.CreateCondition();
                            test.AssertThrows(expectedException, () =>
                            {
                                condition.AssertNull(value, expression, message);
                            });
                        });
                    }

                    AssertNullTest(
                        value: 1,
                        expectedException: new PreConditionFailure(
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullTest(
                        value: 1,
                        expression: "",
                        expectedException: new PreConditionFailure(
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullTest(
                        value: 1,
                        expression: "abc",
                        expectedException: new PreConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullTest(
                        value: 1,
                        message: "",
                        expectedException: new PreConditionFailure(
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullTest(
                        value: 1,
                        message: "abc",
                        expectedException: new PreConditionFailure(
                            "Message: abc",
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullTest(
                        value: true,
                        expectedException: new PreConditionFailure(
                            "Expected: null",
                            "Actual:   True"));
                    AssertNullTest(
                        value: "",
                        expectedException: new PreConditionFailure(
                            "Expected: null",
                            "Actual:   \"\""));

                    AssertNullTest(
                        value: null);
                    AssertNullTest(
                        value: null,
                        expression: "abc");
                });
            });
        }
    }
}
