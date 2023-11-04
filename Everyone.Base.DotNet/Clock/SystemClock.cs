using System;

namespace Everyone
{
    /// <summary>
    /// A default system clock implementation of the <see cref="Clock"/> interface.
    /// </summary>
    public class SystemClock : Clock
    {
        protected SystemClock()
        {
        }

        public static SystemClock Create()
        {
            return new SystemClock();
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
