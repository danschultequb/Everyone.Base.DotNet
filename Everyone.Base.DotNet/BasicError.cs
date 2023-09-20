namespace Everyone
{
    /// <summary>
    /// A basic implementation of the <see cref="Error"/> interface.
    /// </summary>
    public class BasicError : Error
    {
        protected BasicError(string message, System.Diagnostics.StackTrace? stackTrace)
        {
            Pre.Condition.AssertNotNullAndNotEmpty(message, nameof(message));

            Message = message;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Create a new <see cref="BasicError"/> from the provided <paramref name="message"/> and
        /// <paramref name="stackTrace"/>.
        /// </summary>
        /// <param name="message">The message that explains the <see cref="BasicError"/>.</param>
        /// <param name="stackTrace">The optional <see cref="System.Diagnostics.StackTrace"/> that
        /// the <see cref="BasicError"/> came from.</param>
        public static BasicError Create(string message, System.Diagnostics.StackTrace? stackTrace = null)
        {
            return new BasicError(message, stackTrace);
        }

        public string Message { get; }

        public System.Diagnostics.StackTrace? StackTrace { get; }
    }
}
