namespace Everyone
{
    public static class PreCondition
    {
        private static AssertMessageFunctions messageFunctions = AssertMessageFunctions.Create();
        public static AssertMessageFunctions MessageFunctions
        {
            get { return PreCondition.messageFunctions; }
            set { PreCondition.messageFunctions = value; }
        }

        private static CompareFunctions compareFunctions = CompareFunctions.Create();
        public static CompareFunctions CompareFunctions
        {
            get { return PreCondition.compareFunctions; }
            set { PreCondition.compareFunctions = value; }
        }

        private static AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? PreCondition.MessageFunctions;
        }

        private static CompareFunctions GetCompareFunctions(AssertParameters? parameters)
        {
            return parameters?.CompareFunctions ?? PreCondition.CompareFunctions;
        }

        public static void AssertTrue(bool? value, string? expression = null)
        {
            PreCondition.AssertTrue(value, new AssertParameters { Expression = expression });
        }

        public static void AssertTrue(bool? value, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).AreEqual(value, true))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedTrue(value: value, parameters));
            }
        }

        public static void AssertFalse(bool? value, string? expression = null)
        {
            PreCondition.AssertFalse(value, new AssertParameters { Expression = expression });
        }

        public static void AssertFalse(bool? value, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).AreEqual(value, false))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedFalse(value: value, parameters));
            }
        }

        public static void AssertNull(object? value, string? expression = null)
        {
            PreCondition.AssertNull(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNull(object? value, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsNull(value))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedNull(value: value, parameters));
            }
        }

        public static void AssertNotNull(object? value, string? expression = null)
        {
            PreCondition.AssertNotNull(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNotNull(object? value, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsNotNull(value))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedNotNull(value: value, parameters));
            }
        }

        public static void AssertNotNullAndNotEmpty(string? value, string? expression = null)
        {
            PreCondition.AssertNotNullAndNotEmpty(value, new AssertParameters { Expression = expression });
        }

        public static void AssertNotNullAndNotEmpty(string? value, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsNotNullAndNotEmpty(value))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedNotNullAndNotEmpty(value: value, parameters));
            }
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null)
        {
            PreCondition.AssertBetween(lowerBound, value, new AssertParameters { Expression = expression });
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsBetween(lowerBound, value, upperBound))
            {
                throw new PreConditionFailure(PreCondition.GetAssertMessageFunctions(parameters).ExpectedBetween(lowerBound, value, upperBound, parameters));
            }
        }
    }
}
