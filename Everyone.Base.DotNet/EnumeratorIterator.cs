using System.Collections;
using System.Collections.Generic;

namespace everyone
{
    public class EnumeratorIterator<T> : Iterator<T>
    {
        private readonly IEnumerator<T> enumerator;
        private bool hasStarted;
        private bool hasCurrent;

        internal EnumeratorIterator(IEnumerator<T> enumerator)
        {
            PreCondition.AssertNotNull(enumerator, nameof(enumerator));

            this.enumerator = enumerator;
        }

        object? IEnumerator.Current => this.Current;

        public T Current
        {
            get
            {
                PreCondition.AssertTrue(this.HasCurrent(), nameof(this.HasCurrent));

                return this.enumerator.Current;
            }
        }

        public bool HasCurrent()
        {
            return this.hasCurrent;
        }

        public bool HasStarted()
        {
            return this.hasStarted;
        }

        public bool Next()
        {
            this.hasStarted = true;

            this.hasCurrent = this.enumerator.MoveNext();

            return this.HasCurrent();
        }

        public bool Disposed { get; private set; }

        public bool Dispose()
        {
            bool result = !this.Disposed;
            if (result)
            {
                this.Disposed = true;

                this.enumerator.Dispose();
            }
            return result;
        }
    }

    public static class EnumeratorIterator
    {
        public static EnumeratorIterator<T> Create<T>(IEnumerator<T> enumerator)
        {
            return new EnumeratorIterator<T>(enumerator);
        }
    }
}
