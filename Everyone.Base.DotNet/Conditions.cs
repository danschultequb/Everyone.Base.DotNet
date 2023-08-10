namespace everyone
{
    public static class Conditions
    {
        public static AssertMessageFunctions MessageFunctions { get; set; } = AssertMessageFunctions.Create();

        public static CompareFunctions CompareFunctions { get; set; } = CompareFunctions.Create();
    }
}
