using System;

namespace Everyone
{
    /// <summary>
    /// A collection of functions that can be used to convert objects of different types to their
    /// <see cref="string"/> representation.
    /// </summary>
    public interface MutableToStringFunctions : ToStringFunctions
    {
        /// <summary>
        /// Create a new <see cref="MutableToStringFunctions"/> object.
        /// </summary>
        /// <param name="addDefaultFunctions">Whether to create an empty
        /// <see cref="ToStringFunctions"/> object (false) or to add the default initial functions
        /// (true).</param>
        public static new MutableToStringFunctions Create(bool addDefaultFunctions = true)
        {
            return BasicToStringFunctions.Create(addDefaultFunctions);
        }

        /// <summary>
        /// Add the <see cref="Func{T, string}"/> that will be used to convert an object of type
        /// <typeparamref name="T"/> to <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to convert.</typeparam>
        /// <param name="toStringFunction">The <see cref="Func{T, string}"/> to add.</param>
        /// <returns>A <see cref="Disposable"/> that can be used to unregister the provided
        /// <paramref name="toStringFunction"/>.</returns>
        public Disposable AddToStringFunction<T>(Func<T?, string> toStringFunction);
    }
}
