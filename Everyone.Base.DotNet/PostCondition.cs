namespace Everyone
{
    public static class PostCondition
    {
        private static AssertMessageFunctions? messageFunctions = null;
        public static AssertMessageFunctions AssertMessageFunctions
        {
            get { return PostCondition.messageFunctions ?? Conditions.AssertMessageFunctions; }
            set { PostCondition.messageFunctions = value; }
        }

        private static CompareFunctions? compareFunctions = null;
        public static CompareFunctions CompareFunctions
        {
            get { return PostCondition.compareFunctions ?? Conditions.CompareFunctions; }
            set { PostCondition.compareFunctions = value; }
        }

        private static AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? PostCondition.AssertMessageFunctions;
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

        public static void AssertGreaterThan<T, U>(T? value, U? lowerBound, string? expression = null)
        {
            PostCondition.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).IsGreaterThan(value, lowerBound))
            {
                throw new PostConditionFailure(
                    PostCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThan(
                                    value: value,
                                    lowerBound: lowerBound,
                                    parameters: parameters));
            }
        }

        public static void AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null)
        {
            PostCondition.AssertGreaterThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            if (!PostCondition.GetCompareFunctions(parameters).IsGreaterThanOrEqualTo(value, lowerBound))
            {
                throw new PostConditionFailure(
                    PostCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThanOrEqualTo(
                                    value: value,
                                    lowerBound: lowerBound,
                                    parameters: parameters));
            }
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null)
        {
            PostCondition.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
        {
            CompareFunctions compareFunctions = PostCondition.GetCompareFunctions(parameters);
            if (!compareFunctions.IsBetween(lowerBound, value, upperBound))
            {
                AssertMessageFunctions messageFunctions = PostCondition.GetAssertMessageFunctions(parameters);
                throw new PostConditionFailure(
                    compareFunctions.AreEqual(lowerBound, upperBound)
                    ? messageFunctions.ExpectedEqual(
                        expected: lowerBound,
                        actual: value,
                        parameters: parameters)
                    : messageFunctions.ExpectedBetween(
                        lowerBound: lowerBound,
                        value: value,
                        upperBound: upperBound,
                        parameters: parameters));
            }
        }
    }
}
