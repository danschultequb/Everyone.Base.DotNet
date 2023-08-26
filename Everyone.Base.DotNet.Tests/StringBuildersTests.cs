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
            });
        }
    }
}
