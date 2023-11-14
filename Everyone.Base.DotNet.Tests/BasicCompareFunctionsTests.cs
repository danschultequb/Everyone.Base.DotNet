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
            runner.TestType<BasicCompareFunctions>(() =>
            {
                runner.TestMethod("Create()", (Test test) =>
                {
                    BasicCompareFunctions functions = BasicCompareFunctions.Create();
                    test.AssertNotNull(functions);
                });

                runner.TestMethod("TupleEqual(ITuple?,ITuple?)", () =>
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

                runner.TestMethod("ExceptionEqual(Exception?,Exception?)", () =>
                {
                    void ExceptionEqualTest(Exception? expectedException, Exception? actualException, bool expected)
                    {
                        runner.Test($"with {Language.AndList(new[] { expectedException, actualException }.Map(runner.ToString))}", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            test.AssertEqual(expected, functions.ExceptionEqual(expectedException, actualException));
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

                    ExceptionEqualTest(new AwaitException(new Exception("abc")), null, false);
                    ExceptionEqualTest(new AwaitException(new Exception("abc")), new Exception(), false);
                    ExceptionEqualTest(new AwaitException(new Exception("abc")), new Exception("abc"), false);
                    ExceptionEqualTest(new AwaitException(new Exception("abc")), new AwaitException(new Exception()), false);
                    ExceptionEqualTest(new AwaitException(new Exception("abc")), new AwaitException(new Exception("abc")), true);
                    ExceptionEqualTest(new AwaitException(new Exception("abc")), new AwaitException(new IOException("abc")), false);

                    ExceptionEqualTest(null, new AwaitException(new Exception("abc")), false);
                    ExceptionEqualTest(new Exception("def"), new AwaitException(new Exception("abc")), false);
                    ExceptionEqualTest(new Exception("abc"), new AwaitException(new Exception("abc")), false);
                    ExceptionEqualTest(new IOException("abc"), new AwaitException(new Exception("abc")), false);
                    ExceptionEqualTest(new IOException("abc"), new AwaitException(new IOException("abc")), false);

                    ExceptionEqualTest(null, new Exception("fake-message", new Exception("abc")), false);
                    ExceptionEqualTest(new Exception("def"), new Exception("fake-message", new Exception("abc")), false);
                    ExceptionEqualTest(new Exception("abc"), new Exception("fake-message", new Exception("abc")), false);
                    ExceptionEqualTest(new IOException("abc"), new Exception("fake-message", new Exception("abc")), false);
                    ExceptionEqualTest(new IOException("abc"), new Exception("fake-message", new IOException("abc")), false);
                });

                runner.TestMethod("AddEqualFunction(Func<T?,U?,bool>)", () =>
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
                            test.AssertTrue(addEqualFunctionResult.IsNotDisposed());

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
                                test.AssertTrue(addEqualFunctionResult2.IsNotDisposed());

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

                runner.TestMethod("Equal(T?,U?)", () =>
                {
                    void EqualTest<T,U>(T? lhs, U? rhs, bool expected, string? message = null)
                    {
                        runner.Test($"with {runner.ToString(lhs)} ({Types.GetFullName(lhs)}) and {runner.ToString(rhs)} ({Types.GetFullName(rhs)})", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            test.AssertEqual(expected, functions.AreEqual(lhs, rhs), message: message);
                        });
                    }

                    EqualTest<string,int?>(null, null, true);
                    EqualTest("a", 5, false);
                    EqualTest(false, 5, false);

                    EqualTest((byte)1, (byte)1, true);
                    EqualTest((byte)2, (sbyte)2, true);
                    EqualTest((byte)3, (short)3, true);
                    EqualTest((byte)4, (ushort)4, true);
                    EqualTest((byte)5, (int)5, true);
                    EqualTest((byte)6, (uint)6, true);
                    EqualTest((byte)7, (long)7, true);
                    EqualTest((byte)8, (ulong)8, true);
                    EqualTest((byte)9, (nint)9, true);
                    EqualTest((byte)10, (nuint)10, true);
                    EqualTest((byte)11, (float)11, true);
                    EqualTest((byte)12, (double)12, true);
                    EqualTest((byte)13, (decimal)13, true);

                    EqualTest((sbyte)1, (byte)1, true);
                    EqualTest((sbyte)2, (sbyte)2, true);
                    EqualTest((sbyte)3, (short)3, true);
                    EqualTest((sbyte)4, (ushort)4, true);
                    EqualTest((sbyte)5, (int)5, true);
                    EqualTest((sbyte)6, (uint)6, true);
                    EqualTest((sbyte)7, (long)7, true);
                    EqualTest((sbyte)8, (ulong)8, false, "Ambiguous comparison");
                    EqualTest((sbyte)9, (nint)9, true);
                    EqualTest((sbyte)10, (nuint)10, false, "Ambiguous comparison");
                    EqualTest((sbyte)11, (float)11, true);
                    EqualTest((sbyte)12, (double)12, true);
                    EqualTest((sbyte)13, (decimal)13, true);

                    EqualTest((short)1, (byte)1, true);
                    EqualTest((short)2, (sbyte)2, true);
                    EqualTest((short)3, (short)3, true);
                    EqualTest((short)4, (ushort)4, true);
                    EqualTest((short)5, (int)5, true);
                    EqualTest((short)6, (uint)6, true);
                    EqualTest((short)7, (long)7, true);
                    EqualTest((short)8, (ulong)8, false, "Ambiguous comparison");
                    EqualTest((short)9, (nint)9, true);
                    EqualTest((short)10, (nuint)10, false, "Ambiguous comparison");
                    EqualTest((short)11, (float)11, true);
                    EqualTest((short)12, (double)12, true);
                    EqualTest((short)13, (decimal)13, true);

                    EqualTest((ushort)1, (byte)1, true);
                    EqualTest((ushort)2, (sbyte)2, true);
                    EqualTest((ushort)3, (short)3, true);
                    EqualTest((ushort)4, (ushort)4, true);
                    EqualTest((ushort)5, (int)5, true);
                    EqualTest((ushort)6, (uint)6, true);
                    EqualTest((ushort)7, (long)7, true);
                    EqualTest((ushort)8, (ulong)8, true);
                    EqualTest((ushort)9, (nint)9, true);
                    EqualTest((ushort)10, (nuint)10, true);
                    EqualTest((ushort)11, (float)11, true);
                    EqualTest((ushort)12, (double)12, true);
                    EqualTest((ushort)13, (decimal)13, true);

                    EqualTest((int)1, (byte)1, true);
                    EqualTest((int)2, (sbyte)2, true);
                    EqualTest((int)3, (short)3, true);
                    EqualTest((int)4, (ushort)4, true);
                    EqualTest((int)5, (int)5, true);
                    EqualTest((int)6, (uint)6, true);
                    EqualTest((int)7, (long)7, true);
                    EqualTest((int)8, (ulong)8, false, "Ambiguous comparison");
                    EqualTest((int)9, (nint)9, true);
                    EqualTest((int)10, (nuint)10, false, "Ambiguous comparison");
                    EqualTest((int)11, (float)11, true);
                    EqualTest((int)12, (double)12, true);
                    EqualTest((int)13, (decimal)13, true);

                    EqualTest((uint)1, (byte)1, true);
                    EqualTest((uint)2, (sbyte)2, true);
                    EqualTest((uint)3, (short)3, true);
                    EqualTest((uint)4, (ushort)4, true);
                    EqualTest((uint)5, (int)5, true);
                    EqualTest((uint)6, (uint)6, true);
                    EqualTest((uint)7, (long)7, true);
                    EqualTest((uint)8, (ulong)8, true);
                    EqualTest((uint)9, (nint)9, true);
                    EqualTest((uint)10, (nuint)10, true);
                    EqualTest((uint)11, (float)11, true);
                    EqualTest((uint)12, (double)12, true);
                    EqualTest((uint)13, (decimal)13, true);

                    EqualTest((long)1, (byte)1, true);
                    EqualTest((long)2, (sbyte)2, true);
                    EqualTest((long)3, (short)3, true);
                    EqualTest((long)4, (ushort)4, true);
                    EqualTest((long)5, (int)5, true);
                    EqualTest((long)6, (uint)6, true);
                    EqualTest((long)7, (long)7, true);
                    EqualTest((long)8, (ulong)8, false, "Ambiguous comparison");
                    EqualTest((long)9, (nint)9, true);
                    EqualTest((long)10, (nuint)10, false, "Ambiguous comparison");
                    EqualTest((long)11, (float)11, true);
                    EqualTest((long)12, (double)12, true);
                    EqualTest((long)13, (decimal)13, true);

                    EqualTest((ulong)1, (byte)1, true);
                    EqualTest((ulong)2, (sbyte)2, false, "Ambiguous comparison");
                    EqualTest((ulong)3, (short)3, false, "Ambiguous comparison");
                    EqualTest((ulong)4, (ushort)4, true);
                    EqualTest((ulong)5, (int)5, false, "Ambiguous comparison");
                    EqualTest((ulong)6, (uint)6, true);
                    EqualTest((ulong)7, (long)7, false, "Ambiguous comparison");
                    EqualTest((ulong)8, (ulong)8, true);
                    EqualTest((ulong)9, (nint)9, false, "Ambiguous comparison");
                    EqualTest((ulong)10, (nuint)10, true);
                    EqualTest((ulong)11, (float)11, true);
                    EqualTest((ulong)12, (double)12, true);
                    EqualTest((ulong)13, (decimal)13, true);

                    EqualTest((nint)1, (byte)1, true);
                    EqualTest((nint)2, (sbyte)2, true);
                    EqualTest((nint)3, (short)3, true);
                    EqualTest((nint)4, (ushort)4, true);
                    EqualTest((nint)5, (int)5, true);
                    EqualTest((nint)6, (uint)6, true);
                    EqualTest((nint)7, (long)7, true);
                    EqualTest((nint)8, (ulong)8, false, "Ambiguous comparison");
                    EqualTest((nint)9, (nint)9, true);
                    EqualTest((nint)10, (nuint)10, false, "Ambiguous comparison");
                    EqualTest((nint)11, (float)11, true);
                    EqualTest((nint)12, (double)12, true);
                    EqualTest((nint)13, (decimal)13, true);

                    EqualTest((nuint)1, (byte)1, true);
                    EqualTest((nuint)2, (sbyte)2, false, "Ambiguous comparison");
                    EqualTest((nuint)3, (short)3, false, "Ambiguous comparison");
                    EqualTest((nuint)4, (ushort)4, true);
                    EqualTest((nuint)5, (int)5, false, "Ambiguous comparison");
                    EqualTest((nuint)6, (uint)6, true);
                    EqualTest((nuint)7, (long)7, false, "Ambiguous comparison");
                    EqualTest((nuint)8, (ulong)8, true);
                    EqualTest((nuint)9, (nint)9, false, "Ambiguous comparison");
                    EqualTest((nuint)10, (nuint)10, true);
                    EqualTest((nuint)11, (float)11, true);
                    EqualTest((nuint)12, (double)12, true);
                    EqualTest((nuint)13, (decimal)13, true);

                    EqualTest((float)1, (byte)1, true);
                    EqualTest((float)2, (sbyte)2, true);
                    EqualTest((float)3, (short)3, true);
                    EqualTest((float)4, (ushort)4, true);
                    EqualTest((float)5, (int)5, true);
                    EqualTest((float)6, (uint)6, true);
                    EqualTest((float)7, (long)7, true);
                    EqualTest((float)8, (ulong)8, true);
                    EqualTest((float)9, (nint)9, true);
                    EqualTest((float)10, (nuint)10, true);
                    EqualTest((float)11, (float)11, true);
                    EqualTest((float)12, (double)12, true);
                    EqualTest((float)13, (decimal)13, true);

                    EqualTest((double)1, (byte)1, true);
                    EqualTest((double)2, (sbyte)2, true);
                    EqualTest((double)3, (short)3, true);
                    EqualTest((double)4, (ushort)4, true);
                    EqualTest((double)5, (int)5, true);
                    EqualTest((double)6, (uint)6, true);
                    EqualTest((double)7, (long)7, true);
                    EqualTest((double)8, (ulong)8, true);
                    EqualTest((double)9, (nint)9, true);
                    EqualTest((double)10, (nuint)10, true);
                    EqualTest((double)11, (float)11, true);
                    EqualTest((double)12, (double)12, true);
                    EqualTest((double)13, (decimal)13, true);

                    EqualTest((decimal)1, (byte)1, true);
                    EqualTest((decimal)2, (sbyte)2, true);
                    EqualTest((decimal)3, (short)3, true);
                    EqualTest((decimal)4, (ushort)4, true);
                    EqualTest((decimal)5, (int)5, true);
                    EqualTest((decimal)6, (uint)6, true);
                    EqualTest((decimal)7, (long)7, true);
                    EqualTest((decimal)8, (ulong)8, true);
                    EqualTest((decimal)9, (nint)9, true);
                    EqualTest((decimal)10, (nuint)10, true);
                    EqualTest((decimal)11, (float)11, true);
                    EqualTest((decimal)12, (double)12, true);
                    EqualTest((decimal)13, (decimal)13, true);

                    EqualTest(new[] { 1, 2, 3 }, List.Create(1, 2, 3), true);
                    EqualTest(new Exception("abc"), new Exception("abc"), true);
                    EqualTest(new Exception("abc"), new Exception("def"), false);
                    EqualTest(new Exception("abc"), new IOException("abc"), false);
                });

                runner.TestMethod("Compare<T,U>(T?,U?)", () =>
                {
                    void CompareTest<T,U>(T? lhs, U? rhs, Comparison? expectedComparison = null, Exception? expectedException = null, string? message = null)
                    {
                        runner.Test($"with {runner.ToString(lhs)} ({Types.GetFullName(lhs)}) and {runner.ToString(rhs)} ({Types.GetFullName(rhs)})", (Test test) =>
                        {
                            BasicCompareFunctions functions = BasicCompareFunctions.Create();
                            if (expectedComparison != null)
                            {
                                test.AssertEqual(expectedComparison, functions.Compare(lhs, rhs), message);
                            }

                            if (expectedException != null)
                            {
                                test.AssertThrows(expectedException, () =>
                                {
                                    functions.Compare(lhs, rhs);
                                });
                            }
                        });
                    }

                    CompareTest((object?)null, (object?)null, Comparison.Equal);

                    CompareTest((string?)null, "", Comparison.LessThan);
                    CompareTest("", (string?)null, Comparison.GreaterThan);
                    CompareTest("", "", Comparison.Equal);
                    CompareTest("", "a", Comparison.LessThan);
                    CompareTest("a", "", Comparison.GreaterThan);
                    CompareTest("a", "a", Comparison.Equal);
                    CompareTest("a", "b", Comparison.LessThan);
                    CompareTest("b", "a", Comparison.GreaterThan);

                    CompareTest((byte)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((byte)1, (byte)1, Comparison.Equal);
                    CompareTest((byte)1, (byte)2, Comparison.LessThan);
                    CompareTest((byte)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((byte)2, (sbyte)2, Comparison.Equal);
                    CompareTest((byte)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((byte)3, (short)2, Comparison.GreaterThan);
                    CompareTest((byte)3, (short)3, Comparison.Equal);
                    CompareTest((byte)3, (short)4, Comparison.LessThan);
                    CompareTest((byte)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((byte)4, (ushort)4, Comparison.Equal);
                    CompareTest((byte)4, (ushort)5, Comparison.LessThan);
                    CompareTest((byte)5, (int)4, Comparison.GreaterThan);
                    CompareTest((byte)5, (int)5, Comparison.Equal);
                    CompareTest((byte)5, (int)6, Comparison.LessThan);
                    CompareTest((byte)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((byte)6, (uint)6, Comparison.Equal);
                    CompareTest((byte)6, (uint)7, Comparison.LessThan);
                    CompareTest((byte)7, (long)6, Comparison.GreaterThan);
                    CompareTest((byte)7, (long)7, Comparison.Equal);
                    CompareTest((byte)7, (long)8, Comparison.LessThan);
                    CompareTest((byte)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((byte)8, (ulong)8, Comparison.Equal);
                    CompareTest((byte)8, (ulong)9, Comparison.LessThan);
                    CompareTest((byte)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((byte)9, (nint)9, Comparison.Equal);
                    CompareTest((byte)9, (nint)10, Comparison.LessThan);
                    CompareTest((byte)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((byte)10, (nuint)10, Comparison.Equal);
                    CompareTest((byte)10, (nuint)11, Comparison.LessThan);
                    CompareTest((byte)11, (float)10, Comparison.GreaterThan);
                    CompareTest((byte)11, (float)11, Comparison.Equal);
                    CompareTest((byte)11, (float)12, Comparison.LessThan);
                    CompareTest((byte)12, (double)11, Comparison.GreaterThan);
                    CompareTest((byte)12, (double)12, Comparison.Equal);
                    CompareTest((byte)12, (double)13, Comparison.LessThan);
                    CompareTest((byte)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((byte)13, (decimal)13, Comparison.Equal);
                    CompareTest((byte)13, (decimal)14, Comparison.LessThan);

                    CompareTest((sbyte)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((sbyte)1, (byte)1, Comparison.Equal);
                    CompareTest((sbyte)1, (byte)2, Comparison.LessThan);
                    CompareTest((sbyte)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((sbyte)2, (sbyte)2, Comparison.Equal);
                    CompareTest((sbyte)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((sbyte)3, (short)2, Comparison.GreaterThan);
                    CompareTest((sbyte)3, (short)3, Comparison.Equal);
                    CompareTest((sbyte)3, (short)4, Comparison.LessThan);
                    CompareTest((sbyte)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((sbyte)4, (ushort)4, Comparison.Equal);
                    CompareTest((sbyte)4, (ushort)5, Comparison.LessThan);
                    CompareTest((sbyte)5, (int)4, Comparison.GreaterThan);
                    CompareTest((sbyte)5, (int)5, Comparison.Equal);
                    CompareTest((sbyte)5, (int)6, Comparison.LessThan);
                    CompareTest((sbyte)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((sbyte)6, (uint)6, Comparison.Equal);
                    CompareTest((sbyte)6, (uint)7, Comparison.LessThan);
                    CompareTest((sbyte)7, (long)6, Comparison.GreaterThan);
                    CompareTest((sbyte)7, (long)7, Comparison.Equal);
                    CompareTest((sbyte)7, (long)8, Comparison.LessThan);
                    CompareTest((sbyte)8, (ulong)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UInt64."));
                    CompareTest((sbyte)8, (ulong)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UInt64."));
                    CompareTest((sbyte)8, (ulong)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UInt64."));
                    CompareTest((sbyte)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((sbyte)9, (nint)9, Comparison.Equal);
                    CompareTest((sbyte)9, (nint)10, Comparison.LessThan);
                    CompareTest((sbyte)10, (nuint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UIntPtr."));
                    CompareTest((sbyte)10, (nuint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UIntPtr."));
                    CompareTest((sbyte)10, (nuint)11, expectedException: new InvalidOperationException("No compare function found that matches the types System.SByte and System.UIntPtr."));
                    CompareTest((sbyte)11, (float)10, Comparison.GreaterThan);
                    CompareTest((sbyte)11, (float)11, Comparison.Equal);
                    CompareTest((sbyte)11, (float)12, Comparison.LessThan);
                    CompareTest((sbyte)12, (double)11, Comparison.GreaterThan);
                    CompareTest((sbyte)12, (double)12, Comparison.Equal);
                    CompareTest((sbyte)12, (double)13, Comparison.LessThan);
                    CompareTest((sbyte)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((sbyte)13, (decimal)13, Comparison.Equal);
                    CompareTest((sbyte)13, (decimal)14, Comparison.LessThan);

                    CompareTest((short)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((short)1, (byte)1, Comparison.Equal);
                    CompareTest((short)1, (byte)2, Comparison.LessThan);
                    CompareTest((short)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((short)2, (sbyte)2, Comparison.Equal);
                    CompareTest((short)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((short)3, (short)2, Comparison.GreaterThan);
                    CompareTest((short)3, (short)3, Comparison.Equal);
                    CompareTest((short)3, (short)4, Comparison.LessThan);
                    CompareTest((short)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((short)4, (ushort)4, Comparison.Equal);
                    CompareTest((short)4, (ushort)5, Comparison.LessThan);
                    CompareTest((short)5, (int)4, Comparison.GreaterThan);
                    CompareTest((short)5, (int)5, Comparison.Equal);
                    CompareTest((short)5, (int)6, Comparison.LessThan);
                    CompareTest((short)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((short)6, (uint)6, Comparison.Equal);
                    CompareTest((short)6, (uint)7, Comparison.LessThan);
                    CompareTest((short)7, (long)6, Comparison.GreaterThan);
                    CompareTest((short)7, (long)7, Comparison.Equal);
                    CompareTest((short)7, (long)8, Comparison.LessThan);
                    CompareTest((short)8, (ulong)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UInt64."));
                    CompareTest((short)8, (ulong)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UInt64."));
                    CompareTest((short)8, (ulong)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UInt64."));
                    CompareTest((short)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((short)9, (nint)9, Comparison.Equal);
                    CompareTest((short)9, (nint)10, Comparison.LessThan);
                    CompareTest((short)10, (nuint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UIntPtr."));
                    CompareTest((short)10, (nuint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UIntPtr."));
                    CompareTest((short)10, (nuint)11, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int16 and System.UIntPtr."));
                    CompareTest((short)11, (float)10, Comparison.GreaterThan);
                    CompareTest((short)11, (float)11, Comparison.Equal);
                    CompareTest((short)11, (float)12, Comparison.LessThan);
                    CompareTest((short)12, (double)11, Comparison.GreaterThan);
                    CompareTest((short)12, (double)12, Comparison.Equal);
                    CompareTest((short)12, (double)13, Comparison.LessThan);
                    CompareTest((short)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((short)13, (decimal)13, Comparison.Equal);
                    CompareTest((short)13, (decimal)14, Comparison.LessThan);

                    CompareTest((ushort)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((ushort)1, (byte)1, Comparison.Equal);
                    CompareTest((ushort)1, (byte)2, Comparison.LessThan);
                    CompareTest((ushort)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((ushort)2, (sbyte)2, Comparison.Equal);
                    CompareTest((ushort)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((ushort)3, (short)2, Comparison.GreaterThan);
                    CompareTest((ushort)3, (short)3, Comparison.Equal);
                    CompareTest((ushort)3, (short)4, Comparison.LessThan);
                    CompareTest((ushort)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((ushort)4, (ushort)4, Comparison.Equal);
                    CompareTest((ushort)4, (ushort)5, Comparison.LessThan);
                    CompareTest((ushort)5, (int)4, Comparison.GreaterThan);
                    CompareTest((ushort)5, (int)5, Comparison.Equal);
                    CompareTest((ushort)5, (int)6, Comparison.LessThan);
                    CompareTest((ushort)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((ushort)6, (uint)6, Comparison.Equal);
                    CompareTest((ushort)6, (uint)7, Comparison.LessThan);
                    CompareTest((ushort)7, (long)6, Comparison.GreaterThan);
                    CompareTest((ushort)7, (long)7, Comparison.Equal);
                    CompareTest((ushort)7, (long)8, Comparison.LessThan);
                    CompareTest((ushort)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((ushort)8, (ulong)8, Comparison.Equal);
                    CompareTest((ushort)8, (ulong)9, Comparison.LessThan);
                    CompareTest((ushort)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((ushort)9, (nint)9, Comparison.Equal);
                    CompareTest((ushort)9, (nint)10, Comparison.LessThan);
                    CompareTest((ushort)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((ushort)10, (nuint)10, Comparison.Equal);
                    CompareTest((ushort)10, (nuint)11, Comparison.LessThan);
                    CompareTest((ushort)11, (float)10, Comparison.GreaterThan);
                    CompareTest((ushort)11, (float)11, Comparison.Equal);
                    CompareTest((ushort)11, (float)12, Comparison.LessThan);
                    CompareTest((ushort)12, (double)11, Comparison.GreaterThan);
                    CompareTest((ushort)12, (double)12, Comparison.Equal);
                    CompareTest((ushort)12, (double)13, Comparison.LessThan);
                    CompareTest((ushort)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((ushort)13, (decimal)13, Comparison.Equal);
                    CompareTest((ushort)13, (decimal)14, Comparison.LessThan);

                    CompareTest((int)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((int)1, (byte)1, Comparison.Equal);
                    CompareTest((int)1, (byte)2, Comparison.LessThan);
                    CompareTest((int)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((int)2, (sbyte)2, Comparison.Equal);
                    CompareTest((int)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((int)3, (short)2, Comparison.GreaterThan);
                    CompareTest((int)3, (short)3, Comparison.Equal);
                    CompareTest((int)3, (short)4, Comparison.LessThan);
                    CompareTest((int)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((int)4, (ushort)4, Comparison.Equal);
                    CompareTest((int)4, (ushort)5, Comparison.LessThan);
                    CompareTest((int)5, (int)4, Comparison.GreaterThan);
                    CompareTest((int)5, (int)5, Comparison.Equal);
                    CompareTest((int)5, (int)6, Comparison.LessThan);
                    CompareTest((int)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((int)6, (uint)6, Comparison.Equal);
                    CompareTest((int)6, (uint)7, Comparison.LessThan);
                    CompareTest((int)7, (long)6, Comparison.GreaterThan);
                    CompareTest((int)7, (long)7, Comparison.Equal);
                    CompareTest((int)7, (long)8, Comparison.LessThan);
                    CompareTest((int)8, (ulong)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UInt64."));
                    CompareTest((int)8, (ulong)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UInt64."));
                    CompareTest((int)8, (ulong)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UInt64."));
                    CompareTest((int)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((int)9, (nint)9, Comparison.Equal);
                    CompareTest((int)9, (nint)10, Comparison.LessThan);
                    CompareTest((int)10, (nuint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UIntPtr."));
                    CompareTest((int)10, (nuint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UIntPtr."));
                    CompareTest((int)10, (nuint)11, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int32 and System.UIntPtr."));
                    CompareTest((int)11, (float)10, Comparison.GreaterThan);
                    CompareTest((int)11, (float)11, Comparison.Equal);
                    CompareTest((int)11, (float)12, Comparison.LessThan);
                    CompareTest((int)12, (double)11, Comparison.GreaterThan);
                    CompareTest((int)12, (double)12, Comparison.Equal);
                    CompareTest((int)12, (double)13, Comparison.LessThan);
                    CompareTest((int)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((int)13, (decimal)13, Comparison.Equal);
                    CompareTest((int)13, (decimal)14, Comparison.LessThan);

                    CompareTest((uint)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((uint)1, (byte)1, Comparison.Equal);
                    CompareTest((uint)1, (byte)2, Comparison.LessThan);
                    CompareTest((uint)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((uint)2, (sbyte)2, Comparison.Equal);
                    CompareTest((uint)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((uint)3, (short)2, Comparison.GreaterThan);
                    CompareTest((uint)3, (short)3, Comparison.Equal);
                    CompareTest((uint)3, (short)4, Comparison.LessThan);
                    CompareTest((uint)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((uint)4, (ushort)4, Comparison.Equal);
                    CompareTest((uint)4, (ushort)5, Comparison.LessThan);
                    CompareTest((uint)5, (int)4, Comparison.GreaterThan);
                    CompareTest((uint)5, (int)5, Comparison.Equal);
                    CompareTest((uint)5, (int)6, Comparison.LessThan);
                    CompareTest((uint)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((uint)6, (uint)6, Comparison.Equal);
                    CompareTest((uint)6, (uint)7, Comparison.LessThan);
                    CompareTest((uint)7, (long)6, Comparison.GreaterThan);
                    CompareTest((uint)7, (long)7, Comparison.Equal);
                    CompareTest((uint)7, (long)8, Comparison.LessThan);
                    CompareTest((uint)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((uint)8, (ulong)8, Comparison.Equal);
                    CompareTest((uint)8, (ulong)9, Comparison.LessThan);
                    CompareTest((uint)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((uint)9, (nint)9, Comparison.Equal);
                    CompareTest((uint)9, (nint)10, Comparison.LessThan);
                    CompareTest((uint)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((uint)10, (nuint)10, Comparison.Equal);
                    CompareTest((uint)10, (nuint)11, Comparison.LessThan);
                    CompareTest((uint)11, (float)10, Comparison.GreaterThan);
                    CompareTest((uint)11, (float)11, Comparison.Equal);
                    CompareTest((uint)11, (float)12, Comparison.LessThan);
                    CompareTest((uint)12, (double)11, Comparison.GreaterThan);
                    CompareTest((uint)12, (double)12, Comparison.Equal);
                    CompareTest((uint)12, (double)13, Comparison.LessThan);
                    CompareTest((uint)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((uint)13, (decimal)13, Comparison.Equal);
                    CompareTest((uint)13, (decimal)14, Comparison.LessThan);

                    CompareTest((long)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((long)1, (byte)1, Comparison.Equal);
                    CompareTest((long)1, (byte)2, Comparison.LessThan);
                    CompareTest((long)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((long)2, (sbyte)2, Comparison.Equal);
                    CompareTest((long)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((long)3, (short)2, Comparison.GreaterThan);
                    CompareTest((long)3, (short)3, Comparison.Equal);
                    CompareTest((long)3, (short)4, Comparison.LessThan);
                    CompareTest((long)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((long)4, (ushort)4, Comparison.Equal);
                    CompareTest((long)4, (ushort)5, Comparison.LessThan);
                    CompareTest((long)5, (int)4, Comparison.GreaterThan);
                    CompareTest((long)5, (int)5, Comparison.Equal);
                    CompareTest((long)5, (int)6, Comparison.LessThan);
                    CompareTest((long)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((long)6, (uint)6, Comparison.Equal);
                    CompareTest((long)6, (uint)7, Comparison.LessThan);
                    CompareTest((long)7, (long)6, Comparison.GreaterThan);
                    CompareTest((long)7, (long)7, Comparison.Equal);
                    CompareTest((long)7, (long)8, Comparison.LessThan);
                    CompareTest((long)8, (ulong)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UInt64."));
                    CompareTest((long)8, (ulong)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UInt64."));
                    CompareTest((long)8, (ulong)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UInt64."));
                    CompareTest((long)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((long)9, (nint)9, Comparison.Equal);
                    CompareTest((long)9, (nint)10, Comparison.LessThan);
                    CompareTest((long)10, (nuint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UIntPtr."));
                    CompareTest((long)10, (nuint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UIntPtr."));
                    CompareTest((long)10, (nuint)11, expectedException: new InvalidOperationException("No compare function found that matches the types System.Int64 and System.UIntPtr."));
                    CompareTest((long)11, (float)10, Comparison.GreaterThan);
                    CompareTest((long)11, (float)11, Comparison.Equal);
                    CompareTest((long)11, (float)12, Comparison.LessThan);
                    CompareTest((long)12, (double)11, Comparison.GreaterThan);
                    CompareTest((long)12, (double)12, Comparison.Equal);
                    CompareTest((long)12, (double)13, Comparison.LessThan);
                    CompareTest((long)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((long)13, (decimal)13, Comparison.Equal);
                    CompareTest((long)13, (decimal)14, Comparison.LessThan);

                    CompareTest((ulong)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((ulong)1, (byte)1, Comparison.Equal);
                    CompareTest((ulong)1, (byte)2, Comparison.LessThan);
                    CompareTest((ulong)2, (sbyte)1, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.SByte."));
                    CompareTest((ulong)2, (sbyte)2, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.SByte."));
                    CompareTest((ulong)2, (sbyte)3, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.SByte."));
                    CompareTest((ulong)3, (short)2, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int16."));
                    CompareTest((ulong)3, (short)3, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int16."));
                    CompareTest((ulong)3, (short)4, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int16."));
                    CompareTest((ulong)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((ulong)4, (ushort)4, Comparison.Equal);
                    CompareTest((ulong)4, (ushort)5, Comparison.LessThan);
                    CompareTest((ulong)5, (int)4, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int32."));
                    CompareTest((ulong)5, (int)5, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int32."));
                    CompareTest((ulong)5, (int)6, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int32."));
                    CompareTest((ulong)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((ulong)6, (uint)6, Comparison.Equal);
                    CompareTest((ulong)6, (uint)7, Comparison.LessThan);
                    CompareTest((ulong)7, (long)6, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int64."));
                    CompareTest((ulong)7, (long)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int64."));
                    CompareTest((ulong)7, (long)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.Int64."));
                    CompareTest((ulong)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((ulong)8, (ulong)8, Comparison.Equal);
                    CompareTest((ulong)8, (ulong)9, Comparison.LessThan);
                    CompareTest((ulong)9, (nint)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.IntPtr."));
                    CompareTest((ulong)9, (nint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.IntPtr."));
                    CompareTest((ulong)9, (nint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.UInt64 and System.IntPtr."));
                    CompareTest((ulong)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((ulong)10, (nuint)10, Comparison.Equal);
                    CompareTest((ulong)10, (nuint)11, Comparison.LessThan);
                    CompareTest((ulong)11, (float)10, Comparison.GreaterThan);
                    CompareTest((ulong)11, (float)11, Comparison.Equal);
                    CompareTest((ulong)11, (float)12, Comparison.LessThan);
                    CompareTest((ulong)12, (double)11, Comparison.GreaterThan);
                    CompareTest((ulong)12, (double)12, Comparison.Equal);
                    CompareTest((ulong)12, (double)13, Comparison.LessThan);
                    CompareTest((ulong)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((ulong)13, (decimal)13, Comparison.Equal);
                    CompareTest((ulong)13, (decimal)14, Comparison.LessThan);

                    CompareTest((nint)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((nint)1, (byte)1, Comparison.Equal);
                    CompareTest((nint)1, (byte)2, Comparison.LessThan);
                    CompareTest((nint)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((nint)2, (sbyte)2, Comparison.Equal);
                    CompareTest((nint)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((nint)3, (short)2, Comparison.GreaterThan);
                    CompareTest((nint)3, (short)3, Comparison.Equal);
                    CompareTest((nint)3, (short)4, Comparison.LessThan);
                    CompareTest((nint)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((nint)4, (ushort)4, Comparison.Equal);
                    CompareTest((nint)4, (ushort)5, Comparison.LessThan);
                    CompareTest((nint)5, (int)4, Comparison.GreaterThan);
                    CompareTest((nint)5, (int)5, Comparison.Equal);
                    CompareTest((nint)5, (int)6, Comparison.LessThan);
                    CompareTest((nint)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((nint)6, (uint)6, Comparison.Equal);
                    CompareTest((nint)6, (uint)7, Comparison.LessThan);
                    CompareTest((nint)7, (long)6, Comparison.GreaterThan);
                    CompareTest((nint)7, (long)7, Comparison.Equal);
                    CompareTest((nint)7, (long)8, Comparison.LessThan);
                    CompareTest((nint)8, (ulong)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UInt64."));
                    CompareTest((nint)8, (ulong)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UInt64."));
                    CompareTest((nint)8, (ulong)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UInt64."));
                    CompareTest((nint)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((nint)9, (nint)9, Comparison.Equal);
                    CompareTest((nint)9, (nint)10, Comparison.LessThan);
                    CompareTest((nint)10, (nuint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UIntPtr."));
                    CompareTest((nint)10, (nuint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UIntPtr."));
                    CompareTest((nint)10, (nuint)11, expectedException: new InvalidOperationException("No compare function found that matches the types System.IntPtr and System.UIntPtr."));
                    CompareTest((nint)11, (float)10, Comparison.GreaterThan);
                    CompareTest((nint)11, (float)11, Comparison.Equal);
                    CompareTest((nint)11, (float)12, Comparison.LessThan);
                    CompareTest((nint)12, (double)11, Comparison.GreaterThan);
                    CompareTest((nint)12, (double)12, Comparison.Equal);
                    CompareTest((nint)12, (double)13, Comparison.LessThan);
                    CompareTest((nint)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((nint)13, (decimal)13, Comparison.Equal);
                    CompareTest((nint)13, (decimal)14, Comparison.LessThan);

                    CompareTest((nuint)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((nuint)1, (byte)1, Comparison.Equal);
                    CompareTest((nuint)1, (byte)2, Comparison.LessThan);
                    CompareTest((nuint)2, (sbyte)1, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.SByte."));
                    CompareTest((nuint)2, (sbyte)2, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.SByte."));
                    CompareTest((nuint)2, (sbyte)3, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.SByte."));
                    CompareTest((nuint)3, (short)2, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int16."));
                    CompareTest((nuint)3, (short)3, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int16."));
                    CompareTest((nuint)3, (short)4, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int16."));
                    CompareTest((nuint)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((nuint)4, (ushort)4, Comparison.Equal);
                    CompareTest((nuint)4, (ushort)5, Comparison.LessThan);
                    CompareTest((nuint)5, (int)4, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int32."));
                    CompareTest((nuint)5, (int)5, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int32."));
                    CompareTest((nuint)5, (int)6, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int32."));
                    CompareTest((nuint)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((nuint)6, (uint)6, Comparison.Equal);
                    CompareTest((nuint)6, (uint)7, Comparison.LessThan);
                    CompareTest((nuint)7, (long)6, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int64."));
                    CompareTest((nuint)7, (long)7, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int64."));
                    CompareTest((nuint)7, (long)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.Int64."));
                    CompareTest((nuint)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((nuint)8, (ulong)8, Comparison.Equal);
                    CompareTest((nuint)8, (ulong)9, Comparison.LessThan);
                    CompareTest((nuint)9, (nint)8, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.IntPtr."));
                    CompareTest((nuint)9, (nint)9, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.IntPtr."));
                    CompareTest((nuint)9, (nint)10, expectedException: new InvalidOperationException("No compare function found that matches the types System.UIntPtr and System.IntPtr."));
                    CompareTest((nuint)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((nuint)10, (nuint)10, Comparison.Equal);
                    CompareTest((nuint)10, (nuint)11, Comparison.LessThan);
                    CompareTest((nuint)11, (float)10, Comparison.GreaterThan);
                    CompareTest((nuint)11, (float)11, Comparison.Equal);
                    CompareTest((nuint)11, (float)12, Comparison.LessThan);
                    CompareTest((nuint)12, (double)11, Comparison.GreaterThan);
                    CompareTest((nuint)12, (double)12, Comparison.Equal);
                    CompareTest((nuint)12, (double)13, Comparison.LessThan);
                    CompareTest((nuint)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((nuint)13, (decimal)13, Comparison.Equal);
                    CompareTest((nuint)13, (decimal)14, Comparison.LessThan);

                    CompareTest((float)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((float)1, (byte)1, Comparison.Equal);
                    CompareTest((float)1, (byte)2, Comparison.LessThan);
                    CompareTest((float)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((float)2, (sbyte)2, Comparison.Equal);
                    CompareTest((float)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((float)3, (short)2, Comparison.GreaterThan);
                    CompareTest((float)3, (short)3, Comparison.Equal);
                    CompareTest((float)3, (short)4, Comparison.LessThan);
                    CompareTest((float)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((float)4, (ushort)4, Comparison.Equal);
                    CompareTest((float)4, (ushort)5, Comparison.LessThan);
                    CompareTest((float)5, (int)4, Comparison.GreaterThan);
                    CompareTest((float)5, (int)5, Comparison.Equal);
                    CompareTest((float)5, (int)6, Comparison.LessThan);
                    CompareTest((float)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((float)6, (uint)6, Comparison.Equal);
                    CompareTest((float)6, (uint)7, Comparison.LessThan);
                    CompareTest((float)7, (long)6, Comparison.GreaterThan);
                    CompareTest((float)7, (long)7, Comparison.Equal);
                    CompareTest((float)7, (long)8, Comparison.LessThan);
                    CompareTest((float)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((float)8, (ulong)8, Comparison.Equal);
                    CompareTest((float)8, (ulong)9, Comparison.LessThan);
                    CompareTest((float)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((float)9, (nint)9, Comparison.Equal);
                    CompareTest((float)9, (nint)10, Comparison.LessThan);
                    CompareTest((float)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((float)10, (nuint)10, Comparison.Equal);
                    CompareTest((float)10, (nuint)11, Comparison.LessThan);
                    CompareTest((float)11, (float)10, Comparison.GreaterThan);
                    CompareTest((float)11, (float)11, Comparison.Equal);
                    CompareTest((float)11, (float)12, Comparison.LessThan);
                    CompareTest((float)12, (double)11, Comparison.GreaterThan);
                    CompareTest((float)12, (double)12, Comparison.Equal);
                    CompareTest((float)12, (double)13, Comparison.LessThan);
                    CompareTest((float)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((float)13, (decimal)13, Comparison.Equal);
                    CompareTest((float)13, (decimal)14, Comparison.LessThan);

                    CompareTest((double)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((double)1, (byte)1, Comparison.Equal);
                    CompareTest((double)1, (byte)2, Comparison.LessThan);
                    CompareTest((double)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((double)2, (sbyte)2, Comparison.Equal);
                    CompareTest((double)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((double)3, (short)2, Comparison.GreaterThan);
                    CompareTest((double)3, (short)3, Comparison.Equal);
                    CompareTest((double)3, (short)4, Comparison.LessThan);
                    CompareTest((double)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((double)4, (ushort)4, Comparison.Equal);
                    CompareTest((double)4, (ushort)5, Comparison.LessThan);
                    CompareTest((double)5, (int)4, Comparison.GreaterThan);
                    CompareTest((double)5, (int)5, Comparison.Equal);
                    CompareTest((double)5, (int)6, Comparison.LessThan);
                    CompareTest((double)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((double)6, (uint)6, Comparison.Equal);
                    CompareTest((double)6, (uint)7, Comparison.LessThan);
                    CompareTest((double)7, (long)6, Comparison.GreaterThan);
                    CompareTest((double)7, (long)7, Comparison.Equal);
                    CompareTest((double)7, (long)8, Comparison.LessThan);
                    CompareTest((double)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((double)8, (ulong)8, Comparison.Equal);
                    CompareTest((double)8, (ulong)9, Comparison.LessThan);
                    CompareTest((double)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((double)9, (nint)9, Comparison.Equal);
                    CompareTest((double)9, (nint)10, Comparison.LessThan);
                    CompareTest((double)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((double)10, (nuint)10, Comparison.Equal);
                    CompareTest((double)10, (nuint)11, Comparison.LessThan);
                    CompareTest((double)11, (float)10, Comparison.GreaterThan);
                    CompareTest((double)11, (float)11, Comparison.Equal);
                    CompareTest((double)11, (float)12, Comparison.LessThan);
                    CompareTest((double)12, (double)11, Comparison.GreaterThan);
                    CompareTest((double)12, (double)12, Comparison.Equal);
                    CompareTest((double)12, (double)13, Comparison.LessThan);
                    CompareTest((double)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((double)13, (decimal)13, Comparison.Equal);
                    CompareTest((double)13, (decimal)14, Comparison.LessThan);

                    CompareTest((decimal)1, (byte)0, Comparison.GreaterThan);
                    CompareTest((decimal)1, (byte)1, Comparison.Equal);
                    CompareTest((decimal)1, (byte)2, Comparison.LessThan);
                    CompareTest((decimal)2, (sbyte)1, Comparison.GreaterThan);
                    CompareTest((decimal)2, (sbyte)2, Comparison.Equal);
                    CompareTest((decimal)2, (sbyte)3, Comparison.LessThan);
                    CompareTest((decimal)3, (short)2, Comparison.GreaterThan);
                    CompareTest((decimal)3, (short)3, Comparison.Equal);
                    CompareTest((decimal)3, (short)4, Comparison.LessThan);
                    CompareTest((decimal)4, (ushort)3, Comparison.GreaterThan);
                    CompareTest((decimal)4, (ushort)4, Comparison.Equal);
                    CompareTest((decimal)4, (ushort)5, Comparison.LessThan);
                    CompareTest((decimal)5, (int)4, Comparison.GreaterThan);
                    CompareTest((decimal)5, (int)5, Comparison.Equal);
                    CompareTest((decimal)5, (int)6, Comparison.LessThan);
                    CompareTest((decimal)6, (uint)5, Comparison.GreaterThan);
                    CompareTest((decimal)6, (uint)6, Comparison.Equal);
                    CompareTest((decimal)6, (uint)7, Comparison.LessThan);
                    CompareTest((decimal)7, (long)6, Comparison.GreaterThan);
                    CompareTest((decimal)7, (long)7, Comparison.Equal);
                    CompareTest((decimal)7, (long)8, Comparison.LessThan);
                    CompareTest((decimal)8, (ulong)7, Comparison.GreaterThan);
                    CompareTest((decimal)8, (ulong)8, Comparison.Equal);
                    CompareTest((decimal)8, (ulong)9, Comparison.LessThan);
                    CompareTest((decimal)9, (nint)8, Comparison.GreaterThan);
                    CompareTest((decimal)9, (nint)9, Comparison.Equal);
                    CompareTest((decimal)9, (nint)10, Comparison.LessThan);
                    CompareTest((decimal)10, (nuint)9, Comparison.GreaterThan);
                    CompareTest((decimal)10, (nuint)10, Comparison.Equal);
                    CompareTest((decimal)10, (nuint)11, Comparison.LessThan);
                    CompareTest((decimal)11, (float)10, Comparison.GreaterThan);
                    CompareTest((decimal)11, (float)11, Comparison.Equal);
                    CompareTest((decimal)11, (float)12, Comparison.LessThan);
                    CompareTest((decimal)12, (double)11, Comparison.GreaterThan);
                    CompareTest((decimal)12, (double)12, Comparison.Equal);
                    CompareTest((decimal)12, (double)13, Comparison.LessThan);
                    CompareTest((decimal)13, (decimal)12, Comparison.GreaterThan);
                    CompareTest((decimal)13, (decimal)13, Comparison.Equal);
                    CompareTest((decimal)13, (decimal)14, Comparison.LessThan);
                });
            });
        }
    }
}
