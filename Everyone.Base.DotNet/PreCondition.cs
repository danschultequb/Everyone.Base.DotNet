namespace Everyone
{
    public static class PreCondition
    {
        private static AssertMessageFunctions? messageFunctions = null;
        public static AssertMessageFunctions AssertMessageFunctions
        {
            get { return PreCondition.messageFunctions ?? Conditions.AssertMessageFunctions; }
            set { PreCondition.messageFunctions = value; }
        }

        private static CompareFunctions? compareFunctions = null;
        public static CompareFunctions CompareFunctions
        {
            get { return PreCondition.compareFunctions ?? Conditions.CompareFunctions; }
            set { PreCondition.compareFunctions = value; }
        }

        private static AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? PreCondition.AssertMessageFunctions;
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
                throw new PreConditionFailure(
                    PreCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedNotNullAndNotEmpty(
                                    value: value,
                                    parameters: parameters));
            }
        }

        public static void AssertGreaterThan<T,U>(T? value, U? lowerBound, string? expression = null)
        {
            PreCondition.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsGreaterThan(value, lowerBound))
            {
                throw new PreConditionFailure(
                    PreCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThan(
                                    value: value,
                                    lowerBound: lowerBound,
                                    parameters: parameters));
            }
        }

        public static void AssertGreaterThanOrEqualTo<T, U>(T? value, U? upperBound, string? expression = null)
        {
            PreCondition.AssertGreaterThanOrEqualTo(
                value: value,
                upperBound: upperBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertGreaterThanOrEqualTo<T, U>(T? value, U? upperBound, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsGreaterThanOrEqualTo(value, upperBound))
            {
                throw new PreConditionFailure(
                    PreCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThanOrEqualTo(
                                    value: value,
                                    lowerBound: upperBound,
                                    parameters: parameters));
            }
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null)
        {
            PreCondition.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                parameters: new AssertParameters { Expression = expression });
        }

        public static void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
        {
            if (!PreCondition.GetCompareFunctions(parameters).IsBetween(lowerBound, value, upperBound))
            {
                throw new PreConditionFailure(
                    PreCondition.GetAssertMessageFunctions(parameters)
                                .ExpectedBetween(
                                    lowerBound: lowerBound,
                                    value: value,
                                    upperBound: upperBound,
                                    parameters: parameters));
            }
        }
    }
}
