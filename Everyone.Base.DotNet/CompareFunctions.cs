using System;

namespace Everyone
{
    /// <summary>
    /// An object that can be used to compare values using custom compare functions.
    /// </summary>
    public interface CompareFunctions
    {
        /// <summary>
        /// Create the default implementation for <see cref="CompareFunctions"/>.
        /// </summary>
        public static CompareFunctions Create(bool addDefaultEqualFunctions = true, bool addDefaultCompareFunctions = true)
        {
            return BasicCompareFunctions.Create(
                addDefaultEqualFunctions: addDefaultEqualFunctions,
                addDefaultCompareFunctions: addDefaultCompareFunctions);
        }

        /// <summary>
        /// Add a <see cref="Func{T?,U?,Comparison}"/> that can be used to compare values of type
        /// <typeparamref name="T"/> and <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first value to compare.</typeparam>
        /// <typeparam name="U">The type of the second value to compare.</typeparam>
        /// <param name="compareFunction">The compare function to add.</param>
        /// <returns>A <see cref="Disposable"/> that can be used to unregister the provided
        /// <paramref name="compareFunction"/>.</returns>
        public Disposable AddCompareFunction<T, U>(Func<T?, U?, Comparison> compareFunction);

        /// <summary>
        /// Add a <see cref="Func{T, U, bool}"/> that can be used to compare values of type
        /// <typeparamref name="T"/> and <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first value to compare.</typeparam>
        /// <typeparam name="U">The type of the second value to compare.</typeparam>
        /// <param name="equalFunction">The equal function to add.</param>
        /// <returns>A <see cref="Disposable"/> that can be used to unregister the provided
        /// <paramref name="equalFunction"/>.</returns>
        public Disposable AddEqualFunction<T, U>(Func<T?, U?, bool> equalFunction);

        /// <summary>
        /// Get whether the provided <paramref name="value"/> is null.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        public bool IsNull<T>(T? value)
        {
            return this.AreEqual<object?, T>(null, value);
        }

        /// <summary>
        /// Get whether the provided <paramref name="value"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        public bool IsNotNull<T>(T? value)
        {
            return this.AreNotEqual<object?, T>(null, value);
        }

        /// <summary>
        /// Get whether the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool AreEqual<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided values are not equal.
        /// </summary>
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool AreNotEqual<T, U>(T? lhs, U? rhs)
        {
            return !this.AreEqual(lhs, rhs);
        }

        public bool IsNullOrEmpty(string? value)
        {
            return this.AreEqual(value, (string?)null) ||
                this.AreEqual(value, string.Empty);
        }

        public bool IsNotNullAndNotEmpty(string? value)
        {
            return !this.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Compare the provided values.
        /// </summary>
        /// <typeparam name="T">The type of the first value to compare.</typeparam>
        /// <typeparam name="U">The type of the second value to compare.</typeparam>
        /// <param name="lhs">The first value to compare.</param>
        /// <param name="rhs">The second value to compare.</param>
        /// <exception cref="InvalidOperationException">if no compare function has been registered
        /// that matches the provided values.</exception>
        public Comparison Compare<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <paramref name="lhs"/> value is less than the provided
        /// <paramref name="rhs"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool? IsLessThan<T,U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) == Comparison.LessThan;
        }

        /// <summary>
        /// Get whether the provided <paramref name="lhs"/> value is less than or equal to the
        /// provided <paramref name="rhs"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool IsLessThanOrEqualTo<T,U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) != Comparison.GreaterThan;
        }

        public bool IsGreaterThan<T,U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) == Comparison.GreaterThan;
        }

        public bool IsGreaterThanOrEqualTo<T,U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) != Comparison.LessThan;
        }

        public bool IsBetween<T,U,V>(T? lowerBound, U? value, V? upperBound)
        {
            return this.IsLessThanOrEqualTo(lowerBound, value) &&
                this.IsLessThanOrEqualTo(value, upperBound);
        }
    }
}
