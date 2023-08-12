using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when a pre-condition fails.
    /// </summary>
    public class PreConditionFailure : Exception
    {
        /// <summary>
        /// Create a new <see cref="PreConditionFailure"/>.
        /// </summary>
        /// <param name="messageLines">The lines of the message that explain the failure.</param>
        public PreConditionFailure(params string[] messageLines)
            : base(string.Join(Environment.NewLine, messageLines))
        {
        }
    }
}
