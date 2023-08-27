namespace Everyone
{
    public static class Pre
    {
        public static Assertions Condition { get; } = Assertions.Create((string message) => new PreConditionFailure(message));
    }
}
