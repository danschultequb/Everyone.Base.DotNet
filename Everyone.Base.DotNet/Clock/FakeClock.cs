using System;
using System.Text;

namespace Everyone
{
    public class FakeClock : Clock
    {
        private DateTime currentTime;

        protected FakeClock(DateTime currentTime)
        {
            Pre.Condition.AssertNotNull(currentTime, nameof(currentTime));

            this.currentTime = currentTime;
        }

        public static FakeClock Create()
        {
            return FakeClock.Create(DateTime.Now);
        }

        public static FakeClock Create(DateTime currentTime)
        {
            return new FakeClock(currentTime);
        }

        public DateTime GetCurrentTime()
        {
            return this.currentTime;
        }

        /// <summary>
        /// Advance this <see cref="FakeClock"/> forward by the provided <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="duration">The <see cref="TimeSpan"/> to advance this
        /// <see cref="FakeClock"/> by.</param>
        /// <returns>This object for method chaining.</returns>
        public FakeClock Advance(TimeSpan duration)
        {
            Pre.Condition.AssertNotNull(duration, nameof(duration));
            Pre.Condition.AssertGreaterThanOrEqualTo(duration, TimeSpan.Zero, nameof(duration));

            this.currentTime = this.currentTime.Add(duration);

            return this;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('{');
            builder.AppendJSONProperty("CurrentTime", this.currentTime.ToString());
            builder.Append('}');
            return builder.ToString();
        }
    }
}
