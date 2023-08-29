namespace Everyone
{
    /// <summary>
    /// An object that can iterate over a sequence of values.
    /// </summary>
    public static partial class Iterator
    {
        public static Iterator<T> Create<T>(params T[] values)
        {
            return Iterator.Create<T>(System.Linq.Enumerable.ToList(values).GetEnumerator());
        }

        public static Iterator<T> Create<T>(System.Collections.Generic.IEnumerator<T> enumerator)
        {
            return EnumeratorIterator.Create(enumerator);
        }
    }

    /// <summary>
    /// An object that can iterate over a sequence of values.
    /// </summary>
    /// <typeparam name="T">The type of values that are returned by this <see cref="Iterator{T}"/>.</typeparam>
    public interface Iterator<T> : System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IEnumerator<T>, Disposable
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

        object? System.Collections.IEnumerator.Current => this.Current;

        public abstract bool Dispose();

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
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

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }
    }

    /// <summary>
    /// An abstract base class for <see cref="Iterator{T}"/> types that decorate other
    /// <see cref="Iterator{T}"/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IteratorDecorator<T> : IteratorBase<T>
    {
        protected readonly Iterator<T> innerDecorator;

        protected IteratorDecorator(Iterator<T> innerDecorator)
        {
            Pre.Condition.AssertNotNull(innerDecorator, nameof(innerDecorator));

            this.innerDecorator = innerDecorator;
        }

        public override T Current => this.innerDecorator.Current;

        public override bool Disposed => this.innerDecorator.Disposed;

        public override bool Dispose()
        {
            return this.innerDecorator.Dispose();
        }

        public override bool HasCurrent()
        {
            return this.innerDecorator.HasCurrent();
        }

        public override bool HasStarted()
        {
            return this.innerDecorator.HasStarted();
        }

        public override bool Next()
        {
            return this.innerDecorator.Next();
        }
    }
}
