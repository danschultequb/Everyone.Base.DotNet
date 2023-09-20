using System;
using System.Diagnostics;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Error"/> that occurred.
    /// </summary>
    public interface Error
    {
        /// <summary>
        /// Create a new <see cref="BasicError"/> from the provided <paramref name="message"/> and
        /// <paramref name="stackTrace"/>.
        /// </summary>
        /// <param name="message">The message that explains the <see cref="Error"/>.</param>
        /// <param name="stackTrace">The optional <see cref="System.Diagnostics.StackTrace"/> that
        /// the <see cref="Error"/> came from.</param>
        public static BasicError Create(string message, StackTrace? stackTrace = null)
        {
            return BasicError.Create(message, stackTrace);
        }

        /// <summary>
        /// Create a new <see cref="UncaughtExceptionError"/> from the provided <see cref="Exception"/>.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that the returned
        /// <see cref="UncaughtExceptionError"/> will wrap.</param>
        public static UncaughtExceptionError Create(Exception exception)
        {
            return UncaughtExceptionError.Create(exception);
        }

        /// <summary>
        /// The message that describes this <see cref="Error"/>.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The optional <see cref="StackTrace"/> that shows where this <see cref="Error"/>
        /// occurred.
        /// </summary>
        public System.Diagnostics.StackTrace? StackTrace { get; }
    }

    public abstract class ErrorBase : Error
    {
        public abstract string Message { get; }

        public abstract StackTrace? StackTrace { get; }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Error);
        }

        public virtual bool Equals(Error? rhs)
        {
            return rhs != null &&
                this.GetType() == rhs.GetType() &&
                this.Message == rhs.Message;
        }
    }
}
