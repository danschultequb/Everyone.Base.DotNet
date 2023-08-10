using System;
using System.Collections;
using System.Collections.Generic;

namespace everyone
{
    public static class TypesTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Types), () =>
            {
                runner.TestGroup("GetType(T)", () =>
                {
                    runner.Test("with typed null", (Test test) =>
                    {
                        test.AssertEqual(typeof(int?), Types.GetType<int?>(null));

                        string? nullString = null;
                        test.AssertEqual(typeof(string), Types.GetType(nullString));
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        test.AssertEqual(typeof(string), Types.GetType("Hello"));
                        test.AssertEqual(typeof(int), Types.GetType(5));
                    });
                });
            });
        }
    }
}
