using System.Text;

namespace everyone
{
    public static class StringBuilders
    {
        /// <summary>
        /// Get whether this <see cref="StringBuilder"/> ends with the provided <see cref="char"/>.
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> to check.</param>
        /// <param name="value">The <see cref="char"/> to look for.</param>
        public static bool EndsWith(this StringBuilder builder, char value)
        {
            return builder.Length > 0 &&
                   builder[builder.Length - 1] == value;
        }
    }
}
