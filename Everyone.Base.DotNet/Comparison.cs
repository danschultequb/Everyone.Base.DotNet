namespace Everyone
{
    /// <summary>
    /// The result of comparing two values.
    /// </summary>
    public enum Comparison
    {
        /// <summary>
        /// The first value is less than the second value.
        /// </summary>
        LessThan,

        /// <summary>
        /// The first value is equal to the second value.
        /// </summary>
        Equal,

        /// <summary>
        /// The first value is greater than the second value.
        /// </summary>
        GreaterThan,
    }

    /// <summary>
    /// A collection of static functions for interacting with <see cref="Comparison"/> values.
    /// </summary>
    public static class Comparisons
    {
        /// <summary>
        /// Create a <see cref="Comparison"/> from the provided <see cref="int"/> comparison
        /// result.
        /// </summary>
        /// <param name="comparison">The <see cref="int"/> comparison result to convert to a
        /// <see cref="Comparison"/>.</param>
        public static Comparison Create(int comparison)
        {
            return comparison < 0 ? Comparison.LessThan
                : comparison == 0 ? Comparison.Equal
                : Comparison.GreaterThan;
        }
    }
}
