using System;
using System.Linq;

namespace Everyone
{
    public static class PostConditionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(PostCondition), () =>
            {
                runner.TestGroup("AssertTrue(bool?,string?)", () =>
                {
                    void AssertTrueErrorTest(bool? value, string? expression, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => PostCondition.AssertTrue(value, expression),
                                expected);
                        });
                    }

                    AssertTrueErrorTest(
                        value: null,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueErrorTest(
                        value: null,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   null"));
                    AssertTrueErrorTest(
                        value: false,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: True",
                            "Actual:   False"));
                    AssertTrueErrorTest(
                        value: false,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: True",
                            "Actual:   False"));

                    void AssertTrueTest(bool? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PostCondition.AssertTrue(value, expression);
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
                            test.AssertThrows(() => PostCondition.AssertFalse(value, expression),
                                expected);
                        });
                    }

                    AssertFalseErrorTest(
                        value: null,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseErrorTest(
                        value: null,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   null"));
                    AssertFalseErrorTest(
                        value: true,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: False",
                            "Actual:   True"));
                    AssertFalseErrorTest(
                        value: true,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: False",
                            "Actual:   True"));

                    void AssertFalseTest(bool? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PostCondition.AssertFalse(value, expression);
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
                            test.AssertThrows(() => PostCondition.AssertNull(value, expression),
                                expected);
                        });
                    }

                    AssertNullErrorTest(
                        value: 1,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullErrorTest(
                        value: 1,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   1"));
                    AssertNullErrorTest(
                        value: true,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: null",
                            "Actual:   True"));
                    AssertNullErrorTest(
                        value: true,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   True"));
                    AssertNullErrorTest(
                        value: "",
                        expression: null,
                        new PostConditionFailure(
                            "Expected: null",
                            "Actual:   \"\""));
                    AssertNullErrorTest(
                        value: "",
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: null",
                            "Actual:   \"\""));

                    void AssertNullTest(object? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PostCondition.AssertNull(value, expression);
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
                            test.AssertThrows(() => PostCondition.AssertNotNull(value, expression),
                                expected);
                        });
                    }

                    AssertNotNullErrorTest(
                        value: null,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: not null",
                            "Actual:       null"));
                    AssertNotNullErrorTest(
                        value: null,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: not null",
                            "Actual:       null"));

                    void AssertNotNullTest(object? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PostCondition.AssertNotNull(value, expression);
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
                            test.AssertThrows(() => PostCondition.AssertNotNullAndNotEmpty(value, expression),
                                expected);
                        });
                    }

                    AssertNotNullAndNotEmptyErrorTest(
                        value: null,
                        expression: null,
                        new PostConditionFailure(
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: null,
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: "",
                        expression: null,
                        new PostConditionFailure(
                            "Expected: not null and not empty",
                            "Actual:   \"\""));
                    AssertNotNullAndNotEmptyErrorTest(
                        value: "",
                        expression: "abc",
                        new PostConditionFailure(
                            "Expression: abc",
                            "Expected: not null and not empty",
                            "Actual:   \"\""));

                    void AssertNotNullAndNotEmptyTest(string? value, string? expression)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, expression }.Select(runner.ToString))}", (Test test) =>
                        {
                            PostCondition.AssertNotNull(value, expression);
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
            });
        }
    }
}
