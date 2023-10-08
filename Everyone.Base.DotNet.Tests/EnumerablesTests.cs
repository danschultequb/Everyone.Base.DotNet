using System;
using System.Collections.Generic;

namespace Everyone
{
    public static class EnumerablesTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Enumerables), () =>
            {
                runner.TestMethod("Contains(this IEnumerable<T>,U)", () =>
                {
                    void ContainsTest<T,U>(IEnumerable<T> values, U value, bool? expected = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { values, value }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                test.AssertEqual(expected, values.Contains(value));
                            });
                        });
                    }

                    ContainsTest(
                        values: (int[])null!,
                        value: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsTest(
                        values: (int[])null!,
                        value: 5,
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsTest(
                        values: (int[])null!,
                        value: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));

                    ContainsTest(
                        values: new int[0],
                        value: (string)null!,
                        expected: false);
                    ContainsTest(
                        values: new int[0],
                        value: 5,
                        expected: false);
                    ContainsTest(
                        values: new int[0],
                        value: "hello",
                        expected: false);

                    ContainsTest(
                        values: new[] { 1 },
                        value: (string)null!,
                        expected: false);
                    ContainsTest(
                        values: new[] { 1 },
                        value: 5,
                        expected: false);
                    ContainsTest(
                        values: new[] { 1 },
                        value: 1,
                        expected: true);
                    ContainsTest(
                        values: new[] { 1 },
                        value: "hello",
                        expected: false);

                    ContainsTest(
                        values: new[] { '\'', '\"' },
                        value: 'a',
                        expected: false);
                    ContainsTest(
                        values: new[] { '\'', '\"' },
                        value: '\'',
                        expected: true);
                    ContainsTest(
                        values: new[] { '\'', '\"' },
                        value: '\"',
                        expected: true);
                });

                runner.TestMethod("ContainsAll(this IEnumerable<T>,IEnumerable<U>)", () =>
                {
                    void ContainsAllTest<T, U>(IEnumerable<T> values, IEnumerable<U> toCheck, bool? expected = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { values, toCheck }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                test.AssertEqual(expected, values.ContainsAll(toCheck));
                            });
                        });
                    }

                    ContainsAllTest(
                        values: (int[])null!,
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAllTest(
                        values: (int[])null!,
                        toCheck: new[] { 5 },
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAllTest(
                        values: (int[])null!,
                        toCheck: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));

                    ContainsAllTest(
                        values: new int[0],
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: toCheck",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAllTest(
                        values: new int[0],
                        toCheck: new[] { 5 },
                        expected: false);
                    ContainsAllTest(
                        values: new int[0],
                        toCheck: "hello",
                        expected: false);

                    ContainsAllTest(
                        values: new[] { 1 },
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: toCheck",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAllTest(
                        values: new[] { 1 },
                        toCheck: new[] { 5 },
                        expected: false);
                    ContainsAllTest(
                        values: new[] { 1 },
                        toCheck: new[] { 1 },
                        expected: true);
                    ContainsAllTest(
                        values: new[] { 1 },
                        toCheck: new[] { 1, 2 },
                        expected: false);
                    ContainsAllTest(
                        values: new[] { 1, 3 },
                        toCheck: new[] { 1, 2 },
                        expected: false);
                    ContainsAllTest(
                        values: new[] { 1 },
                        toCheck: "hello",
                        expected: false);

                    ContainsAllTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { 'a' },
                        expected: false);
                    ContainsAllTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { '\'' },
                        expected: true);
                    ContainsAllTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { '\"' },
                        expected: true);
                });

                runner.TestMethod("ContainsAny(this IEnumerable<T>,IEnumerable<U>)", () =>
                {
                    void ContainsAnyTest<T, U>(IEnumerable<T> values, IEnumerable<U> toCheck, bool? expected = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { values, toCheck }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                test.AssertEqual(expected, values.ContainsAny(toCheck));
                            });
                        });
                    }

                    ContainsAnyTest(
                        values: (int[])null!,
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAnyTest(
                        values: (int[])null!,
                        toCheck: new[] { 5 },
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAnyTest(
                        values: (int[])null!,
                        toCheck: "hello",
                        expectedException: new PreConditionFailure(
                            "Expression: values",
                            "Expected: not null",
                            "Actual:   null"));

                    ContainsAnyTest(
                        values: new int[0],
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: toCheck",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAnyTest(
                        values: new int[0],
                        toCheck: new[] { 5 },
                        expected: false);
                    ContainsAnyTest(
                        values: new int[0],
                        toCheck: "hello",
                        expected: false);

                    ContainsAnyTest(
                        values: new[] { 1 },
                        toCheck: (string)null!,
                        expectedException: new PreConditionFailure(
                            "Expression: toCheck",
                            "Expected: not null",
                            "Actual:   null"));
                    ContainsAnyTest(
                        values: new[] { 1 },
                        toCheck: new[] { 5 },
                        expected: false);
                    ContainsAnyTest(
                        values: new[] { 1 },
                        toCheck: new[] { 1 },
                        expected: true);
                    ContainsAnyTest(
                        values: new[] { 1 },
                        toCheck: new[] { 1, 2 },
                        expected: true);
                    ContainsAnyTest(
                        values: new[] { 1, 3 },
                        toCheck: new[] { 1, 2 },
                        expected: true);
                    ContainsAnyTest(
                        values: new[] { 1 },
                        toCheck: "hello",
                        expected: false);

                    ContainsAnyTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { 'a' },
                        expected: false);
                    ContainsAnyTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { '\'' },
                        expected: true);
                    ContainsAnyTest(
                        values: new[] { '\'', '\"' },
                        toCheck: new[] { '\"' },
                        expected: true);
                });

                runner.TestMethod("ToString(this IEnumerable,Func<object?,string>?)", () =>
                {
                    runner.Test("with null and null", (Test test) =>
                    {
                        test.AssertEqual("null", Enumerables.ToString(null, null));
                    });

                    runner.Test("with null and non-null", (Test test) =>
                    {
                        test.AssertEqual("null", Enumerables.ToString(null, value => value?.ToString() ?? "NULL"));
                    });

                    runner.Test("with empty and null", (Test test) =>
                    {
                        test.AssertEqual("[]", Enumerables.ToString(new int[0], null));
                    });

                    runner.Test("with empty and non-null", (Test test) =>
                    {
                        test.AssertEqual("[]", Enumerables.ToString(new int[0], value => value?.ToString() ?? "NULL"));
                    });

                    runner.Test("with one non-null value and null", (Test test) =>
                    {
                        test.AssertEqual("[1]", Enumerables.ToString(new[] { 1 }, null));
                    });

                    runner.Test("with one non-null value and non-null", (Test test) =>
                    {
                        test.AssertEqual("[1]", Enumerables.ToString(new[] { 1 }, value => value?.ToString() ?? "NULL"));
                    });

                    runner.Test("with two non-null values and null", (Test test) =>
                    {
                        test.AssertEqual("[1,2]", Enumerables.ToString(new[] { 1, 2 }, null));
                    });

                    runner.Test("with two non-null values and non-null", (Test test) =>
                    {
                        test.AssertEqual("[1,2]", Enumerables.ToString(new[] { 1, 2 }, value => value?.ToString() ?? "NULL"));
                    });

                    runner.Test("with one null value and null", (Test test) =>
                    {
                        test.AssertEqual("[null]", Enumerables.ToString(new object?[] { null }, null));
                    });

                    runner.Test("with one null value and non-null", (Test test) =>
                    {
                        test.AssertEqual("[NULL]", Enumerables.ToString(new object?[] { null }, value => value?.ToString() ?? "NULL"));
                    });

                    runner.Test("with two-layer null list and null", (Test test) =>
                    {
                        test.AssertEqual("[System.Object[]]", Enumerables.ToString(new[] { new object?[] { null } }, null));
                    });

                    runner.Test("with two-layer null list and non-null", (Test test) =>
                    {
                        test.AssertEqual("[System.Object[]]", Enumerables.ToString(new[] { new object?[] { null } }, value => value?.ToString() ?? "NULL"));
                    });
                });
            });
        }
    }
}
