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

        /// <summary>
        /// Get this <see cref="Iterator{T}"/>'s current value and advance to the next value.
        /// </summary>
        public T TakeCurrent();

        /// <summary>
        /// Ensure that this <see cref="Iterator{T}"/> has started.
        /// </summary>
        /// <returns>This object for method chaining.</returns>
        public Iterator<T> Start();
    }

    /// <summary>
    /// A base implementation of the <see cref="Iterator{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of values returned by this <see cref="Iterator{T}"/>.</typeparam>
    public abstract class IteratorBase<T,TIterator> : Iterator<T> where TIterator : class, Iterator<T>
    {
        public abstract T Current { get; }

        public abstract DisposableState GetDisposableState();

        object? System.Collections.IEnumerator.Current => this.Current;

        public abstract Result<bool> Dispose();

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

        public virtual T TakeCurrent()
        {
            Pre.Condition.AssertTrue(this.HasCurrent(), "this.HasCurrent()");

            T result = this.Current;
            this.Next();

            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        Iterator<T> Iterator<T>.Start()
        {
            return this.Start();
        }

        public virtual TIterator Start()
        {
            if (!this.HasStarted())
            {
                this.Next();
            }
            return (this as TIterator)!;
        }
    }

    /// <summary>
    /// An abstract base class for <see cref="Iterator{T}"/> types that decorate other
    /// <see cref="Iterator{T}"/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IteratorDecorator<T,TIterator> : IteratorBase<T,TIterator> where TIterator : class, Iterator<T>
    {
        private readonly Iterator<T> innerIterator;

        protected IteratorDecorator(Iterator<T> innerIterator)
        {
            Pre.Condition.AssertNotNull(innerIterator, nameof(innerIterator));

            this.innerIterator = innerIterator;
        }

        public override T Current => this.innerIterator.Current;

        public override DisposableState GetDisposableState()
        {
            return this.innerIterator.GetDisposableState();
        }

        public override Result<bool> Dispose()
        {
            return this.innerIterator.Dispose();
        }

        public override bool HasCurrent()
        {
            return this.innerIterator.HasCurrent();
        }

        public override bool HasStarted()
        {
            return this.innerIterator.HasStarted();
        }

        public override bool Next()
        {
            return this.innerIterator.Next();
        }
    }
}
