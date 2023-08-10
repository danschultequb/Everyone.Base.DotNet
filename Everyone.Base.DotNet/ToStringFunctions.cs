namespace everyone
{
    /// <summary>
    /// A collection of functions that can be used to convert objects of different types to their
    /// <see cref="string"/> representation.
    /// </summary>
    public interface ToStringFunctions
    {
        /// <summary>
        /// Create a new <see cref="MutableToStringFunctions"/> object.
        /// </summary>
        /// <param name="addDefaultFunctions">Whether to create an empty
        /// <see cref="ToStringFunctions"/> object (false) or to add the default initial functions
        /// (true).</param>
        public static MutableToStringFunctions Create(bool addDefaultFunctions = true)
        {
            return MutableToStringFunctions.Create(addDefaultFunctions);
        }

        /// <summary>
        /// Get the <see cref="string"/> representation of the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the provided value.</typeparam>
        /// <param name="value">The value to get the <see cref="string"/> representation of.</param>
        public string ToString<T>(T? value);
    }
}
