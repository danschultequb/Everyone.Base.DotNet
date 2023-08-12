using System;
using System.Linq;

namespace Everyone
{
    public static class PreConditionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(PreCondition), () =>
            {
                runner.TestGroup("AssertTrue(bool?,string?)", () =>
                {
                    void AssertTrueErrorTest(bool? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PreCondition.AssertTrue(value, expression),
                                expected);
                        });
                    }

                    AssertTrueErrorTest(
                        value: null,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueErrorTest(
                        value: null,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueErrorTest(
                        value: false,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: True",
                            "Actual:   False"));
                    AssertTrueErrorTest(
                        value: false,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   False"));

                    void AssertTrueTest(bool? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PreCondition.AssertTrue(value, expression);
                        });
                    }

                    AssertTrueTest(
                        value: true,
                        expression: null);
                    AssertTrueTest(
                        value: true,
                        expression: "abc");
                });

                runner.TestGroup("AssertFalse(bool?,string?)", () =>
                {
                    void AssertFalseErrorTest(bool? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PreCondition.AssertFalse(value, expression),
                                expected);
                        });
                    }

                    AssertFalseErrorTest(
                        value: null,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseErrorTest(
                        value: null,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseErrorTest(
                        value: true,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: False",
                            "Actual:   True"));
                    AssertFalseErrorTest(
                        value: true,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   True"));

                    void AssertFalseTest(bool? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PreCondition.AssertFalse(value, expression);
                        });
                    }

                    AssertFalseTest(
                        value: false,
                        expression: null);
                    AssertFalseTest(
                        value: false,
                        expression: "abc");
                });

                runner.TestGroup("AssertNull(object?,string?)", () =>
                {
                    void AssertNullErrorTest(object? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PreCondition.AssertNull(value, expression),
                                expected);
                        });
                    }

                    AssertNullErrorTest(
                        value: 1,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullErrorTest(
                        value: 1,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullErrorTest(
                        value: true,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: null",
                            "Actual:   True"));
                    AssertNullErrorTest(
                        value: true,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   True"));
                    AssertNullErrorTest(
                        value: "",
                        expression: null,
                        new PreConditionFailure(
                            "Expected: null",
                            "Actual:   \"\""));
                    AssertNullErrorTest(
                        value: "",
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   \"\""));

                    void AssertNullTest(object? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PreCondition.AssertNull(value, expression);
                        });
                    }

                    AssertNullTest(
                        value: null,
                        expression: null);
                    AssertNullTest(
                        value: null,
                        expression: "abc");
                });

                runner.TestGroup("AssertNotNull(object?,string?)", () =>
                {
                    void AssertNotNullErrorTest(object? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PreCondition.AssertNotNull(value, expression),
                                expected);
                        });
                    }

                    AssertNotNullErrorTest(
                        value: null,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: not null",
                            "Actual:       null"));
                    AssertNotNullErrorTest(
                        value: null,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: not null",
                            "Actual:       null"));

                    void AssertNotNullTest(object? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PreCondition.AssertNotNull(value, expression);
                        });
                    }

                    AssertNotNullTest(
                        value: false,
                        expression: null);
                    AssertNotNullTest(
                        value: false,
                        expression: "abc");
                    AssertNotNullTest(
                        value: 2,
                        expression: null);
                    AssertNotNullTest(
                        value: 2,
                        expression: "abc");
                    AssertNotNullTest(
                        value: "a",
                        expression: null);
                    AssertNotNullTest(
                        value: "a",
                        expression: "abc");
                });

                runner.TestGroup("AssertNotNullAndNotEmpty(string?,string?)", () =>
                {
                    void AssertNotNullAndNotEmptyErrorTest(string? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PreCondition.AssertNotNullAndNotEmpty(value, expression),
                                expected);
                        });
                    }

                    AssertNotNullAndNotEmptyErrorTest(
                        value: null,
                        expression: null,
                        new PreConditionFailure(
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: null,
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: "",
                        expression: null,
                        new PreConditionFailure(
                            "Expected: not null and not empty",
                            "Actual:   \"\""));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: "",
                        expression: "abc",
                        new PreConditionFailure(
                            "Expression: abc",
                            "Expected: not null and not empty",
                            "Actual:   \"\""));

                    void AssertNotNullAndNotEmptyTest(string? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PreCondition.AssertNotNull(value, expression);
                        });
                    }

                    AssertNotNullAndNotEmptyTest(
                        value: " ",
                        expression: null);
                    AssertNotNullAndNotEmptyTest(
                        value: " ",
                        expression: "abc");
                    AssertNotNullAndNotEmptyTest(
                        value: "a",
                        expression: null);
                    AssertNotNullAndNotEmptyTest(
                        value: "a",
                        expression: "abc");
                });

                runner.TestGroup("AssertBetween<T,U,V>(T?,U?,V?,string?)", () =>
                {
                    void AssertBetweenTest<T,U,V>(T? lowerBound, U? value, V? upperBound, string? expression = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { lowerBound, value, upperBound, expression }.Map(runner.ToString))}", (Test test) =>
                        {
                            if (expectedException == null)
                            {
                                PreCondition.AssertBetween(
                                    lowerBound: lowerBound,
                                    value: value,
                                    upperBound: upperBound,
                                    expression: expression);
                            }
                            else
                            {
                                test.AssertThrows(expectedException, () =>
                                {
                                    PreCondition.AssertBetween(
                                        lowerBound: lowerBound,
                                        value: value,
                                        upperBound: upperBound,
                                        expression: expression);
                                });
                            }
                        });
                    }

                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 2,
                        upperBound: 3);
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 2,
                        upperBound: 3,
                        expression: "hello");
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 0,
                        upperBound: 3,
                        expectedException: new PreConditionFailure(
                            "Expected: between 1 and 3",
                            "Actual:           0"));
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 0,
                        upperBound: 3,
                        expression: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: hello",
                            "Expected: between 1 and 3",
                            "Actual:           0"));

                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '2',
                        upperBound: '3');
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '2',
                        upperBound: '3',
                        expression: "hello");
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '0',
                        upperBound: '3',
                        expectedException: new PreConditionFailure(
                            "Expected: between '1' and '3'",
                            "Actual:           '0'"));
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '0',
                        upperBound: '3',
                        expression: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: hello",
                            "Expected: between '1' and '3'",
                            "Actual:           '0'"));

                    AssertBetweenTest(
                        lowerBound: "no compare function",
                        value: false,
                        upperBound: 40,
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Boolean."));
                    AssertBetweenTest(
                        lowerBound: "no compare function",
                        value: false,
                        upperBound: 40,
                        expression: "hello",
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Boolean."));

                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "function",
                        upperBound: 40,
                        expectedException: new PreConditionFailure(
                            "Expected: between \"no compare\" and 40",
                            "Actual:           \"function\""));
                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "function",
                        upperBound: 40,
                        expression: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: hello",
                            "Expected: between \"no compare\" and 40",
                            "Actual:           \"function\""));

                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "oops",
                        upperBound: 40,
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Int32."));
                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "oops",
                        upperBound: 40,
                        expression: "hello",
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Int32."));
                });
            });
        }
    }
}
