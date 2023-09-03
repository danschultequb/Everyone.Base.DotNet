using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Everyone
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
        public static string? Escape(this string? value, IEnumerable<char>? dontEscape = null)
        {
            string? result = value;

            if (!string.IsNullOrEmpty(value))
            {
                StringBuilder builder = new StringBuilder();
                foreach (char c in value)
                {
                    builder.Append(Characters.Escape(c, dontEscape));
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
        public static string? EscapeAndQuote(this string? value, IEnumerable<char>? dontEscape = null)
        {
            return Strings.EscapeAndQuote(value, Strings.defaultQuote, dontEscape);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        /// <returns></returns>
        public static string? EscapeAndQuote(this string? value, char quote, IEnumerable<char>? dontEscape = null)
        {
            return Strings.EscapeAndQuote(value, quote.ToString(), dontEscape);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        /// <returns></returns>
        public static string? EscapeAndQuote(this string? value, string quote, IEnumerable<char>? dontEscape = null)
        {
            if (dontEscape == null)
            {
                if (quote == "\'")
                {
                    dontEscape = new[] { '"' };
                }
                else if (quote == "\"")
                {
                    dontEscape = new[] { '\'' };
                }
            }

            string? result = Strings.Escape(value, dontEscape);
            result = Strings.Quote(result, quote);
            return result;
        }

        public static string Join(this IEnumerable<string?> values, char separator)
        {
            return string.Join(separator, values);
        }

        public static string Join(this IEnumerable<string?> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}
