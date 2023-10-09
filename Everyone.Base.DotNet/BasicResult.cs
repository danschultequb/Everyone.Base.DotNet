using System;

namespace Everyone
{
    public class BasicResult : ResultBase
    {
        private readonly Exception? exception;

        protected BasicResult(Exception? exception)
        {
            this.exception = exception;
        }

        public static BasicResult Create()
        {
            return new BasicResult(exception: null);
        }

        public static BasicResult Error(Exception exception)
        {
            Pre.Condition.AssertNotNull(exception, nameof(exception));

            return new BasicResult(exception);
        }

        public override void Await()
        {
            if (this.exception != null)
            {
                throw new AwaitException(this.exception);
            }
        }
    }

    public class BasicResult<T> : ResultBase<T>
    {
        private readonly Exception? exception;
        private readonly T? value;

        protected BasicResult(T? value, Exception? exception)
        {
            this.value = value;
            this.exception = exception;
        }

        public static BasicResult<T> Error(Exception exception)
        {
            Pre.Condition.AssertNotNull(exception, nameof(exception));

            return new BasicResult<T>(value: default, exception: exception);
        }

        public static BasicResult<T> Create(T value)
        {
            return new BasicResult<T>(value: value, exception: default);
        }

        public override T Await()
        {
            if (this.exception != null)
            {
                throw new AwaitException(this.exception);
            }
            return this.value!;
        }
    }
}
