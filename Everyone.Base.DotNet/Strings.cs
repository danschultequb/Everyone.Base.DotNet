using System.Text;

namespace everyone
{
    /// <summary>
    /// A collection of methods for extending the built in <see cref="string"/> type.
    /// </summary>
    public static partial class Strings
    {
        private const string defaultQuote = "\"";

        /// <summary>
        /// Escape the provided <see cref="string"/> so that all escape sequences are revealed.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to escape.</param>
        public static string? Escape(this string? value)
        {
            string? result = value;

            if (!string.IsNullOrEmpty(value))
            {
                StringBuilder builder = new StringBuilder();
                foreach (char c in value)
                {
                    builder.Append(Characters.Escape(c));
                }
                result = (builder.Length == value.Length ? value : builder.ToString());
            }

            return result;
        }

        /// <summary>
        /// Surround the provided <see cref="string"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this string? value)
        {
            return Strings.Quote(value, Strings.defaultQuote);
        }

        /// <summary>
        /// Surround the provided <see cref="string"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this string? value, char quote)
        {
            return Strings.Quote(value, quote.ToString());
        }

        /// <summary>
        /// Surround the provided <see cref="string"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this string? value, string quote)
        {
            return value == null ? null : $"{quote}{value}{quote}";
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        /// <returns></returns>
        public static string? EscapeAndQuote(this string? value)
        {
            return Strings.EscapeAndQuote(value, Strings.defaultQuote);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        /// <returns></returns>
        public static string? EscapeAndQuote(this string? value, char quote)
        {
            return Strings.EscapeAndQuote(value, quote.ToString());
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        /// <returns></returns>
        public static string? EscapeAndQuote(this string? value, string quote)
        {
            return Strings.Quote(Strings.Escape(value), quote);
        }
    }
}
