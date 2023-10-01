using System;

namespace Everyone
{
    /// <summary>
    /// The <see cref="Result"/> of invoking an operation that can fail.
    /// </summary>
    public interface Result
    {
        /// <summary>
        /// Create a new successful <see cref="Result"/>.
        /// </summary>
        public static Result Create()
        {
            return BasicResult.Create();
        }

        /// <summary>
        /// Create a new error <see cref="Result"/> from the provided <see cref="Exception"/>.
        /// </summary>
        /// <param name="exception">The <see cref="System.Exception"/> that the returned
        /// <see cref="Result"/> will contain.</param>
        public static Result Create(Exception exception)
        {
            return BasicResult.Create(exception);
        }

        /// <summary>
        /// Create a new value <see cref="Result{T}"/> from the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="Result{T}"/> is expected to
        /// return.</typeparam>
        /// <param name="value">The value that the returned <see cref="Result{T}"/> will contain.</param>
        public static Result<T> Create<T>(T value)
        {
            return BasicResult<T>.Create(value);
        }

        /// <summary>
        /// Create a new <see cref="Result{T}"/> from the provided <see cref="System.Exception"/>.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="Result{T}"/> is expected to
        /// return.</typeparam>
        /// <param name="exception">The <see cref="System.Exception"/> that the returned
        /// <see cref="Result{T}"/> will contain.</param>
        public static Result<T> Create<T>(Exception exception)
        {
            return BasicResult<T>.Create(exception);
        }

        /// <summary>
        /// Create a new <see cref="Result"/> that will invoke the provided <see cref="Action"/>
        /// when it is awaited.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke when the returned
        /// <see cref="Result"/> is awaited.</param>
        public static Result Create(Action action)
        {
            return LazyResult.Create(action);
        }

        /// <summary>
        /// Create a new <see cref="Result{T}"/> that will invoke the provided
        /// <see cref="Func{T}"/> when it is awaited.
        /// </summary>
        /// <typeparam name="T">The type of value the proivded <see cref="Func{T}"/> returns.</typeparam>
        /// <param name="function">The <see cref="Func{T}"/> to invoke when the returned
        /// <see cref="Result{T}"/> is awaited.</param>
        public static Result<T> Create<T>(Func<T> function)
        {
            return LazyResult.Create(function);
        }

        /// <summary>
        /// Allow this <see cref="Result"/> to complete. If this <see cref="Result"/> contains an
        /// <see cref="System.Exception"/>, then the <see cref="System.Exception"/> will be wrapped
        /// in an <see cref="AwaitException"/> and thrown.
        /// </summary>
        public void Await();

        /// <summary>
        /// Get a new <see cref="Result"/> that contains the result of invoking the provided
        /// <see cref="Action"/> after this <see cref="Result"/> is completed.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke after this <see cref="Result"/>
        /// is completed.</param>
        public Result Then(Action action);

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that contains the result of invoking the provided
        /// <see cref="Func{T}"/> after this <see cref="Result"/> is completed.
        /// </summary>
        /// <typeparam name="T">The type of the value returned by the provided
        /// <see cref="Func{T}"/>.</typeparam>
        /// <param name="function">The <see cref="Func{T}"/> to invoke after this
        /// <see cref="Result"/> is completed.</param>
        public Result<T> Then<T>(Func<T> function);
    }

    /// <summary>
    /// The <see cref="Result{T}"/> of invoking an operation that can fail.
    /// </summary>
    /// <typeparam name="T">The type of value that the <see cref="Result{T}"/> should return.</typeparam>
    public interface Result<T> : Result
    {
        /// <summary>
        /// Allow this <see cref="Result"/> to complete. If this <see cref="Result"/> contains an
        /// <see cref="System.Exception"/>, then the <see cref="System.Exception"/> will be wrapped
        /// in an <see cref="AwaitException"/> and thrown.
        /// </summary>
        public new T Await();

        /// <summary>
        /// Get a new <see cref="Result"/> that contains the result of invoking the provided
        /// <see cref="Action{T}"/> after this <see cref="Result{T}"/> is completed.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to invoke after this
        /// <see cref="Result{T}"/> is completed.</param>
        public Result Then(Action<T> action);

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that contains the result of invoking the provided
        /// <see cref="Func{T, TResult}"/> after this <see cref="Result"/> is completed.
        /// </summary>
        /// <typeparam name="U">The type of the value returned by the provided
        /// <see cref="Func{T, TResult}"/>.</typeparam>
        /// <param name="function">The <see cref="Func{T, TResult}"/> to invoke after this
        /// <see cref="Result{T}"/> is completed.</param>
        public Result<U> Then<U>(Func<T,U> function);
    }

    public abstract class ResultBase : Result
    {
        public abstract void Await();

        public virtual Result Then(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                this.Await();
                action.Invoke();
            });
        }

        public virtual Result<T> Then<T>(Func<T> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                this.Await();
                return function.Invoke();
            });
        }
    }

    public abstract class ResultBase<T> : Result<T>
    {
        public abstract T Await();

        void Result.Await()
        {
            this.Await();
        }

        public virtual Result Then(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                this.Await();
                action.Invoke();
            });
        }

        public virtual Result Then(Action<T> action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                T value = this.Await();
                action.Invoke(value);
            });
        }

        public virtual Result<U> Then<U>(Func<U> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                this.Await();
                return function.Invoke();
            });
        }

        public virtual Result<U> Then<U>(Func<T,U> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T value = this.Await();
                return function.Invoke(value);
            });
        }
    }
}
