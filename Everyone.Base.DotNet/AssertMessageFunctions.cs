namespace everyone
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
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="value"/>
        /// value.</param>
        public string ExpectedTrue(bool value, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been false.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that wasn't false.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="value"/>
        /// value.</param>
        public string ExpectedFalse(bool value, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been null.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that wasn't null.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="value"/>
        /// value.</param>
        public string ExpectedNull<T>(T value, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been not null.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="value"/> value.</typeparam>
        /// <param name="value">The actual value that was null.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="value"/>
        /// value.</param>
        public string ExpectedNotNull<T>(T value, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been the same.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been the same as.</param>
        /// <param name="actual">The actual value that wasn't the same as the
        /// <paramref name="expected"/> value.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="actual"/>
        /// value.</param>
        public string ExpectedSame<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been not the same.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been not the same as.</param>
        /// <param name="actual">The actual value that was the same as the
        /// <paramref name="expected"/> value.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="actual"/>
        /// value.</param>
        public string ExpectedNotSame<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been equal.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been equal to.</param>
        /// <param name="actual">The actual value that didn't equal the <paramref name="expected"/>
        /// value.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="actual"/>
        /// value.</param>
        public string ExpectedEqual<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided values should have been not equal.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="expected"/> value.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="actual"/> value.</typeparam>
        /// <param name="expected">The value that the <paramref name="actual"/> value should have
        /// been not equal to.</param>
        /// <param name="actual">The actual value that was equal to the <paramref name="expected"/>
        /// value.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="actual"/>
        /// value.</param>
        public string ExpectedNotEqual<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null);

        /// <summary>
        /// Get the message that explains that the provided value should have been not null and not
        /// empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="message">The optional message that can be provided to the resulting
        /// message.</param>
        /// <param name="newLine">The new line <see cref="string"/> that will be used to join the
        /// resulting lines.</param>
        /// <param name="expression">The expression that produced the <paramref name="actual"/>
        /// value.</param>
        public string ExpectedNotNullAndNotEmpty(string? value, string? message = null, string? newLine = null, string? expression = null);
    }
}
