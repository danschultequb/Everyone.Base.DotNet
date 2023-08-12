using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when a post-condition fails.
    /// </summary>
    public class PostConditionFailure : Exception
    {
        /// <summary>
        /// Create a new <see cref="PostConditionFailure"/>.
        /// </summary>
        /// <param name="messageLines">The lines of the message that explain the failure.</param>
        public PostConditionFailure(params string[] messageLines)
            : base(string.Join(Environment.NewLine, messageLines))
        {
        }
    }
}
