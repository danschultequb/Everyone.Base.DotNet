using System.Collections;

namespace Everyone
{
    /// <summary>
    /// An object that generates custom messages.
    /// </summary>
    public interface AssertMessageFunctions
    {
        /// <summary>
        /// Create the default implementation of the <see cref="AssertMessageFunctions"/> interface.
        /// </summary>
        /// <returns></returns>
        public static AssertMessageFunctions Create(ToStringFunctions? toStringFunctions = null)
        {
            return BasicAssertMessageFunctions.Create(toStringFunctions);
        }

        /// <summary>
        /// The <see cref="ToStringFunctions"/> that will be used to convert objects to
        /// <see cref="string"/> representations.
        /// </summary>
        public ToStringFunctions ToStringFunctions { get; }

        /// <summary>
        /// Get the message that explains that the provided values should have been true.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that wasn't true.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedTrue(bool? value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been false.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that wasn't false.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedFalse(bool? value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been null.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that wasn't null.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNull<T>(T value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been not null.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that was null.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNotNull<T>(T value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been the same.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been the same as.</param>
        /// <param name="actual">The actual value that wasn't the same as the
        /// <paramref name="expected"/> value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedSame<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been not the same.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been not the same as.</param>
        /// <param name="actual">The actual value that was the same as the
        /// <paramref name="expected"/> value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNotSame<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been equal.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been equal to.</param>
        /// <param name="actual">The actual value that didn't equal the <paramref name="expected"/>
        /// value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedEqual<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided values should have been not equal.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been not equal to.</param>
        /// <param name="actual">The actual value that was equal to the <paramref name="expected"/>
        /// value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNotEqual<T, U>(T? expected, U? actual, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided value should have been not null and not
        /// empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNotNullAndNotEmpty(string? value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided value should have been not null and not
        /// empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The optional parameters for the error message.
        public string ExpectedNotNullAndNotEmpty(IEnumerable? value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided <paramref name="value"/> should have
        /// been greater than the provided <paramref name="lowerBound"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="lowerBound"/>.</typeparam>
        /// <param name="value">The <paramref name="value"/> to check.</param>
        /// <param name="lowerBound">The <paramref name="lowerBound"/> that the
        /// <paramref name="value"/> must be less than.</param>
        /// <param name="parameters">The optional parameters for the error message.</param>
        public string ExpectedGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided <paramref name="value"/> should have
        /// been greater than or equal to the provided <paramref name="lowerBound"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="lowerBound"/>.</typeparam>
        /// <param name="value">The <paramref name="value"/> to check.</param>
        /// <param name="lowerBound">The <paramref name="lowerBound"/> that the
        /// <paramref name="value"/> must be less than.</param>
        /// <param name="parameters">The optional parameters for the error message.</param>
        public string ExpectedGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided <paramref name="value"/> should have
        /// been between (inclusive) the provided <paramref name="lowerBound"/> and
        /// <paramref name="upperBound"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="lowerBound"/>.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="value"/>.</typeparam>
        /// <typeparam name="V">The type of the <paramref name="upperBound"/>.</typeparam>
        /// <param name="lowerBound">The <paramref name="lowerBound"/> that the
        /// <paramref name="value"/> must be greater than or equal to.</param>
        /// <param name="value">The <paramref name="value"/> to check.</param>
        /// <param name="upperBound">The <paramref name="upperBound"/> that the
        /// <paramref name="value"/> must be less than or equal to.</param>
        /// <param name="parameters">The optional parameters for the error message.</param>
        public string ExpectedBetween<T,U,V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided <see cref="Disposable"/> should have
        /// been disposed.
        /// </summary>
        /// <param name="value">The value to that should've been disposed.</param>
        /// <param name="parameters">The optional parameters for the error message.</param>
        public string ExpectedDisposed(Disposable value, AssertParameters? parameters);

        /// <summary>
        /// Get the message that explains that the provided <see cref="Disposable"/> should have
        /// been not disposed.
        /// </summary>
        /// <param name="value">The value to that should've been not disposed.</param>
        /// <param name="parameters">The optional parameters for the error message.</param>
        public string ExpectedNotDisposed(Disposable value, AssertParameters? parameters);
    }
}
