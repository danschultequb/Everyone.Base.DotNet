using System.Text;

namespace Everyone
{
    /// <summary>
    /// Optional parameters that can be provided when generating a failed assertion message.
    /// </summary>
    public class AssertParameters
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.AppendJSONProperty(nameof(this.Message), this.Message);
            builder.AppendJSONProperty(nameof(this.Expression), this.Expression);
            builder.AppendJSONProperty(nameof(this.NewLine), this.NewLine);
            builder.Append("}");

            return builder.ToString();
        }
    }
}
