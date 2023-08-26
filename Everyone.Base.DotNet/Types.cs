using System;

namespace Everyone
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

        public static string GetFullName<T>()
        {
            return Types.GetFullName(Types.GetType<T>());
        }

        public static string GetFullName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.FullName ?? type.Name;
        }

        public static string GetName<T>()
        {
            return Types.GetName(Types.GetType<T>());
        }

        public static string GetName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.Name;
        }

        public static string GetFullName<T>(T? value)
        {
            return Types.GetFullName(Types.GetType<T>(value));
        }

        /// <summary>
        /// Get whether this <paramref name="value"/> is an instance of the provided
        /// <paramref name="baseType"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the this <paramref name="value"/>.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="baseType">The base <see cref="Type"/> to look for.</param>
        public static bool InstanceOf<T>(this T? value, Type baseType)
        {
            Type valueType = Types.GetType<T>(value);
            return Types.InstanceOf(valueType, baseType);
        }

        /// <summary>
        /// Get whether this <see cref="Type"/> is an instance of the provided
        /// <paramref name="baseType"/>.
        /// </summary>
        /// <param name="valueType">The <see cref="Type"/> to check.</param>
        /// <param name="baseType">The base <see cref="Type"/> to look for.</param>
        public static bool InstanceOf(this Type valueType, Type baseType)
        {
            if (valueType == null)
            {
                throw new ArgumentNullException(nameof(valueType));
            }
            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            return baseType.IsAssignableFrom(valueType);
        }
    }
}
