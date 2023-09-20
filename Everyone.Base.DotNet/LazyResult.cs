using System;

namespace Everyone
{
    public class LazyResult : Result
    {
        private readonly Action action;
        private Error? error;
        private bool completed;

        protected LazyResult(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            this.action = action;
        }

        public static LazyResult Create(Action action)
        {
            return new LazyResult(action);
        }

        public static LazyResult<T> Create<T>(Func<T> function)
        {
            return LazyResult<T>.Create(function);
        }

        public virtual void Await()
        {
            if (!this.completed)
            {
                this.completed = true;

                try
                {
                    this.action.Invoke();
                }
                catch (Exception e)
                {
                    this.error = UncaughtExceptionError.Create(e);
                }
            }

            if (this.error != null)
            {
                throw new AwaitErrorException(this.error);
            }
        }
    }

    public class LazyResult<T> : ResultBase<T>
    {
        private readonly Func<T> function;
        private Exception? exception;
        private T? value;
        private bool completed;

        protected LazyResult(Func<T> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            this.function = function;
        }

        public static LazyResult<T> Create(Func<T> function)
        {
            return new LazyResult<T>(function);
        }

        public override T Await()
        {
            if (!this.completed)
            {
                this.completed = true;

                try
                {
                    this.value = this.function.Invoke();
                }
                catch (Exception e)
                {
                    this.exception = e;
                }
            }

            if (this.exception != null)
            {
                throw new AwaitException(this.exception);
            }

            return this.value!;
        }
    }
}
