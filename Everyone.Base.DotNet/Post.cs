namespace Everyone
{
    public static class Post
    {
        public static Condition Condition { get; } = Condition.Create((string message) => new PostConditionFailure(message));
    }
}
