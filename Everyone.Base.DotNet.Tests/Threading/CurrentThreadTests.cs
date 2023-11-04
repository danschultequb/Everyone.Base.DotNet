namespace Everyone
{
    public static class CurrentThreadTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(CurrentThread), () =>
            {
                runner.TestMethod("GetId()", (Test test) =>
                {
                    int currentThreadId = CurrentThread.GetId();
                    test.AssertEqual(currentThreadId, CurrentThread.GetId());
                });

                runner.TestMethod("Yield()", (Test test) =>
                {
                    CurrentThread.Yield();
                });
            });
        }
    }
}
