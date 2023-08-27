namespace Everyone
{
    public static class PreTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Pre), () =>
            {
                runner.Test("Condition", (Test test) =>
                {
                    Assertions preCondition = Pre.Condition;
                    test.AssertNotNull(preCondition);
                });
            });
        }
    }
}
