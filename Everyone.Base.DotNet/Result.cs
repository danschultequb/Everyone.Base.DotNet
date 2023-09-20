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
        /// Create a new error <see cref="Result"/> from the provided <see cref="Error"/>.
        /// </summary>
        /// <param name="error">The <see cref="Error"/> that the returned <see cref="Result"/> will
        /// contain.</param>
        public static Result Create(Error error)
        {
            return BasicResult.Create(error);
        }

        /// <summary>
        /// Create a new value <see cref="Result{T}"/> from the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="Result{T}"/> is expected to
        /// return.</typeparam>
        /// <param name="value">The value that the returned <see cref="Result{T}"/> will contain.</param>
        public static Result<T> Create<T>(T value)
        {
            return BasicResult.Create(value);
        }

        /// <summary>
        /// Create a new error <see cref="Result{T}"/> from the provided <see cref="Error"/>.
        /// </summary>
        /// <typeparam name="T">The type of value the <see cref="Result{T}"/> is expected to
        /// return.</typeparam>
        /// <param name="error">The <see cref="Error"/> that the returned <see cref="Result{T}"/>
        /// will contain.</param>
        public static Result<T> Create<T>(Error error)
        {
            return BasicResult.Create<T>(error);
        }

        public static Result Create(Action action)
        {
            return LazyResult.Create(action);
        }

        public static Result<T> Create<T>(Func<T> function)
        {
            return LazyResult.Create(function);
        }

        /// <summary>
        /// Allow this <see cref="Result"/> to complete. If this <see cref="Result"/> contains an
        /// <see cref="Error"/>, then the <see cref="Error"/> will be wrapped in an
        /// <see cref="AwaitErrorException"/> and thrown.
        /// </summary>
        public void Await();
    }

    /// <summary>
    /// The <see cref="Result{T}"/> of invoking an operation that can fail.
    /// </summary>
    /// <typeparam name="T">The type of value that the <see cref="Result{T}"/> should return.</typeparam>
    public interface Result<T> : Result
    {
        /// <summary>
        /// Allow this <see cref="Result"/> to complete. If this <see cref="Result"/> contains an
        /// <see cref="Error"/>, then the <see cref="Error"/> will be wrapped in an
        /// <see cref="AwaitErrorException"/> and thrown.
        /// </summary>
        public new T Await();
    }

    public abstract class ResultBase : Result
    {
        public abstract void Await();
    }

    public abstract class ResultBase<T> : Result<T>
    {
        public abstract T Await();

        void Result.Await()
        {
            this.Await();
        }
    }
}
