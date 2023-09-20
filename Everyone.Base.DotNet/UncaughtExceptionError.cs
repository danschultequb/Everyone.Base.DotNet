using System;
using System.Diagnostics;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Error"/> that is caused by an uncaught <see cref="Exception"/> within the
    /// context of a <see cref="Result"/>.
    /// </summary>
    public class UncaughtExceptionError : Error
    {
        private StackTrace? stackTrace;

        protected UncaughtExceptionError(Exception exception)
        {
            Pre.Condition.AssertNotNull(exception, nameof(exception));

            this.UncaughtException = exception;
        }

        public static UncaughtExceptionError Create(Exception exception)
        {
            return new UncaughtExceptionError(exception);
        }

        /// <summary>
        /// The <see cref="Exception"/> that wasn't caught.
        /// </summary>
        public Exception UncaughtException { get; }

        public string Message
        {
            get { return this.UncaughtException.Message; }
        }

        public StackTrace? StackTrace
        {
            get
            {
                if (this.stackTrace == null && !string.IsNullOrEmpty(this.UncaughtException.StackTrace))
                {
                    this.stackTrace = new StackTrace(this.UncaughtException);
                }
                return this.stackTrace;
            }
        }
    }
}
