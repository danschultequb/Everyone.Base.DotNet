namespace Everyone
{
    public static class Pre
    {
        public static Condition Condition { get; } = Condition.Create((string message) => new PreConditionFailure(message));
    }
}
