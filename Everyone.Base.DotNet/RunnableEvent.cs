using System;

namespace everyone
{
    /// <summary>
    /// An <see cref="Event"/> that can have its subscribed <see cref="Action"/>s invoked.
    /// </summary>
    public interface RunnableEvent : Event
    {
        /// <summary>
        /// Invoke the <see cref="Action"/>s that have been subscribed to this <see cref="RunnableEvent"/>.
        /// </summary>
        public void Invoke();

        public new static RunnableEvent Create()
        {
            return new BasicRunnableEvent();
        }

        public new static RunnableEvent<T1> Create<T1>()
        {
            return new BasicRunnableEvent<T1>();
        }

        public new static RunnableEvent<T1,T2> Create<T1,T2>()
        {
            return new BasicRunnableEvent<T1,T2>();
        }
    }

    /// <summary>
    /// An <see cref="Event"/> that can have its subscribed <see cref="Action"/>s invoked.
    /// </summary>
    public interface RunnableEvent<T1> : Event<T1>
    {
        /// <summary>
        /// Invoke the <see cref="Action"/>s that have been subscribed to this <see cref="RunnableEvent"/>.
        /// </summary>
        public void Invoke(T1 arg1);
    }

    /// <summary>
    /// An <see cref="Event"/> that can have its subscribed <see cref="Action"/>s invoked.
    /// </summary>
    public interface RunnableEvent<T1,T2> : Event<T1,T2>
    {
        /// <summary>
        /// Invoke the <see cref="Action"/>s that have been subscribed to this <see cref="RunnableEvent"/>.
        /// </summary>
        public void Invoke(T1 arg1, T2 arg2);
    }
}
