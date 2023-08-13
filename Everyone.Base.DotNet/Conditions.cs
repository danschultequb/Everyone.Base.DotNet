namespace Everyone
{
    public static class Conditions
    {
        public static AssertMessageFunctions AssertMessageFunctions { get; set; } = AssertMessageFunctions.Create();

        public static CompareFunctions CompareFunctions { get; set; } = CompareFunctions.Create();
    }
}
