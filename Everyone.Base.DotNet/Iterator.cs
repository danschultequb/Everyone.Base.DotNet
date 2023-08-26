using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Everyone
{
    public static class Iterator
    {
        public static Iterator<T> Create<T>(params T[] values)
        {
            return Iterator.Create<T>(values.ToImmutableList().GetEnumerator());
        }

        public static Iterator<T> Create<T>(IEnumerator<T> enumerator)
        {
            return EnumeratorIterator.Create(enumerator);
        }
    }

    /// <summary>
    /// An object that can iterate over a sequence of values.
    /// </summary>
    /// <typeparam name="T">The type of values that are returned by this <see cref="Iterator{T}"/>.</typeparam>
    public interface Iterator<T> : IEnumerable<T>, IEnumerator<T>, Disposable
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
    }

    /// <summary>
    /// A base implementation of the <see cref="Iterator{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of values returned by this <see cref="Iterator{T}"/>.</typeparam>
    public abstract class IteratorBase<T> : Iterator<T>
    {
        public abstract T Current { get; }

        public abstract bool Disposed { get; }

        object? IEnumerator.Current => this.Current;

        public abstract bool Dispose();

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public abstract bool HasCurrent();

        public abstract bool HasStarted();

        public bool MoveNext()
        {
            return this.Next();
        }

        public abstract bool Next();

        public void Reset()
        {
            throw new System.NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
