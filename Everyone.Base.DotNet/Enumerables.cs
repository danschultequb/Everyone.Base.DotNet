using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Everyone
{
    public static partial class Enumerables
    {
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

        public static bool SequenceEqual(this IEnumerable? lhs, IEnumerable? rhs)
        {
            return Enumerables.SequenceEqual(lhs, rhs, object.Equals);
        }

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

        public static string ToString(this IEnumerable values)
        {
            string result = "null";
            if (values != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append('[');
                bool firstValue = true;
                foreach (object value in values)
                {
                    if (firstValue)
                    {
                        firstValue = false;
                    }
                    else
                    {
                        builder.Append(',');
                    }
                    builder.Append(value);
                }
                builder.Append(']');
                result = builder.ToString();
            }
            return result;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> values, T value)
        {
            return values.Concat(new[] { value });
        }

        public static bool ContainsAll<T>(this IEnumerable<T> values, IEnumerable<T> toCheck)
        {
            return !toCheck.Any(value => !values.Contains(value));
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

        public static Iterator<T> Iterate<T>(this IEnumerable<T> enumerable)
        {
            Pre.Condition.AssertNotNull(enumerable, nameof(enumerable));

            return Iterator.Create(enumerable.GetEnumerator());
        }
    }
}
