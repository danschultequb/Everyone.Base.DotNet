﻿namespace Everyone
{
    public static class PostTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Post), () =>
            {
                runner.Test("Condition", (Test test) =>
                {
                    Assertions postCondition = Post.Condition;
                    test.AssertNotNull(postCondition);
                });
            });
        }
    }
}
