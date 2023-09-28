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

        public static BasicResult Create(Exception exception)
        {
            Pre.Condition.AssertNotNull(exception, nameof(exception));

            return new BasicResult(exception);
        }

        public static BasicResult<T> Create<T>()
        {
            return BasicResult<T>.Create(exception: null);
        }

        public static BasicResult<T> Create<T>(Exception exception)
        {
            Pre.Condition.AssertNotNull(exception, nameof(exception));

            return BasicResult<T>.Create(exception);
        }

        public static BasicResult<T> Create<T>(T value)
        {
            return BasicResult<T>.Create(value);
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
        private readonly T? value;
        private readonly Exception? exception;

        protected BasicResult(T? value, Exception? exception)
        {
            this.value = value;
            this.exception = exception;
        }

        public static BasicResult<T> Create(Exception? exception)
        {
            return new BasicResult<T>(value: default, exception: exception);
        }

        public static BasicResult<T> Create(T value)
        {
            return new BasicResult<T>(value: value, exception: null);
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
