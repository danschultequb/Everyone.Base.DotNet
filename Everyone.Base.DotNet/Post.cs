namespace Everyone
{
    public static class Post
    {
        public static Assertions Condition { get; } = Assertions.Create((string message) => new PostConditionFailure(message));
    }
}
