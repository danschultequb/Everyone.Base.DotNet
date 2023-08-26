﻿using System.Collections.Generic;
using System.Linq;

namespace Everyone
{
    public static class EnumeratorIteratorTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(EnumeratorIterator), () =>
            {
                runner.TestMethod("Create(IEnumerator<T>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(() => EnumeratorIterator.Create((IEnumerator<int>)null!),
                            new PreConditionFailure(
                                "Expression: enumerator",
                                "Expected: not null",
                                "Actual:   null"));
                    });

                    runner.Test("with with empty Enumerator that hasn't started", (Test test) =>
                    {
                        IEnumerator<int> enumerator = CreateEnumerator<int>();
                        EnumeratorIterator<int> iterator = EnumeratorIterator.Create(enumerator);
                        test.AssertNotNull(iterator);
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertFalse(iterator.HasStarted());

                        test.AssertFalse(iterator.Next());
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertTrue(iterator.HasStarted());
                    });

                    runner.Test("with with empty Enumerator that has started", (Test test) =>
                    {
                        IEnumerator<int> enumerator = CreateEnumerator<int>();
                        test.AssertFalse(enumerator.MoveNext());

                        EnumeratorIterator<int> iterator = EnumeratorIterator.Create(enumerator);
                        test.AssertNotNull(iterator);
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertFalse(iterator.HasStarted());

                        test.AssertFalse(iterator.Next());
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertTrue(iterator.HasStarted());
                    });

                    runner.Test("with with non-empty Enumerator that hasn't started", (Test test) =>
                    {
                        int[] values = new[] { 1, 2, 3 };
                        IEnumerator<int> enumerator = CreateEnumerator(values);
                        EnumeratorIterator<int> iterator = EnumeratorIterator.Create(enumerator);
                        test.AssertNotNull(iterator);
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertFalse(iterator.HasStarted());

                        for (int i = 0; i < values.Length; i++)
                        {
                            test.AssertTrue(iterator.Next());
                            test.AssertTrue(iterator.HasCurrent());
                            test.AssertTrue(iterator.HasStarted());
                            test.AssertEqual(values[i], iterator.Current);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            test.AssertFalse(iterator.Next());
                            test.AssertFalse(iterator.HasCurrent());
                            test.AssertTrue(iterator.HasStarted());
                        }
                    });

                    runner.Test("with with non-empty Enumerator that has started", (Test test) =>
                    {
                        int[] values = new[] { 1, 2, 3 };
                        IEnumerator<int> enumerator = CreateEnumerator(values);
                        test.AssertTrue(enumerator.MoveNext());
                        test.AssertEqual(1, enumerator.Current);

                        EnumeratorIterator<int> iterator = EnumeratorIterator.Create(enumerator);
                        test.AssertNotNull(iterator);
                        test.AssertFalse(iterator.HasCurrent());
                        test.AssertFalse(iterator.HasStarted());
                        test.AssertThrows(() => { int _ = iterator.Current; },
                            new PreConditionFailure(
                                "Expression: this.HasCurrent()",
                                "Expected: True",
                                "Actual:   False"));

                        for (int i = 1; i < values.Length; i++)
                        {
                            test.AssertTrue(iterator.Next());
                            test.AssertTrue(iterator.HasCurrent());
                            test.AssertTrue(iterator.HasStarted());
                            test.AssertEqual(values[i], iterator.Current);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            test.AssertFalse(iterator.Next());
                            test.AssertFalse(iterator.HasCurrent());
                            test.AssertTrue(iterator.HasStarted());
                        }
                    });
                });

                runner.TestMethod("Dispose()", (Test test) =>
                {
                    EnumeratorIterator<int> iterator = CreateIterator<int>();
                    test.AssertFalse(iterator.Disposed);

                    test.AssertTrue(iterator.Dispose());
                    test.AssertTrue(iterator.Disposed);

                    for (int i = 0; i < 2; i++)
                    {
                        test.AssertFalse(iterator.Dispose());
                        test.AssertTrue(iterator.Disposed);
                    }
                });

                runner.Test("foreach", (Test test) =>
                {
                    List<int> values = new List<int>();
                    EnumeratorIterator<int> iterator = CreateIterator(1, 2, 3);
                    foreach (int value in iterator)
                    {
                        values.Add(value);
                    }
                    test.AssertEqual(new[] { 1, 2, 3 }, values);
                });
            });
        }

        private static IEnumerator<T> CreateEnumerator<T>(params T[] values)
        {
            return values.ToList().GetEnumerator();
        }

        private static EnumeratorIterator<T> CreateIterator<T>(params T[] values)
        {
            return EnumeratorIterator.Create(CreateEnumerator(values));
        }
    }
}
