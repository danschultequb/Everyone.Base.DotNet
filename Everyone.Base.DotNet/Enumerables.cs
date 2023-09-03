using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Everyone
{
    public static partial class Enumerables
    {
        /// <summary>
        /// Get whether the provided <see cref="IEnumerable"/> is not empty.
        /// </summary>
        /// <param name="values">The <see cref="IEnumerable"/> to check.</param>
        public static bool Any(this IEnumerable? values)
        {
            bool result = false;
            if (values != null)
            {
                IEnumerator enumerator = values.GetEnumerator();
                try
                {
                    result = enumerator.MoveNext();
                }
                finally
                {
                    if (enumerator is IDisposable disposableEnumerator)
                    {
                        disposableEnumerator.Dispose();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get whether the provided <see cref="IEnumerable"/>s have equal values in the same
        /// order.
        /// </summary>
        /// <param name="lhs">The first <see cref="IEnumerable"/> to check.</param>
        /// <param name="rhs">The second <see cref="IEnumerable"/> to check.</param>
        public static bool SequenceEqual(this IEnumerable? lhs, IEnumerable? rhs)
        {
            return Enumerables.SequenceEqual(lhs, rhs, object.Equals);
        }

        /// <summary>
        /// Get whether the provided <see cref="IEnumerable"/>s have equal values (according to the
        /// provided <paramref name="equalFunction"/>) in the same order.
        /// </summary>
        /// <param name="lhs">The first <see cref="IEnumerable"/> to check.</param>
        /// <param name="rhs">The second <see cref="IEnumerable"/> to check.</param>
        /// <param name="equalFunction">The <see cref="Func{T1, T2, TResult}"/> to use to determine
        /// if two values are equal</param>
        public static bool SequenceEqual(this IEnumerable? lhs, IEnumerable? rhs, Func<object?, object?, bool> equalFunction)
        {
            if (equalFunction == null)
            {
                throw new ArgumentNullException(nameof(equalFunction));
            }

            IEnumerator? lhsEnumerator = lhs?.GetEnumerator();
            bool? lhsHasCurrent = lhsEnumerator?.MoveNext();

            IEnumerator? rhsEnumerator = rhs?.GetEnumerator();
            bool? rhsHasCurrent = rhsEnumerator?.MoveNext();

            bool result = lhsHasCurrent == rhsHasCurrent;
            while (lhsHasCurrent == true && rhsHasCurrent == true)
            {
                result = equalFunction.Invoke(lhsEnumerator!.Current, rhsEnumerator!.Current);
                if (!result)
                {
                    break;
                }
                else
                {
                    lhsHasCurrent = lhsEnumerator!.MoveNext();
                    rhsHasCurrent = rhsEnumerator!.MoveNext();
                    result = (lhsHasCurrent == rhsHasCurrent);
                }
            }

            return result;
        }

        /// <summary>
        /// Get a new <see cref="IEnumerable{T}"/> that concatenates the provided
        /// <paramref name="value"/> onto the end of the provided <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">The type of values stored in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="values">The initial values.</param>
        /// <param name="value">The value to concatenate.</param>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> values, T value)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Concat(new[] { value });
        }

        /// <summary>
        /// Get whether this <see cref="IEnumerable{T}"/> contains the provided 
        /// <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="U">The type of the value to look for.</typeparam>
        /// <param name="values">The <see cref="IEnumerable{T}"/> that may contain the provided
        /// <paramref name="value"/>.</param>
        /// <param name="value">The value to look for.</param>
        public static bool Contains<T, U>(this IEnumerable<T> values, U value)
        {
            return Enumerables.Contains(
                values: values,
                value: value,
                equalFunction: (U value, T enumerableValue) =>
                {
                    return value?.Equals(enumerableValue);
                });
        }

        /// <summary>
        /// Get whether this <see cref="IEnumerable{T}"/> contains the provided 
        /// <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="U">The type of the value to look for.</typeparam>
        /// <param name="values">The <see cref="IEnumerable{T}"/> that may contain the provided
        /// <paramref name="value"/>.</param>
        /// <param name="value">The value to look for.</param>
        /// <param name="compareFunction">The <see cref="Func{T1, T2, TResult}"/> that will be used
        /// to compare values.</param>
        public static bool Contains<T, U>(this IEnumerable<T> values, U value, Func<U, T, Comparison?> compareFunction)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));
            Pre.Condition.AssertNotNull(compareFunction, nameof(compareFunction));

            return Enumerables.Contains(
                values: values,
                value: value,
                equalFunction: (U value, T enumerableValue) =>
                {
                    return compareFunction(value, enumerableValue) == Comparison.Equal;
                });
        }

        /// <summary>
        /// Get whether this <see cref="IEnumerable{T}"/> contains the provided 
        /// <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="U">The type of the value to look for.</typeparam>
        /// <param name="values">The <see cref="IEnumerable{T}"/> that may contain the provided
        /// <paramref name="value"/>.</param>
        /// <param name="value">The value to look for.</param>
        /// <param name="equalFunction">The <see cref="Func{T1, T2, TResult}"/> that will be used
        /// to compare values.</param>
        public static bool Contains<T,U>(this IEnumerable<T> values, U value, Func<U,T,bool?> equalFunction)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));
            Pre.Condition.AssertNotNull(equalFunction, nameof(equalFunction));

            bool result = false;
            if (values != null)
            {
                foreach (T enumerableValue in values)
                {
                    if (equalFunction(value, enumerableValue) == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get whether the provided <paramref name="values"/> contains all of the
        /// <paramref name="toCheck"/> values.
        /// </summary>
        /// <typeparam name="T">The type of values in the first <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="U">The type of vlaues in the <see cref="IEnumerable{T}"/>
        /// <paramref name="toCheck"/>.</typeparam>
        /// <param name="values">The values that may contain all of the <paramref name="toCheck"/>
        /// values.</param>
        /// <param name="toCheck">The values to search for in the provided
        /// <paramref name="values"/>.</param>
        public static bool ContainsAll<T,U>(this IEnumerable<T> values, IEnumerable<U> toCheck)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));
            Pre.Condition.AssertNotNull(toCheck, nameof(toCheck));

            return !toCheck.Any(value => !values.Contains(value));
        }

        /// <summary>
        /// Get whether the provided <paramref name="values"/> contains any of the
        /// <paramref name="toCheck"/> values.
        /// </summary>
        /// <typeparam name="T">The type of values in the first <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="U">The type of vlaues in the <see cref="IEnumerable{T}"/>
        /// <paramref name="toCheck"/>.</typeparam>
        /// <param name="values">The values that may contain any of the <paramref name="toCheck"/>
        /// values.</param>
        /// <param name="toCheck">The values to search for in the provided
        /// <paramref name="values"/>.</param>
        public static bool ContainsAny<T,U>(this IEnumerable<T> values, IEnumerable<U> toCheck)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));
            Pre.Condition.AssertNotNull(toCheck, nameof(toCheck));

            return toCheck.Any(value => values.Contains(value));
        }

        /// <summary>
        /// Get an <see cref="IEnumerable"/> where each value is the result of applying the
        /// provided <paramref name="mapFunction"/> to a value in this <see cref="IEnumerable"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the original <see cref="IEnumerable"/>.</typeparam>
        /// <typeparam name="U">The type of values in the returned <see cref="IEnumerable"/>.</typeparam>
        /// <param name="values">The values to map from.</param>
        /// <param name="mapFunction">The <see cref="Func"/> that will perform the transformation
        /// transformation from <typeparamref name="T"/> to <typeparamref name="U"/>.</param>
        public static IEnumerable<U> Map<T,U>(this IEnumerable<T> values, Func<T,U> mapFunction)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));
            Pre.Condition.AssertNotNull(mapFunction, nameof(mapFunction));

            return values.Select(mapFunction);
        }

        /// <summary>
        /// Get an <see cref="Iterator{T}"/> that will iterate through the provided
        /// <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of values in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to iterate over.</param>
        public static Iterator<T> Iterate<T>(this IEnumerable<T> enumerable)
        {
            Pre.Condition.AssertNotNull(enumerable, nameof(enumerable));

            return Iterator.Create(enumerable.GetEnumerator());
        }
    }
}
