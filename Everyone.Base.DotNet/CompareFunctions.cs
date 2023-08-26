using System;
using System.Collections;
using System.Linq;

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
        public bool IsNull<T>(T? value);

        /// <summary>
        /// Get whether the provided <paramref name="value"/> is not null.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="value">The value to check.</param>
        public bool IsNotNull<T>(T? value);

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
        public bool AreNotEqual<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <see cref="string"/> is null or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public bool IsNullOrEmpty(string? value);

        /// <summary>
        /// Get whether the provided <see cref="string"/> is not null and not empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public bool IsNotNullAndNotEmpty(string? value);

        /// <summary>
        /// Get whether the provided <see cref="IEnumerable"/> is null or empty.
        /// </summary>
        /// <param name="values">The <see cref="IEnumerable"/> to check.</param>
        public bool IsNullOrEmpty(IEnumerable? values);

        /// <summary>
        /// Get whether the provided <see cref="IEnumerable"/> is not null and not empty.
        /// </summary>
        /// <param name="values">The <see cref="IEnumerable"/> to check.</param>
        public bool IsNotNullAndNotEmpty(IEnumerable? values);

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
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool? IsLessThan<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <paramref name="lhs"/> value is less than or equal to the
        /// provided <paramref name="rhs"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool IsLessThanOrEqualTo<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <paramref name="lhs"/> value is greater than the provided
        /// <paramref name="rhs"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool IsGreaterThan<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <paramref name="lhs"/> value is greater than or equal to the
        /// provided <paramref name="rhs"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the first value.</typeparam>
        /// <typeparam name="U">The type of the second value.</typeparam>
        /// <param name="lhs">The first value.</param>
        /// <param name="rhs">The second value.</param>
        public bool IsGreaterThanOrEqualTo<T, U>(T? lhs, U? rhs);

        /// <summary>
        /// Get whether the provided <paramref name="value"/> is greater than or equal to the
        /// provided <paramref name="lowerBound"/> and less than or equal to the provided
        /// <paramref name="upperBound"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="lowerBound"/>.</typeparam>
        /// <typeparam name="U">The type of the <paramref name="value"/>.</typeparam>
        /// <typeparam name="V">The type of the <paramref name="upperBound"/>.</typeparam>
        /// <param name="lowerBound">The lower bound that the <paramref name="value"/> must be
        /// greater than or equal to.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="upperBound">The upper bound that the <paramref name="value"/> must be less
        /// than or equal to.</param>
        public bool IsBetween<T, U, V>(T? lowerBound, U? value, V? upperBound);
    }

    public abstract class CompareFunctionsBase : CompareFunctions
    {
        public abstract Disposable AddCompareFunction<T, U>(Func<T?, U?, Comparison> compareFunction);

        public abstract Disposable AddEqualFunction<T, U>(Func<T?, U?, bool> equalFunction);

        public abstract bool AreEqual<T, U>(T? lhs, U? rhs);

        public abstract Comparison Compare<T, U>(T? lhs, U? rhs);

        public virtual bool IsNull<T>(T? value)
        {
            return this.AreEqual<object?, T>(null, value);
        }

        public virtual bool IsNotNull<T>(T? value)
        {
            return this.AreNotEqual<object?, T>(null, value);
        }

        public virtual bool AreNotEqual<T, U>(T? lhs, U? rhs)
        {
            return !this.AreEqual(lhs, rhs);
        }

        public virtual bool IsNullOrEmpty(string? value)
        {
            return this.AreEqual(value, (string?)null) ||
                this.AreEqual(value, string.Empty);
        }

        public virtual bool IsNotNullAndNotEmpty(string? value)
        {
            return !this.IsNullOrEmpty(value);
        }

        public virtual bool? IsLessThan<T, U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) == Comparison.LessThan;
        }

        public virtual bool IsLessThanOrEqualTo<T, U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) != Comparison.GreaterThan;
        }

        public virtual bool IsGreaterThan<T, U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) == Comparison.GreaterThan;
        }

        public virtual bool IsGreaterThanOrEqualTo<T, U>(T? lhs, U? rhs)
        {
            return this.Compare(lhs, rhs) != Comparison.LessThan;
        }

        public virtual bool IsBetween<T, U, V>(T? lowerBound, U? value, V? upperBound)
        {
            return this.IsLessThanOrEqualTo(lowerBound, value) &&
                this.IsLessThanOrEqualTo(value, upperBound);
        }

        public bool IsNullOrEmpty(IEnumerable? values)
        {
            return values.Any();
        }

        public bool IsNotNullAndNotEmpty(IEnumerable? values)
        {
            return !this.IsNotNullAndNotEmpty(values);
        }
    }
}
