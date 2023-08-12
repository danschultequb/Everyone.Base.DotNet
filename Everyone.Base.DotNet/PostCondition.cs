namespace Everyone
{
    public static class PostCondition
    {
        private static AssertMessageFunctions messageFunctions = AssertMessageFunctions.Create();
        public static AssertMessageFunctions MessageFunctions
        {
            get { return PostCondition.messageFunctions; }
            set { PostCondition.messageFunctions = value; }
        }

        private static CompareFunctions compareFunctions = CompareFunctions.Create();
        public static CompareFunctions CompareFunctions
        {
            get { return PostCondition.compareFunctions; }
            set { PostCondition.compareFunctions = value; }
        }

        private static AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? PostCondition.MessageFunctions;
        }

        private static CompareFunctions GetCompareFunctions(AssertParameters? parameters)
        {
            return parameters?.CompareFunctions ?? PostCondition.CompareFunctions;
        }

        public static void AssertTrue(bool? value, string? expression = null)
        {
            PostCondition.AssertTrue(value, new AssertParameters { Expression = expression });
        }

        public static void AssertTrue(bool? value, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).AreEqual(value, true))
            {
                throw new PostConditionFailure(PostCondition.GetAssertMessageFunctions(parameters).ExpectedTrue(value: value, parameters));
            }
        }

        public static void AssertFalse(bool? value, string? expression = null)
        {
            PostCondition.AssertFalse(value, new AssertParameters { Expression = expression });
        }

        public static void AssertFalse(bool? value, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).AreEqual(value, false))
            {
                throw new PostConditionFailure(PostCondition.GetAssertMessageFunctions(parameters).ExpectedFalse(value: value, parameters));
            }
        }

        public static void AssertNull(object? value, string? expression = null)
        {
            PostCondition.AssertNull(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNull(object? value, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).IsNull(value))
            {
                throw new PostConditionFailure(PostCondition.GetAssertMessageFunctions(parameters).ExpectedNull(value: value, parameters));
            }
        }

        public static void AssertNotNull(object? value, string? expression = null)
        {
            PostCondition.AssertNotNull(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNotNull(object? value, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).IsNotNull(value))
            {
                throw new PostConditionFailure(PostCondition.GetAssertMessageFunctions(parameters).ExpectedNotNull(value: value, parameters));
            }
        }

        public static void AssertNotNullAndNotEmpty(string? value, string? expression = null)
        {
            PostCondition.AssertNotNullAndNotEmpty(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNotNullAndNotEmpty(string? value, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).IsNotNullAndNotEmpty(value))
            {
                throw new PostConditionFailure(PostCondition.GetAssertMessageFunctions(parameters).ExpectedNotNullAndNotEmpty(value: value, parameters));
            }
        }
    }
}
