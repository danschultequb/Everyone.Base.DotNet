namespace everyone
{
    public static class PreCondition
    {
        private static AssertMessageFunctions? messageFunctions = null;
        public static AssertMessageFunctions MessageFunctions
        {
            get { return PreCondition.messageFunctions ?? Conditions.MessageFunctions; }
            set { PreCondition.messageFunctions = value; }
        }

        private static CompareFunctions? compareFunctions = null;
        public static CompareFunctions CompareFunctions
        {
            get { return PreCondition.compareFunctions ?? Conditions.CompareFunctions; }
            set { PreCondition.compareFunctions = value; }
        }

        public static void AssertTrue(bool value, string? expression = null)
        {
            if (!value)
            {
                throw new PreConditionFailure(PreCondition.MessageFunctions.ExpectedTrue(value: value, expression: expression));
            }
        }

        public static void AssertFalse(bool value, string? expression = null)
        {
            if (value)
            {
                throw new PreConditionFailure(PreCondition.MessageFunctions.ExpectedFalse(value: value, expression: expression));
            }
        }

        public static void AssertNull(object? value, string? expression = null)
        {
            if (!PreCondition.CompareFunctions.IsNull(value))
            {
                throw new PreConditionFailure(PreCondition.MessageFunctions.ExpectedNull(value: value, expression: expression));
            }
        }

        public static void AssertNotNull(object? value, string? expression = null)
        {
            if (!PreCondition.CompareFunctions.IsNotNull(value))
            {
                throw new PreConditionFailure(PreCondition.MessageFunctions.ExpectedNotNull(value: value, expression: expression));
            }
        }

        public static void AssertNotNullAndNotEmpty(string? value, string? expression = null)
        {
            if (!PreCondition.CompareFunctions.IsNotNullAndNotEmpty(value))
            {
                throw new PreConditionFailure(PreCondition.MessageFunctions.ExpectedNotNullAndNotEmpty(value: value, expression: expression));
            }
        }
    }
}
