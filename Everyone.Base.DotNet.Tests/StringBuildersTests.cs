using System;
using System.Linq;
using System.Text;

namespace Everyone
{
    public static class StringBuildersTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(StringBuilders), () =>
            {
                runner.TestMethod("EndsWith(this StringBuilder,char)", () =>
                {
                    void EndsWithTest(StringBuilder builder, char value, bool expected)
                    {
                        runner.Test($"with {new object[] { builder, value }.Select(runner.ToString).AndList()}", (Test test) =>
                        {
                            test.AssertEqual(expected, builder.EndsWith(value));
                            test.AssertEqual(expected, StringBuilders.EndsWith(builder, value));
                        });
                    }

                    EndsWithTest(new StringBuilder(), ' ', false);
                    EndsWithTest(new StringBuilder(" "), ' ', true);
                    EndsWithTest(new StringBuilder("abc"), 'a', false);
                    EndsWithTest(new StringBuilder("abc"), 'b', false);
                    EndsWithTest(new StringBuilder("abc"), 'c', true);
                });

                runner.TestMethod("AddJSONProperty(this StringBuilder, string, string)", () =>
                {
                    void AddJSONPropertyTest(string initialText, string propertyName, string propertyValue, string expectedText, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new[] { initialText, propertyName, propertyValue }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                StringBuilder builder = new StringBuilder(initialText);
                                StringBuilder addJSONPropertyResult = StringBuilders.AppendJSONProperty(builder, propertyName, propertyValue);
                                test.AssertSame(builder, addJSONPropertyResult);
                                test.AssertEqual(expectedText, builder.ToString());
                            });
                        });
                    }

                    AddJSONPropertyTest("", "a", "b", ",\"a\":\"b\"");
                    AddJSONPropertyTest("{", "a", "b", "{\"a\":\"b\"");
                    AddJSONPropertyTest("{\"a\":\"b\"", "c", "d", "{\"a\":\"b\",\"c\":\"d\"");
                });
            });
        }
    }
}
