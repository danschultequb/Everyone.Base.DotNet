using System;

namespace Everyone
{
    public class BasicAssertions : AssertionsBase<BasicAssertions>
    {
        protected BasicAssertions(
            Func<string, Exception> createExceptionFunction,
            AssertMessageFunctions assertMessageFunctions,
            CompareFunctions compareFunctions)
            : base(createExceptionFunction, assertMessageFunctions, compareFunctions)
        {
        }

        public static BasicAssertions Create(
            Func<string, Exception> createExceptionFunction,
            AssertMessageFunctions? assertMessageFunctions = null,
            CompareFunctions? compareFunctions = null)
        {
            return new BasicAssertions(
                createExceptionFunction: createExceptionFunction,
                assertMessageFunctions: assertMessageFunctions ?? AssertMessageFunctions.Create(),
                compareFunctions: compareFunctions ?? CompareFunctions.Create());
        }
    }
}
