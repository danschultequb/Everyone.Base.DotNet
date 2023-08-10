using System.Collections.Generic;
using System.Text;

namespace everyone
{
    public static class Language
    {
        public static string AndList(params object?[] values)
        {
            return Language.AndList((IEnumerable<object?>)values);
        }

        public static string AndList(this IEnumerable<object?> values)
        {
            StringBuilder builder = new StringBuilder();

            IEnumerator<object?> enumerator = values.GetEnumerator();
            bool firstValue = true;
            bool secondValue = false;
            bool hasCurrentValue = enumerator.MoveNext();
            while (hasCurrentValue)
            {
                object? currentValue = enumerator.Current;
                bool hasNextValue = enumerator.MoveNext();
                if (firstValue)
                {
                    firstValue = false;
                    secondValue = true;
                }
                else
                {
                    if (secondValue)
                    {
                        secondValue = false;
                        if (hasNextValue)
                        {
                            builder.Append(", ");
                        }
                        else
                        {
                            builder.Append(" and ");
                        }
                    }
                    else
                    {
                        builder.Append(", ");
                        if (!hasNextValue)
                        {
                            builder.Append("and ");
                        }
                    }
                }
                builder.Append(currentValue);

                hasCurrentValue = hasNextValue;
            }

            return builder.ToString();
        }
    }
}
