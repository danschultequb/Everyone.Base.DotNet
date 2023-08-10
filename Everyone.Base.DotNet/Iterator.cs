using System;
using System.Collections;
using System.Collections.Generic;

namespace everyone
{
    /// <summary>
    /// An object that can iterate over a sequence of values.
    /// </summary>
    /// <typeparam name="T">The type of values that are returned by this <see cref="Iterator{T}"/>.</typeparam>
    public interface Iterator<T> : IEnumerator<T>, Disposable
    {
        /// <summary>
        /// Get whether this <see cref="Iterator{T}"/> has started iterating.
        /// </summary>
        public bool HasStarted();

        /// <summary>
        /// Get whether this <see cref="Iterator{T}"/> has a current value.
        /// </summary>
        public bool HasCurrent();

        /// <summary>
        /// Move this <see cref="Iterator{T}"/> to its next value.
        /// </summary>
        public bool Next();

        /// <inheritdoc/>
        bool IEnumerator.MoveNext()
        {
            return this.Next();
        }

        /// <inheritdoc/>
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }

    public static class Iterator
    {
        public static Iterator<T> Create<T>(IEnumerator<T> enumerator)
        {
            PreCondition.AssertNotNull(enumerator, nameof(enumerator));

            return EnumeratorIterator.Create(enumerator);
        }
    }
}
