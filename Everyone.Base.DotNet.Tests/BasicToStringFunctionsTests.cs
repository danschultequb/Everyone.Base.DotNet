using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Everyone
{
    public static class BasicToStringFunctionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<BasicToStringFunctions>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    BasicToStringFunctions functions = BasicToStringFunctions.Create();
                    test.AssertNotNull(functions);
                });

                runner.TestMethod("CharacterToString(char?)", () =>
                {
                    void CharacterToStringTest(char? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.CharacterToString(value));
                        });
                    }

                    CharacterToStringTest(null, "null");
                    CharacterToStringTest('a', "'a'");
                    CharacterToStringTest('b', "'b'");
                    CharacterToStringTest('\r', "'\\r'");
                    CharacterToStringTest('\t', "'\\t'");
                    CharacterToStringTest('\n', "'\\n'");
                    CharacterToStringTest('\v', "'\\v'");
                });

                runner.TestMethod("StringToString(string?)", () =>
                {
                    void StringToStringTest(string? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.StringToString(value));
                        });
                    }

                    StringToStringTest(null, "null");
                    StringToStringTest("", "\"\"");
                    StringToStringTest("abc", "\"abc\"");
                    StringToStringTest("a\r\nb", "\"a\\r\\nb\"");
                });

                runner.TestMethod("EnumerableToString(IEnumerable?)", () =>
                {
                    void EnumerableToStringTest(IEnumerable? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.EnumerableToString(value));
                        });
                    }

                    EnumerableToStringTest(null, "null");
                    EnumerableToStringTest(new int[0], "[]");
                    EnumerableToStringTest(new[] { 1 }, "[1]");
                    EnumerableToStringTest(new[] { 1, 2, 3 }, "[1,2,3]");
                    EnumerableToStringTest(new[] { "1", "2", "3" }, "[\"1\",\"2\",\"3\"]");
                });

                runner.TestMethod("TupleToString(ITuple?)", () =>
                {
                    void TupleToStringTest(ITuple? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.TupleToString(value));
                        });
                    }

                    TupleToStringTest(null, "null");
                    TupleToStringTest(Tuple.Create(1), "(1)");
                    TupleToStringTest((1, 2, 3), "(1,2,3)");
                    TupleToStringTest(Tuple.Create("1", 2, 'c'), "(\"1\",2,'c')");
                });

                runner.TestMethod("ExceptionToString(Exception?)", () =>
                {
                    void ExceptionToStringTest(Exception? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.ExceptionToString(value));
                        });
                    }

                    ExceptionToStringTest(null, "null");
                    ExceptionToStringTest(new Exception(), "System.Exception: \"Exception of type 'System.Exception' was thrown.\"");
                    ExceptionToStringTest(new Exception("abc"), "System.Exception: \"abc\"");
                    ExceptionToStringTest(new ArgumentException("a"), "System.ArgumentException: \"a\"");
                });

                runner.TestMethod("ToString(T?)", () =>
                {
                    void ToStringTest<T>(T? value, string expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            BasicToStringFunctions functions = BasicToStringFunctions.Create();
                            test.AssertEqual(expected, functions.ToString(value));
                        });
                    }

                    ToStringTest<object>(null, "null");

                    ToStringTest<char?>(null, "null");
                    ToStringTest('a', "'a'");
                    ToStringTest('b', "'b'");
                    ToStringTest('\r', "'\\r'");
                    ToStringTest('\t', "'\\t'");
                    ToStringTest('\n', "'\\n'");
                    ToStringTest('\v', "'\\v'");
                    ToStringTest('\'', "'\\''");
                    ToStringTest('\"', "'\"'");

                    ToStringTest<string>(null, "null");
                    ToStringTest("", "\"\"");
                    ToStringTest("'", "\"'\"");
                    ToStringTest("\"", "\"\\\"\"");
                    ToStringTest("abc", "\"abc\"");
                    ToStringTest("a\r\nb", "\"a\\r\\nb\"");

                    ToStringTest<IEnumerable>(null, "null");
                    ToStringTest(new int[0], "[]");
                    ToStringTest(new[] { 1 }, "[1]");
                    ToStringTest(new[] { 1, 2, 3 }, "[1,2,3]");
                    ToStringTest(new[] { "1", "2", "3" }, "[\"1\",\"2\",\"3\"]");

                    ToStringTest<ITuple>(null, "null");
                    ToStringTest(Tuple.Create(1), "(1)");
                    ToStringTest((1, 2, 3), "(1,2,3)");
                    ToStringTest(Tuple.Create("1", 2, 'c'), "(\"1\",2,'c')");

                    ToStringTest<Exception>(null, "null");
                    ToStringTest(new Exception(), "System.Exception: \"Exception of type 'System.Exception' was thrown.\"");
                    ToStringTest(new Exception("abc"), "System.Exception: \"abc\"");
                    ToStringTest(new ArgumentException("a"), "System.ArgumentException: \"a\"");
                });

                runner.TestMethod("AddToStringFunction(Func<T?,string>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicToStringFunctions functions = BasicToStringFunctions.Create();
                        test.AssertThrows(new ArgumentNullException("toStringFunction"), () =>
                        {
                            functions.AddToStringFunction<int?>(null!);
                        });
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        BasicToStringFunctions functions = BasicToStringFunctions.Create();
                        test.AssertEqual("null", functions.ToString<int?>(null!));
                        test.AssertEqual("1", functions.ToString(1));
                        test.AssertEqual("2", functions.ToString(2));

                        using (Disposable addToStringFunctionResult = functions.AddToStringFunction((int? value) => value == null ? "null" : $"{value}.{value}"))
                        {
                            test.AssertNotNull(addToStringFunctionResult);
                            test.AssertTrue(addToStringFunctionResult.IsNotDisposed());

                            test.AssertEqual("null", functions.ToString<int?>(null!));
                            test.AssertEqual("1.1", functions.ToString(1));
                            test.AssertEqual("2.2", functions.ToString(2));

                            using (Disposable addToStringFunctionResult2 = functions.AddToStringFunction((int? value) => value == null ? "null" : $"{value}.{value}.{value}"))
                            {
                                test.AssertNotNull(addToStringFunctionResult2);
                                test.AssertTrue(addToStringFunctionResult2.IsNotDisposed());

                                test.AssertEqual("null", functions.ToString<int?>(null!));
                                test.AssertEqual("1.1.1", functions.ToString(1));
                                test.AssertEqual("2.2.2", functions.ToString(2));
                            }

                            test.AssertEqual("null", functions.ToString<int?>(null!));
                            test.AssertEqual("1.1", functions.ToString(1));
                            test.AssertEqual("2.2", functions.ToString(2));
                        }

                        test.AssertEqual("null", functions.ToString<int?>(null!));
                        test.AssertEqual("1", functions.ToString(1));
                        test.AssertEqual("2", functions.ToString(2));
                    });
                });
            });
        }
    }
}
