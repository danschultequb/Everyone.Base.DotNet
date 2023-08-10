using System;

namespace everyone
{
    public static partial class Types
    {
        public static Type GetType<T>()
        {
            return typeof(T);
        }

        /// <summary>
        /// Get the <see cref="Type"/> of the provided <paramref name="value"/>. If
        /// <paramref name="value"/> is null, then get the <see cref="Type"/> of
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the provided <paramref name="value"/>.</typeparam>
        /// <param name="value">The provided value.</param>
        public static Type GetType<T>(T? value)
        {
            return value?.GetType() ?? Types.GetType<T>();
        }

        public static string GetTypeFullName(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return type.FullName ?? type.Name;
        }

        public static string GetTypeFullName<T>()
        {
            return Types.GetTypeFullName(Types.GetType<T>());
        }

        public static string GetTypeFullName<T>(T? value)
        {
            return Types.GetTypeFullName(Types.GetType<T>(value));
        }
    }
}
