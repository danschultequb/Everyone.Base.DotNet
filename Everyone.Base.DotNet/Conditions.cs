namespace Everyone
{
    public abstract class Conditions
    {
        private Conditions()
        {
        }

        public static AssertMessageFunctions AssertMessageFunctions { get; set; } = AssertMessageFunctions.Create();

        public static CompareFunctions CompareFunctions { get; set; } = CompareFunctions.Create();
    }
}
