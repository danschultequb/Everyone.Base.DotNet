namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when a <see cref="Result"/> that contains an
    /// <see cref="Error"/> is awaited.
    /// </summary>
    public class AwaitErrorException : System.Exception
    {
        public AwaitErrorException(Error error)
            : base(AwaitErrorException.GetErrorMessage(error))
        {
            this.Error = error;
        }

        private static string GetErrorMessage(Error error)
        {
            Pre.Condition.AssertNotNull(error, nameof(error));

            return error.Message;
        }

        /// <summary>
        /// The <see cref="Error"/> that caused this <see cref="AwaitErrorException"/>.
        /// </summary>
        public Error Error { get; }
    }
}
