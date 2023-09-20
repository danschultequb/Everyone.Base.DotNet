using System;

namespace Everyone
{
    public class AwaitException : Exception
    {
        public AwaitException(Exception innerException)
            : base(message: innerException?.Message, innerException: innerException)
        {
            Pre.Condition.AssertNotNull(innerException, nameof(innerException));
        }

        public new Exception InnerException => base.InnerException!;

        /// <summary>
        /// Get the <see cref="Exception"/> that is wrapped by this and any further
        /// <see cref="AwaitException"/>s.
        public Exception Unwrap()
        {
            Exception result = this;
            while (result is AwaitException awaitException)
            {
                result = awaitException.InnerException;
            }

            Post.Condition.AssertNotNull(result, nameof(result));

            return result;
        }
    }
}
