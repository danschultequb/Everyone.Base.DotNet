using System;

namespace Everyone
{
    /// <summary>
    /// An object that represents the current time.
    /// </summary>
    public interface Clock
    {
        /// <summary>
        /// Create a new default <see cref="Clock"/> object.
        /// </summary>
        public static SystemClock Create()
        {
            return SystemClock.Create();
        }

        /// <summary>
        /// Get the current time.
        /// </summary>
        public DateTime GetCurrentTime();
    }
}
