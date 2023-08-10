using Microsoft.VisualBasic;

namespace everyone
{
    /// <summary>
    /// A collection of methods for extending the built in <see cref="char"/> type.
    /// </summary>
    public static partial class Characters
    {
        private const string defaultQuote = "'";

        /// <summary>
        /// Escape the provided <see cref="string"/> so that all escape sequences are revealed.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to escape.</param>
        public static string? Escape(this char? value)
        {
            return value == null ? null : Characters.Escape((char)value);
        }

        /// <summary>
        /// Escape the provided <see cref="string"/> so that all escape sequences are revealed.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to escape.</param>
        public static string Escape(this char value)
        {
            string result;

            switch (value)
            {
                case '\'':
                    result = "\\'";
                    break;

                case '\"':
                    result = "\\\"";
                    break;

                case '\\':
                    result = "\\\\";
                    break;

                case '\0':
                    result = "\\0";
                    break;

                case '\a':
                    result = "\\a";
                    break;

                case '\b':
                    result = "\\b";
                    break;

                case '\f':
                    result = "\\f";
                    break;

                case '\n':
                    result = "\\n";
                    break;

                case '\r':
                    result = "\\r";
                    break;

                case '\t':
                    result = "\\t";
                    break;

                case '\v':
                    result = "\\v";
                    break;

                default:
                    result = value.ToString();
                    break;
            }

            return result!;
        }

        /// <summary>
        /// Surround the provided <see cref="char"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this char? value)
        {
            return value == null ? null : Characters.Quote((char)value);
        }

        /// <summary>
        /// Surround the provided <see cref="char"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string Quote(this char value)
        {
            return Characters.Quote(value, Characters.defaultQuote);
        }

        /// <summary>
        /// Surround the provided <see cref="char"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this char? value, char quote)
        {
            return value == null ? null : Characters.Quote((char)value, quote);
        }

        /// <summary>
        /// Surround the provided <see cref="char"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string Quote(this char value, char quote)
        {
            return $"{quote}{value}{quote}";
        }

        /// <summary>
        /// Surround the provided <see cref="string"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string? Quote(this char? value, string quote)
        {
            return value == null ? null : Characters.Quote((char)value, quote);
        }

        /// <summary>
        /// Surround the provided <see cref="string"/> in quotes.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        /// <param name="quote">The quote to use.</param>
        public static string Quote(this char value, string quote)
        {
            return $"{quote}{value}{quote}";
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string? EscapeAndQuote(this char? value)
        {
            return value == null ? null : Characters.EscapeAndQuote((char)value);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string EscapeAndQuote(this char value)
        {
            return Characters.EscapeAndQuote(value, Characters.defaultQuote);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string? EscapeAndQuote(this char? value, char quote)
        {
            return Strings.Quote(Characters.Escape(value), quote);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string EscapeAndQuote(this char value, char quote)
        {
            return Strings.Quote(Characters.Escape(value), quote)!;
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string? EscapeAndQuote(this char? value, string quote)
        {
            return Strings.Quote(Characters.Escape(value), quote);
        }

        /// <summary>
        /// Escape and quote the provided <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value to quote.</param>
        public static string EscapeAndQuote(this char value, string quote)
        {
            return Strings.Quote(Characters.Escape(value), quote)!;
        }
    }
}
