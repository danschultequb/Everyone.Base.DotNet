using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when something throws an <see cref="Exception"/>
    /// while it is being awaited.
    /// </summary>
    public class AwaitException : Exception
    {
        public AwaitException(Exception innerException)
            : base(message: innerException?.Message, innerException: innerException)
        {
            Pre.Condition.AssertNotNull(innerException, nameof(innerException));
        }

        /// <summary>
        /// The <see cref="Exception"/> that was thrown during the await.
        /// </summary>
        public new Exception InnerException => base.InnerException!;
    }
}
