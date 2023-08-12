using System;

namespace Everyone
{
    public static class DisposableTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Disposable), () =>
            {
                runner.TestGroup("Create(Action)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(new ArgumentNullException(paramName: "action"), () =>
                        {
                            Disposable.Create(action: null!);
                        });
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        int counter = 0;
                        Disposable disposable = Disposable.Create(() => counter++);
                        test.AssertEqual(0, counter);

                        test.AssertTrue(disposable.Dispose());
                        test.AssertEqual(1, counter);

                        test.AssertFalse(disposable.Dispose());
                        test.AssertEqual(1, counter);
                    });
                });
            });
        }
    }
}
