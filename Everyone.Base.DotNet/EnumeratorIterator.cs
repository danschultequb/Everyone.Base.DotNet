using System.Collections.Generic;

namespace Everyone
{
    public static class EnumeratorIterator
    {
        public static EnumeratorIterator<T> Create<T>(IEnumerator<T> enumerator)
        {
            return EnumeratorIterator<T>.Create(enumerator);
        }
    }

    public class EnumeratorIterator<T> : IteratorBase<T, EnumeratorIterator<T>>
    {
        private readonly IEnumerator<T> enumerator;
        private bool disposed;
        private bool hasStarted;
        private bool hasCurrent;

        private EnumeratorIterator(IEnumerator<T> enumerator)
        {
            Pre.Condition.AssertNotNull(enumerator, nameof(enumerator));

            this.enumerator = enumerator;
        }

        internal static EnumeratorIterator<T> Create(IEnumerator<T> enumerator)
        {
            return new EnumeratorIterator<T>(enumerator);
        }

        public override bool Disposed => this.disposed;

        public override bool Dispose()
        {
            bool result = !this.disposed;
            if (result)
            {
                this.disposed = true;

                this.enumerator.Dispose();
            }
            return result;
        }

        public override bool HasCurrent()
        {
            return this.hasCurrent;
        }

        public override bool HasStarted()
        {
            return this.hasStarted;
        }

        public override T Current
        {
            get
            {
                Pre.Condition.AssertTrue(this.HasCurrent(), "this.HasCurrent()");

                return this.enumerator.Current;
            }
        }

        public override bool Next()
        {
            this.hasStarted = true;
            this.hasCurrent = this.enumerator.MoveNext();
            return this.HasCurrent();
        }
    }
}
