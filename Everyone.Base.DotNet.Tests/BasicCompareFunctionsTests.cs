using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Everyone
{
    public static class BasicCompareFunctionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(BasicCompareFunctions), () =>
            {
                runner.Test("Create()", (Test test) =>
                {
                    BasicCompareFunctions functions = BasicCompareFunctions.Create();
                    test.AssertNotNull(functions);
                });

                runner.TestGroup("TupleEqual(ITuple?,ITuple?)", () =>
                {
                    void TupleEqualTest(ITuple? lhs, ITuple? rhs, bool expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            test.AssertEqual(expected, functions.TupleEqual(lhs, rhs));
                        });
                    }

                    TupleEqualTest(null, null, true);
                    TupleEqualTest(null, Tuple.Create(1), false);
                    TupleEqualTest(null, Tuple.Create(1, "2"), false);

                    TupleEqualTest(Tuple.Create(1), null, false);
                    TupleEqualTest(Tuple.Create(1), Tuple.Create(1), true);
                    TupleEqualTest(Tuple.Create(1), Tuple.Create(true), false);
                    TupleEqualTest(Tuple.Create(1), Tuple.Create(1, "2"), false);

                    TupleEqualTest(Tuple.Create(1, "2"), null, false);
                    TupleEqualTest(Tuple.Create(1, "2"), Tuple.Create(1), false);
                    TupleEqualTest(Tuple.Create(1, "2"), Tuple.Create(true), false);
                    TupleEqualTest(Tuple.Create(1, "2"), Tuple.Create(1, "2"), true);
                });

                runner.TestGroup("ExceptionEqual(Exception?,Exception?)", () =>
                {
                    void ExceptionEqualTest(Exception? lhs, Exception? rhs, bool expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            test.AssertEqual(expected, functions.ExceptionEqual(lhs, rhs));
                        });
                    }

                    ExceptionEqualTest(null, null, true);
                    ExceptionEqualTest(null, new Exception(), false);
                    ExceptionEqualTest(null, new Exception("b"), false);
                    ExceptionEqualTest(null, new IOException("b"), false);

                    ExceptionEqualTest(new Exception(), null, false);
                    ExceptionEqualTest(new Exception(), new Exception(), true);
                    ExceptionEqualTest(new Exception(), new Exception("b"), false);
                    ExceptionEqualTest(new Exception(), new IOException("b"), false);

                    ExceptionEqualTest(new Exception("a"), null, false);
                    ExceptionEqualTest(new Exception("a"), new Exception(), false);
                    ExceptionEqualTest(new Exception("a"), new Exception("a"), true);
                    ExceptionEqualTest(new Exception("a"), new Exception("b"), false);
                    ExceptionEqualTest(new Exception("a"), new IOException("b"), false);

                    ExceptionEqualTest(new IOException("a"), null, false);
                    ExceptionEqualTest(new IOException("a"), new Exception(), false);
                    ExceptionEqualTest(new IOException("a"), new Exception("a"), false);
                    ExceptionEqualTest(new IOException("a"), new Exception("b"), false);
                    ExceptionEqualTest(new IOException("a"), new IOException("a"), true);
                    ExceptionEqualTest(new IOException("a"), new IOException("b"), false);
                });

                runner.TestGroup("AddEqualFunction(Func<T?,U?,bool>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicCompareFunctions functions = BasicCompareFunctions.Create();
                        test.AssertThrows(new ArgumentNullException("equalFunction"), () =>
                        {
                            functions.AddEqualFunction<int,bool>(null!);
                        });
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        BasicCompareFunctions functions = BasicCompareFunctions.Create();

                        test.AssertFalse(functions.AreEqual(0, false));
                        test.AssertFalse(functions.AreEqual(0, true));
                        test.AssertFalse(functions.AreEqual(1, false));
                        test.AssertFalse(functions.AreEqual(1, true));
                        
                        using (Disposable addEqualFunctionResult = functions.AddEqualFunction((int lhs, bool rhs) => (lhs == 0) == (rhs == false)))
                        {
                            test.AssertNotNull(addEqualFunctionResult);
                            test.AssertFalse(addEqualFunctionResult.Disposed);

                            test.AssertTrue(functions.AreEqual(0, false));
                            test.AssertFalse(functions.AreEqual(0, true));
                            test.AssertFalse(functions.AreEqual(1, false));
                            test.AssertTrue(functions.AreEqual(1, true));
                            test.AssertFalse(functions.AreEqual(2, false));
                            test.AssertTrue(functions.AreEqual(2, true));
                            test.AssertFalse(functions.AreEqual(3, false));
                            test.AssertTrue(functions.AreEqual(3, true));

                            using (Disposable addEqualFunctionResult2 = functions.AddEqualFunction((int lhs, bool rhs) => (lhs % 2 == 0) == (rhs == false)))
                            {
                                test.AssertNotNull(addEqualFunctionResult2);
                                test.AssertFalse(addEqualFunctionResult2.Disposed);

                                test.AssertTrue(functions.AreEqual(0, false));
                                test.AssertFalse(functions.AreEqual(0, true));
                                test.AssertFalse(functions.AreEqual(1, false));
                                test.AssertTrue(functions.AreEqual(1, true));
                                test.AssertTrue(functions.AreEqual(2, false));
                                test.AssertFalse(functions.AreEqual(2, true));
                                test.AssertFalse(functions.AreEqual(3, false));
                                test.AssertTrue(functions.AreEqual(3, true));
                            }
                        }

                        test.AssertFalse(functions.AreEqual(0, false));
                        test.AssertFalse(functions.AreEqual(0, true));
                        test.AssertFalse(functions.AreEqual(1, false));
                        test.AssertFalse(functions.AreEqual(1, true));
                    });
                });

                runner.TestGroup("Equal(T?,U?)", () =>
                {
                    void EqualTest<T,U>(T? lhs, U? rhs, bool expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            test.AssertEqual(expected, functions.AreEqual(lhs, rhs));
                        });
                    }

                    EqualTest<string,int?>(null, null, true);
                    EqualTest("a", 5, false);
                    EqualTest(new[] { 1, 2, 3 }, new List<int> { 1, 2, 3 }, true);
                    EqualTest(new Exception("abc"), new Exception("abc"), true);
                    EqualTest(new Exception("abc"), new Exception("def"), false);
                    EqualTest(new Exception("abc"), new IOException("abc"), false);
                });
            });
        }
    }
}
