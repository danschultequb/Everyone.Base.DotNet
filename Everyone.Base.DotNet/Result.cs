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
        public static Result Error(Exception exception)
        {
            return BasicResult.Error(exception);
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
        public static Result<T> Error<T>(Exception exception)
        {
            return BasicResult<T>.Error(exception);
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

        /// <summary>
        /// Get a new <see cref="Result"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        public Result Catch<TException>() where TException : Exception;

        /// <summary>
        /// Get a new <see cref="Result"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Action"/> will be invoked.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="action">The <see cref="Action"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public Result Catch<TException>(Action action) where TException : Exception;

        /// <summary>
        /// Get a new <see cref="Result"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Action{T}"/> will be invoked.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="action">The <see cref="Action"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public Result Catch<TException>(Action<TException> action) where TException : Exception;
    }

    public abstract class ResultBase : Result
    {
        public abstract void Await();

        public Result Then(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                this.Await();
                action.Invoke();
            });
        }

        public Result<T> Then<T>(Func<T> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                this.Await();
                return function.Invoke();
            });
        }

        public Result Catch<TException>() where TException : Exception
        {
            return this.Catch<TException>(() => { });
        }

        public Result Catch<TException>(Action action) where TException : Exception
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return this.Catch((TException exception) => action.Invoke());
        }

        public Result Catch<TException>(Action<TException> action) where TException : Exception
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                try
                {
                    this.Await();
                }
                catch (Exception e)
                {
                    if (e.UnwrapTo<TException>() is TException te)
                    {
                        action.Invoke(te);
                    }
                    else
                    {
                        throw;
                    }
                }
            });
        }
    }

    /// <summary>
    /// The <see cref="Result{T}"/> of invoking an operation that can fail.
    /// </summary>
    /// <typeparam name="T">The type of value that the <see cref="Result{T}"/> should return.</typeparam>
    public interface Result<T> : Result
    {
        void Result.Await()
        {
            this.Await();
        }

        /// <summary>
        /// Allow this <see cref="Result{T}"/> to complete. If this <see cref="Result{T}"/>
        /// contains an <see cref="System.Exception"/>, then the <see cref="System.Exception"/>
        /// will be wrapped in an <see cref="AwaitException"/> and thrown.
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
        /// <see cref="Func{T, TResult}"/> after this <see cref="Result{T}"/> is completed.
        /// </summary>
        /// <typeparam name="U">The type of the value returned by the provided
        /// <see cref="Func{T, TResult}"/>.</typeparam>
        /// <param name="function">The <see cref="Func{T, TResult}"/> to invoke after this
        /// <see cref="Result{T}"/> is completed.</param>
        public Result<U> Then<U>(Func<T, U> function);

        Result Result.Catch<TException>()
        {
            return this.Catch<TException>();
        }

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        public new Result<T?> Catch<TException>() where TException : Exception;

        Result Result.Catch<TException>(Action action)
        {
            return this.Catch<TException>(action);
        }

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Action"/> will be invoked.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="action">The <see cref="Action"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public new Result<T?> Catch<TException>(Action action) where TException : Exception;

        Result Result.Catch<TException>(Action<TException> action)
        {
            return this.Catch<TException>(action);
        }

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Action{T}"/> will be invoked.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="action">The <see cref="Action"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public new Result<T?> Catch<TException>(Action<TException> action) where TException : Exception;

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Func{T,TReturn}"/> will be invoked and the return value
        /// returned.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="function">The <see cref="Func{T, TResult}"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public Result<T> Catch<TException>(Func<T> function) where TException : Exception;

        /// <summary>
        /// Get a new <see cref="Result{T}"/> that will catch <see cref="Exception"/>s of type
        /// <typeparamref name="TException"/>. If a <typeparamref name="TException"/> is caught,
        /// then the provided <see cref="Func{T,TReturn}"/> will be invoked and the return value
        /// returned.
        /// </summary>
        /// <typeparam name="TException">The type of <see cref="Exception"/> to catch.</typeparam>
        /// <param name="function">The <see cref="Func{T, TResult}"/> to invoke when a
        /// <typeparamref name="TException"/> is caught.</param>
        public Result<T> Catch<TException>(Func<TException,T> function) where TException : Exception;
    }

    public abstract class ResultBase<T> : Result<T>
    {
        public abstract T Await();

        public Result Then(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                this.Await();
                action.Invoke();
            });
        }

        public Result Then(Action<T> action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                T value = this.Await();
                action.Invoke(value);
            });
        }
        public Result<U> Then<U>(Func<U> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                this.Await();
                return function.Invoke();
            });
        }

        public Result<U> Then<U>(Func<T, U> function)
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T value = this.Await();
                return function.Invoke(value);
            });
        }

        public Result<T?> Catch<TException>() where TException : Exception
        {
            return this.Catch<TException>((TException e) => { });
        }

        public Result<T?> Catch<TException>(Action action) where TException : Exception
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return this.Catch<TException>((TException e) => { action.Invoke(); });
        }

        public Result<T?> Catch<TException>(Action<TException> action) where TException : Exception
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            return Result.Create(() =>
            {
                T? result = default;
                try
                {
                    result = this.Await();
                }
                catch (Exception e)
                {
                    if (e.UnwrapTo<TException>() is TException te)
                    {
                        action.Invoke(te);
                    }
                    else
                    {
                        throw;
                    }
                }
                return result;
            });
        }

        public Result<T> Catch<TException>(Func<T> function) where TException : Exception
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return this.Catch<TException>((TException e) => { return function.Invoke(); });
        }

        public Result<T> Catch<TException>(Func<TException, T> function) where TException : Exception
        {
            Pre.Condition.AssertNotNull(function, nameof(function));

            return Result.Create(() =>
            {
                T? result = default;
                try
                {
                    result = this.Await();
                }
                catch (Exception e)
                {
                    if (e.UnwrapTo<TException>() is TException te)
                    {
                        result = function.Invoke(te);
                    }
                    else
                    {
                        throw;
                    }
                }
                return result;
            });
        }
    }
}
