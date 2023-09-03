using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Everyone
{
    public interface Assertions
    {
        public static Assertions Create(Func<string,Exception> createExceptionFunction, AssertMessageFunctions? assertMessageFunctions = null, CompareFunctions? compareFunctions = null)
        {
            return BasicAssertions.Create(
                createExceptionFunction: createExceptionFunction,
                assertMessageFunctions: assertMessageFunctions ?? AssertMessageFunctions.Create(),
                compareFunctions: compareFunctions ?? CompareFunctions.Create());
        }

        public void Fail(string? message = null);

        /// <summary>
        /// Assert that the provided values are the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertSame<T, U>(T? expected, U? actual, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided values are the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertSame<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided values are not the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertNotSame<T, U>(T? expected, U? actual, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided values are not the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertNotSame<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertEqual<T, U>(T? expected, U? actual, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertEqual<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertNotEqual<T, U>(T? expected, U? actual, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public Assertions AssertNotEqual<T, U>(T? expected, U? actual, AssertParameters? parameters);

        public Assertions AssertTrue(bool? value, string? expression = null, string? message = null);

        public Assertions AssertTrue(bool? value, AssertParameters? parameters);

        public Assertions AssertFalse(bool? value, string? expression = null, string? message = null);

        public Assertions AssertFalse(bool? value, AssertParameters? parameters);

        public Assertions AssertNull(object? value, string? expression = null, string? message = null);

        public Assertions AssertNull(object? value, AssertParameters? parameters);

        public Assertions AssertNotNull([NotNull] object? value, string? expression = null, string? message = null);

        public Assertions AssertNotNull([NotNull] object? value, AssertParameters? parameters);

        public Assertions AssertNotNullAndNotEmpty([NotNull] string? value, string? expression = null, string? message = null);

        public Assertions AssertNotNullAndNotEmpty([NotNull] string? value, AssertParameters? parameters);

        public Assertions AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, string? expression = null, string? message = null);

        public Assertions AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, AssertParameters? parameters);

        public Assertions AssertLessThan<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null);

        public Assertions AssertLessThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        public Assertions AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null);

        public Assertions AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        public Assertions AssertGreaterThan<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null);

        public Assertions AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        public Assertions AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null);

        public Assertions AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        public Assertions AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null, string? message = null);

        public Assertions AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided <see cref="Disposable"/> is disposed.
        /// </summary>
        /// <param name="value">The <see cref="Disposable"/> to check.</param>
        /// <param name="expression">The expression that produced the provided
        /// <see cref="Disposable"/>.</param>
        /// <param name="message">The error message to display if the assertion fails.</param>
        public Assertions AssertDisposed(Disposable value, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided <see cref="Disposable"/> is disposed.
        /// </summary>
        /// <param name="value">The <see cref="Disposable"/> to check.</param>
        /// <param name="parameters">The <see cref="AssertParameters"/> to provide to the
        /// assertion.</param>
        public Assertions AssertDisposed(Disposable value, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided <see cref="Disposable"/> is not disposed.
        /// </summary>
        /// <param name="value">The <see cref="Disposable"/> to check.</param>
        /// <param name="expression">The expression that produced the provided
        /// <see cref="Disposable"/>.</param>
        /// <param name="message">The error message to display if the assertion fails.</param>
        public Assertions AssertNotDisposed(Disposable value, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided <see cref="Disposable"/> is not disposed.
        /// </summary>
        /// <param name="value">The <see cref="Disposable"/> to check.</param>
        /// <param name="parameters">The <see cref="AssertParameters"/> to provide to the
        /// assertion.</param>
        public Assertions AssertNotDisposed(Disposable value, AssertParameters? parameters);

        /// <summary>
        /// Assert that the provided <paramref name="value"/> is one of the provided
        /// <paramref name="possibilities"/>.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="value">The value to look for.</param>
        /// <param name="possibilities">The possible values that the the <paramref name="value"/>
        /// should be.</param>
        /// <param name="expression">The name of the expression that produced the
        /// <paramref name="value"/>.</param>
        /// <param name="message">The error message to display if the assertion fails.</param>
        /// <returns>This object for method chaining.</returns>
        public Assertions AssertOneOf<T,U>(T value, IEnumerable<U> possibilities, string? expression = null, string? message = null);

        /// <summary>
        /// Assert that the provided <paramref name="value"/> is one of the provided
        /// <paramref name="possibilities"/>.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="value">The value to look for.</param>
        /// <param name="possibilities">The possible values that the the <paramref name="value"/>
        /// <param name="parameters">The <see cref="AssertParameters"/> to provide to the
        /// assertion.</param>
        /// <returns>This object for method chaining.</returns>
        public Assertions AssertOneOf<T,U>(T value, IEnumerable<U> possibilities, AssertParameters? parameters);
    }

    /// <summary>
    /// An abstract base-class version of the <see cref="Assertions"/> interface.
    /// </summary>
    /// <typeparam name="TAssertions">The derived type that will be returned from the method
    /// chains.</typeparam>
    public abstract class AssertionsBase<TAssertions> : Assertions where TAssertions : class, Assertions
    {
        private readonly Func<string, Exception> createExceptionFunction;
        private readonly AssertMessageFunctions assertMessageFunctions;
        private readonly CompareFunctions compareFunctions;

        protected AssertionsBase(
            Func<string, Exception> createExceptionFunction,
            AssertMessageFunctions assertMessageFunctions,
            CompareFunctions compareFunctions)
        {
            this.createExceptionFunction = createExceptionFunction ?? throw new ArgumentNullException(nameof(createExceptionFunction));
            this.assertMessageFunctions = assertMessageFunctions ?? throw new ArgumentNullException(nameof(assertMessageFunctions));
            this.compareFunctions = compareFunctions ?? throw new ArgumentNullException(nameof(compareFunctions));
        }

        private AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? this.assertMessageFunctions;
        }

        private CompareFunctions GetCompareFunctions(AssertParameters? parameters)
        {
            return parameters?.CompareFunctions ?? this.compareFunctions;
        }

        void Assertions.Fail(string? message)
        {
            this.Fail(message);
        }

        public virtual void Fail(string? message = null)
        {
            throw this.createExceptionFunction(message ?? string.Empty);
        }

        Assertions Assertions.AssertSame<T, U>(T? expected, U? actual, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertSame(
                expected: expected,
                actual: actual,
                expression: expression,
                message: message);
        }

        public TAssertions AssertSame<T, U>(T? expected, U? actual, string? expression = null, string? message = null)
        {
            return this.AssertSame(
                expected: expected,
                actual: actual,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message
                });
        }

        Assertions Assertions.AssertSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
            where T : default
            where U : default
        {
            throw new NotImplementedException();
        }

        Assertions Assertions.AssertNotSame<T, U>(T? expected, U? actual, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertNotSame(
                expected: expected,
                actual: actual,
                expression: expression,
                message: message);
        }

        Assertions Assertions.AssertNotSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertNotSame(
                expected: expected,
                actual: actual,
                parameters: parameters);
        }

        public TAssertions AssertSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!object.ReferenceEquals(expected, actual))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedSame(expected, actual, parameters));
            }
            return (this as TAssertions)!;
        }

        public TAssertions AssertNotSame<T, U>(T? expected, U? actual, string? expression = null, string? message = null)
        {
            return this.AssertNotSame(
                expected: expected,
                actual: actual,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public TAssertions AssertNotSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (object.ReferenceEquals(expected, actual))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedNotSame(expected, actual, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertEqual<T, U>(T? expected, U? actual, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertEqual(
                expected: expected,
                actual: actual,
                expression: expression,
                message: message);
        }

        public TAssertions AssertEqual<T, U>(T? expected, U? actual, string? expression = null, string? message = null)
        {
            return this.AssertEqual(
                expected: expected,
                actual: actual,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertEqual(
                expected: expected,
                actual: actual,
                parameters: parameters);
        }

        public TAssertions AssertEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(expected, actual))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                    .ExpectedEqual(expected, actual, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertNotEqual<T, U>(T? expected, U? actual, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertNotEqual(
                expected: expected,
                actual: actual,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNotEqual<T, U>(T? expected, U? actual, string? expression = null, string? message = null)
        {
            return this.AssertNotEqual(
                expected: expected,
                actual: actual,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertNotEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertNotEqual(
                expected: expected,
                actual: actual,
                parameters: parameters);
        }

        public TAssertions AssertNotEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreNotEqual(expected, actual))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedNotEqual(expected, actual, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertTrue(bool? value, string? expression, string? message)
        {
            return this.AssertTrue(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertTrue(bool? value, string? expression = null, string? message = null)
        {
            return this.AssertTrue(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertTrue(bool? value, AssertParameters? parameters)
        {
            return this.AssertTrue(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertTrue(bool? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(value, true))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedTrue(
                            value: value,
                            parameters: parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertFalse(bool? value, string? expression, string? message)
        {
            return this.AssertFalse(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertFalse(bool? value, string? expression = null, string? message = null)
        {
            return this.AssertFalse(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertFalse(bool? value, AssertParameters? parameters)
        {
            return this.AssertFalse(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertFalse(bool? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(value, false))
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedFalse(value: value, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertNull(object? value, string? expression, string? message)
        {
            return this.AssertNull(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNull(object? value, string? expression = null, string? message = null)
        {
            return this.AssertNull(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertNull(object? value, AssertParameters? parameters)
        {
            return this.AssertNull(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertNull(object? value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).IsNull(value))
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedNull(value: value, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertNotNull([NotNull] object? value, string? expression, string? message)
        {
            return this.AssertNotNull(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNotNull([NotNull] object? value, string? expression = null, string? message = null)
        {
            return this.AssertNotNull(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertNotNull([NotNull] object? value, AssertParameters? parameters)
        {
            return this.AssertNotNull(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertNotNull([NotNull] object? value, AssertParameters? parameters)
        {
            if (value == null)
            {
                throw this.createExceptionFunction(this.GetAssertMessageFunctions(parameters).ExpectedNotNull(value: value, parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertNotNullAndNotEmpty([NotNull] string? value, string? expression, string? message)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNotNullAndNotEmpty([NotNull] string? value, string? expression = null, string? message = null)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message
                });
        }

        Assertions Assertions.AssertNotNullAndNotEmpty([NotNull] string? value, AssertParameters? parameters)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertNotNullAndNotEmpty([NotNull] string? value, AssertParameters? parameters)
        {
            if (value == null || !this.GetCompareFunctions(parameters).IsNotNullAndNotEmpty(value))
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                                .ExpectedNotNullAndNotEmpty(
                                    value: value,
                                    parameters: parameters));
            }
            return (this as TAssertions)!;
        }



        Assertions Assertions.AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, string? expression, string? message)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, string? expression = null, string? message = null)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, AssertParameters? parameters)
        {
            return this.AssertNotNullAndNotEmpty(
                value: value,
                parameters: parameters);
        }

        public virtual TAssertions AssertNotNullAndNotEmpty([NotNull] IEnumerable? value, AssertParameters? parameters)
        {
            if (value == null || !value.Any())
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedNotNullAndNotEmpty(
                            value: value,
                            parameters: parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertLessThan<T, U>(T? value, U? lowerBound, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertLessThan(
                value: value,
                lowerBound: lowerBound,
                expression: expression,
                message: message);
        }

        public TAssertions AssertLessThan<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            return this.AssertLessThan(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertLessThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertLessThan(
                value: value,
                lowerBound: lowerBound,
                parameters: parameters);
        }

        public virtual TAssertions AssertLessThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
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
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertLessThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                expression: expression,
                message: message);
        }

        public TAssertions AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            return this.AssertLessThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertLessThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: parameters);
        }

        public virtual TAssertions AssertLessThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
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
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertGreaterThan<T, U>(T? value, U? lowerBound, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                expression: expression,
                message: message);
        }

        public TAssertions AssertGreaterThan<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            return this.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertGreaterThan(
                value: value,
                lowerBound: lowerBound,
                parameters: parameters);
        }

        public virtual TAssertions AssertGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
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
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression, string? message)
            where T : default
            where U : default
        {
            return this.AssertGreaterThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                expression: expression,
                message: message);
        }

        public TAssertions AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, string? expression = null, string? message = null)
        {
            return this.AssertGreaterThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
            where T : default
            where U : default
        {
            return this.AssertGreaterThanOrEqualTo(
                value: value,
                lowerBound: lowerBound,
                parameters: parameters);
        }

        public virtual TAssertions AssertGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
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
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression, string? message)
            where T : default
            where U : default
            where V : default
        {
            return this.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                expression: expression,
                message: message);
        }

        public TAssertions AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, string? expression = null, string? message = null)
        {
            return this.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
            where T : default
            where U : default
            where V : default
        {
            return this.AssertBetween(
                lowerBound: lowerBound,
                value: value,
                upperBound: upperBound,
                parameters: parameters);
        }

        public virtual TAssertions AssertBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
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
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertDisposed(Disposable value, string? expression, string? message)
        {
            return this.AssertDisposed(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertDisposed(Disposable value, string? expression = null, string? message = null)
        {
            return this.AssertDisposed(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertDisposed(Disposable value, AssertParameters? parameters)
        {
            return this.AssertDisposed(
                value: value,
                parameters: parameters);
        }

        public TAssertions AssertDisposed(Disposable value, AssertParameters? parameters)
        {
            if (!value.Disposed)
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedDisposed(
                            value: value,
                            parameters: parameters));
            }
            return (this as TAssertions)!;
        }

        Assertions Assertions.AssertNotDisposed(Disposable value, string? expression, string? message)
        {
            return this.AssertNotDisposed(
                value: value,
                expression: expression,
                message: message);
        }

        public TAssertions AssertNotDisposed(Disposable value, string? expression = null, string? message = null)
        {
            return this.AssertNotDisposed(
                value: value,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        Assertions Assertions.AssertNotDisposed(Disposable value, AssertParameters? parameters)
        {
            return this.AssertNotDisposed(
                value: value,
                parameters: parameters);
        }

        public TAssertions AssertNotDisposed(Disposable value, AssertParameters? parameters)
        {
            if (value.Disposed)
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedNotDisposed(
                            value: value,
                            parameters: parameters));
            }
            return (this as TAssertions)!;
        }

        public Assertions AssertOneOf<T,U>(T value, IEnumerable<U> possibilities, string? expression = null, string? message = null)
        {
            return this.AssertOneOf(
                value: value,
                possibilities: possibilities,
                parameters: new AssertParameters
                {
                    Expression = expression,
                    Message = message,
                });
        }

        public Assertions AssertOneOf<T,U>(T value, IEnumerable<U> possibilities, AssertParameters? parameters)
        {
            CompareFunctions compareFunctions = this.GetCompareFunctions(parameters);
            if (possibilities?.Contains(value, (T value, U possibleValue) => compareFunctions.AreEqual(value, possibleValue)) != true)
            {
                throw this.createExceptionFunction(
                    this.GetAssertMessageFunctions(parameters)
                        .ExpectedOneOf(
                            value: value,
                            possibilities: possibilities!,
                            parameters: parameters));
            }
            return this;
        }
    }
}
