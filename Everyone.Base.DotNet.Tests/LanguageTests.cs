namespace Everyone
{
    public static class LanguageTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Language), () =>
            {
                runner.TestGroup("AndList(params object?[])", () =>
                {
                    void AndListTest(object?[] values, string expected)
                    {
                        runner.Test($"with {Language.AndList(values)}", (Test test) =>
                        {
                            test.AssertEqual(expected, Language.AndList(values));
                        });
                    }

                    AndListTest(new object?[] { }, "");
                    AndListTest(new object?[] { null, }, "");
                    AndListTest(new object?[] { false }, "False");
                    AndListTest(new object?[] { 1 }, "1");
                    AndListTest(new object?[] { '2' }, "2");
                    AndListTest(new object?[] { "3" }, "3");
                    AndListTest(new object?[] { 1, 2 }, "1 and 2");
                    AndListTest(new object?[] { 1, 2.0 }, "1 and 2");
                    AndListTest(new object?[] { 1, 2.1 }, "1 and 2.1");
                    AndListTest(new object?[] { 1, 'a', 2 }, "1, a, and 2");
                    AndListTest(new object?[] { 1, null, 2 }, "1, , and 2");
                });
            });
        }
    }
}
