using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Everyone
{
    /// <summary>
    /// Optional parameters that can be provided when generating a failed assertion message.
    /// </summary>
    public record AssertParameters
    {
        /// <summary>
        /// The initial line that explains the failure in more detail.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// The <see cref="string"/> representation of the expression that failed the assertion.
        /// </summary>
        public string? Expression { get; set; }

        /// <summary>
        /// The newline <see cref="string"/> to use to separate lines.
        /// </summary>
        public string? NewLine { get; set; }

        public AssertMessageFunctions? AssertMessageFunctions { get; set; }

        public CompareFunctions? CompareFunctions { get; set; }

        public ToStringFunctions? ToStringFunctions { get; set; }

        private static void AddJSONProperty(StringBuilder builder, string propertyName, string? propertyValue)
        {
            if (!string.IsNullOrEmpty(propertyValue))
            {
                if (!builder.EndsWith('{'))
                {
                    builder.Append(',');
                }
                builder.Append(new[] { propertyName, propertyValue }.Map(Strings.EscapeAndQuote).Join(':'));
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            AssertParameters.AddJSONProperty(builder, nameof(this.Message), this.Message);
            AssertParameters.AddJSONProperty(builder, nameof(this.Expression), this.Expression);
            AssertParameters.AddJSONProperty(builder, nameof(this.NewLine), this.NewLine);
            builder.Append("}");

            return builder.ToString();
        }
    }
}
