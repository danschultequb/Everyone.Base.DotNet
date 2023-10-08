using System;

namespace Everyone
{
    public static class Exceptions
    {
        /// <summary>
        /// Unwrap the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public static Exception Unwrap(this Exception value)
        {
            return Exceptions.UnwrapTo<Exception>(value)!;
        }

        /// <summary>
        /// Unwrap the provided <paramref name="value"/> to the provided type
        /// <typeparamref name="T"/>. This will unwrap any <see cref="Exception"/> and
        /// <see cref="UncaughtExceptionError"/> until it runs out of values to unwrap and returns
        /// null or finds the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to unwrap the <paramref name="value"/> to.</typeparam>
        /// <param name="value">The value to unwrap.</param>
        public static T? UnwrapTo<T>(this Exception value) where T : Exception
        {
            return Exceptions.UnwrapTo(value, typeof(T)) as T;
        }

        /// <summary>
        /// Unwrap the provided <paramref name="value"/> to the provided
        /// <paramref name="targetType"/>. This will unwrap any <see cref="Exception"/> and
        /// <see cref="UncaughtExceptionError"/> until it runs out of values to unwrap and returns
        /// null or finds the <paramref name="targetType"/>.
        /// </summary>
        /// <param name="targetType">The type to unwrap the <paramref name="value"/> to.</param>
        /// <param name="value">The value to unwrap.</param>
        public static Exception? UnwrapTo(this Exception value, Type targetType)
        {
            Pre.Condition.AssertNotNull(value, nameof(value));
            Pre.Condition.AssertNotNull(targetType, nameof(targetType));

            Exception? result = value;
            while (true)
            {
                if (result?.GetType() == targetType)
                {
                    break;
                }
                else if (result is AwaitException awaitException)
                {
                    result = awaitException.InnerException;
                }
                else if (Types.InstanceOf(result, targetType))
                {
                    break;
                }
                else
                {
                    result = null;
                    break;
                }
            }

            return result;
        }
    }
}
