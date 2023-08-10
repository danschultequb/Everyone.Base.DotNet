using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace everyone
{
    public static partial class Enumerables
    {
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
    }
}
