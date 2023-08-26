using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Everyone
{
    public class Condition
    {
        private readonly Func<string,Exception> createExceptionFunction;
        private readonly AssertMessageFunctions assertMessageFunctions;
        private readonly CompareFunctions compareFunctions;

        protected Condition(Func<string,Exception> createExceptionFunction, AssertMessageFunctions assertMessageFunctions, CompareFunctions compareFunctions)
        {
            this.createExceptionFunction = createExceptionFunction ?? throw new ArgumentNullException(nameof(createExceptionFunction));
            this.assertMessageFunctions = assertMessageFunctions ?? throw new ArgumentNullException(nameof(assertMessageFunctions));
            this.compareFunctions = compareFunctions ?? throw new ArgumentNullException(nameof(compareFunctions));
        }

        public static Condition Create(Func<string,Exception> createExceptionFunction, AssertMessageFunctions? assertMessageFunctions = null, CompareFunctions? compareFunctions = null)
        {
            return new Condition(
                createExceptionFunction: createExceptionFunction,
                assertMessageFunctions: assertMessageFunctions ?? AssertMessageFunctions.Create(),
                compareFunctions: compareFunctions ?? CompareFunctions.Create());
        }

        private AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? this.assertMessageFunctions;
        }

        private CompareFunctions GetCompareFunctions(AssertParameters? parameters)
        {
            return parameters?.CompareFunctions ?? this.compareFunctions;
        }

        public void Fail(string? message = null)
        {
            throw this.createExceptionFunction(message ?? string.Empty);
        }

        public void AssertTrue(bool? value, string? expression = null, string? message = null)
        {
            this.AssertTrue(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertTrue(bool? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(value, true))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedTrue(
                            value: value,
                            parameters: parameters));
            }
        }

        public void AssertFalse(bool? value, string? expression = null, string? message = null)
        {
            this.AssertFalse(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertFalse(bool? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(value, false))
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedFalse(value: value, parameters));
            }
        }

        public void AssertNull(object? value, string? expression = null, string? message = null)
        {
            this.AssertNull(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertNull(object? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).IsNull(value))
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedNull(value: value, parameters));
            }
        }

        public void AssertNotNull([NotNull]object? value, string? expression = null, string? message = null)
        {
            this.AssertNotNull(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertNotNull([NotNull] object? value, AssertParameters? parameters)
        {
            if (value == null)
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedNotNull(value: value, parameters));
            }
        }

        public void AssertNotNullAndNotEmpty([NotNull] string? value, string? expression = null, string? message = null)
        {
            this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message
                });
        }

        public void AssertNotNullAndNotEmpty([NotNull] string? value, AssertParameters? parameters)
        {
            if (value == null || !this.GetCompareFunctions(parameters).IsNotNullAndNotEmpty(value))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                                .ExpectedNotNullAndNotEmpty(
                                    value: value,
                                    parameters: parameters));
            }
        }

        public void AssertGreaterThan<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            this.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).IsGreaterThan(value, lowerBound))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThan(
                                    value: value,
                                    lowerBound: lowerBound,
                                    parameters: parameters));
            }
        }

        public void AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            this.AssertGreaterThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).IsGreaterThanOrEqualTo(value, lowerBound))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                                .ExpectedGreaterThanOrEqualTo(
                                    value: value,
                                    lowerBound: lowerBound,
                                    parameters: parameters));
            }
        }

        public void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null, string? message = null)
        {
            this.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
        {
            CompareFunctions compareFunctions = this.GetCompareFunctions(parameters);
            if (!compareFunctions.IsBetween(lowerBound, value, upperBound))
            {
                AssertMessageFunctions messageFunctions = this.GetAssertMessageFunctions(parameters);
                throw this.createExceptionFunction(
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

        public void AssertNotNullAndNotEmpty(IEnumerable? value, string? expression = null, string? message = null)
        {
            this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public void AssertNotNullAndNotEmpty(IEnumerable? value, AssertParameters? parameters)
        {
            if (!value.Any())
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedNotNullAndNotEmpty(
                            value: value,
                            parameters: parameters));
            }
        }
    }
}
