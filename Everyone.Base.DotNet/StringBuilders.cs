using System.Text;

namespace Everyone
{
    public static class StringBuilders
    {
        /// <summary>
        /// Get whether the provided <see cref="StringBuilder"/> has any characters.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to check.</param>
        public static bool Any(this StringBuilder builder)
        {
            return builder.Length > 0;
        }

        /// <summary>
        /// Get the last character in the provided <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to get the last character of.</param>
        public static char Last(this StringBuilder builder)
        {
            Pre.Condition.AssertNotNull(builder, nameof(builder));
            Pre.Condition.AssertTrue(builder.Any(), $"{nameof(builder)}.Any()");

            return builder[builder.Length - 1];
        }

        /// <summary>
        /// Get whether this <see cref="StringBuilder"/> ends with the provided <see cref="char"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to check.</param>
        /// <param name="value">The <see cref="char"/> to look for.</param>
        public static bool EndsWith(this StringBuilder builder, char value)
        {
            Pre.Condition.AssertNotNull(builder, nameof(builder));

            return builder.Any() &&
                   builder.Last() == value;
        }
    }
}
