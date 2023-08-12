using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;

namespace Everyone
{
    public static class TypesTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Types), () =>
            {
                runner.Test("GetType<T>()", (Test test) =>
                {
                    test.AssertEqual(typeof(object), Types.GetType<object>());
                    test.AssertEqual(typeof(object), Types.GetType<object?>());
                    test.AssertEqual(typeof(int), Types.GetType<int>());
                    test.AssertEqual(typeof(Nullable<int>), Types.GetType<int?>());
                    test.AssertEqual(typeof(bool), Types.GetType<bool>());
                    test.AssertEqual(typeof(Nullable<bool>), Types.GetType<bool?>());
                    test.AssertEqual(typeof(string), Types.GetType<string>());
                    test.AssertEqual(typeof(string), Types.GetType<string?>());
                    test.AssertEqual(typeof(IComparable), Types.GetType<IComparable>());
                    test.AssertEqual(typeof(IComparable<int>), Types.GetType<IComparable<int>>());
                    test.AssertEqual(typeof(TestRunner), Types.GetType<TestRunner>());
                });

                runner.TestGroup("GetType<T>(T?)", () =>
                {
                    void GetTypeTest<T>(T? value, Type expected)
                    {
                        runner.Test($"with {runner.ToString(value)} ({Types.GetFullName(value)})", (Test test) =>
                        {
                            test.AssertEqual(expected, Types.GetType<T>(value));
                        });
                    }

                    GetTypeTest((object?)null, typeof(object));
                    GetTypeTest(new object(), typeof(object));
                    GetTypeTest((int?)null, typeof(int?));
                    GetTypeTest(5, typeof(int));
                    GetTypeTest((bool?)null, typeof(bool?));
                    GetTypeTest(false, typeof(bool));
                    GetTypeTest((string?)null, typeof(string));
                    GetTypeTest("", typeof(string));
                    GetTypeTest((TestGroup?)null, typeof(TestGroup));
                    GetTypeTest(new TestGroup(name: "hello"), typeof(TestGroup));
                });

                runner.Test("GetFullName<T>()", (Test test) =>
                {
                    test.AssertEqual("System.Object", Types.GetFullName<object>());
                    test.AssertEqual("System.Object", Types.GetFullName<object?>());
                    test.AssertEqual("System.Int32", Types.GetFullName<int>());
                    test.AssertEqual("System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]", Types.GetFullName<int?>());
                    test.AssertEqual("System.Boolean", Types.GetFullName<bool>());
                    test.AssertEqual("System.Nullable`1[[System.Boolean, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]", Types.GetFullName<bool?>());
                    test.AssertEqual("System.String", Types.GetFullName<string>());
                    test.AssertEqual("System.String", Types.GetFullName<string?>());
                    test.AssertEqual("System.IComparable", Types.GetFullName<IComparable>());
                    test.AssertEqual("System.IComparable`1[[System.String, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]", Types.GetFullName<IComparable<string>>());
                    test.AssertEqual("Everyone.TestRunner", Types.GetFullName<TestRunner>());
                });

                runner.TestGroup("GetFullName(this Type)", () =>
                {
                    runner.Test($"with {runner.ToString((Type?)null)}", (Test test) =>
                    {
                        test.AssertThrows(() => Types.GetFullName(null!),
                            new ArgumentNullException("type"));
                    });

                    void GetFullNameTest(Type type, string expected)
                    {
                        runner.Test($"with {runner.ToString(type)}", (Test test) =>
                        {
                            test.AssertEqual(expected, Types.GetFullName(type));
                            test.AssertEqual(expected, type.GetFullName());
                        });
                    }

                    GetFullNameTest(typeof(object), "System.Object");
                    GetFullNameTest(typeof(int), "System.Int32");
                    GetFullNameTest(typeof(int?), "System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]");
                    GetFullNameTest(typeof(bool), "System.Boolean");
                    GetFullNameTest(typeof(bool?), "System.Nullable`1[[System.Boolean, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]");
                    GetFullNameTest(typeof(string), "System.String");
                    GetFullNameTest(typeof(IComparable), "System.IComparable");
                    GetFullNameTest(typeof(IComparable<string>), "System.IComparable`1[[System.String, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]");
                    GetFullNameTest(typeof(TestRunner), "Everyone.TestRunner");
                });

                runner.TestGroup("InstanceOf<T>(this T?,Type)", () =>
                {
                    void InstanceOfErrorTest<T>(T? value, Type baseType, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, baseType }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                Types.InstanceOf(value, baseType);
                            });
                            test.AssertThrows(expected, () =>
                            {
                                value.InstanceOf(baseType);
                            });
                        });
                    }

                    InstanceOfErrorTest(
                        "hello",
                        null!,
                        new ArgumentNullException("baseType"));

                    void InstanceOfTest<T>(T? value, Type baseType, bool expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, baseType }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Types.InstanceOf(value, baseType));
                            test.AssertEqual(expected, value.InstanceOf(baseType));
                        });
                    }

                    InstanceOfTest((object)null!, typeof(string), false);
                    InstanceOfTest("hello", typeof(int), false);
                    InstanceOfTest("hello", typeof(string), true);
                    InstanceOfTest("hello", typeof(IEnumerable), true);
                    InstanceOfTest("hello", typeof(IEnumerable<char>), true);
                    InstanceOfTest(new List<int>(), typeof(List<int>), true);
                    InstanceOfTest(new List<int>(), typeof(IReadOnlyList<int>), true);
                    InstanceOfTest(new List<int>(), typeof(IEnumerable), true);
                    InstanceOfTest(new List<int>(), typeof(IEnumerable<int>), true);
                    InstanceOfTest(new List<int>(), typeof(string), false);
                });

                runner.TestGroup("InstanceOf(this Type,Type)", () =>
                {
                    void InstanceOfErrorTest(Type valueType, Type baseType, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { valueType, baseType }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                Types.InstanceOf(valueType, baseType);
                            });
                            test.AssertThrows(expected, () =>
                            {
                                valueType.InstanceOf(baseType);
                            });
                        });
                    }

                    InstanceOfErrorTest(
                        null!,
                        typeof(string),
                        new ArgumentNullException("valueType"));
                    InstanceOfErrorTest(
                        typeof(string),
                        null!,
                        new ArgumentNullException("baseType"));

                    void InstanceOfTest(Type valueType, Type baseType, bool expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { valueType, baseType }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Types.InstanceOf(valueType, baseType));
                            test.AssertEqual(expected, valueType.InstanceOf(baseType));
                        });
                    }

                    InstanceOfTest(typeof(object), typeof(string), false);
                    InstanceOfTest(typeof(string), typeof(int), false);
                    InstanceOfTest(typeof(string), typeof(string), true);
                    InstanceOfTest(typeof(string), typeof(IEnumerable), true);
                    InstanceOfTest(typeof(string), typeof(IEnumerable<char>), true);
                    InstanceOfTest(typeof(List<int>), typeof(List<int>), true);
                    InstanceOfTest(typeof(List<int>), typeof(IReadOnlyList<int>), true);
                    InstanceOfTest(typeof(List<int>), typeof(IEnumerable), true);
                    InstanceOfTest(typeof(List<int>), typeof(IEnumerable<int>), true);
                    InstanceOfTest(typeof(List<int>), typeof(string), false);
                });
            });
        }
    }
}
