using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Event"/> that can have <see cref="Action"/>s subscribed to it. When the
    /// <see cref="Event"/> is invoked, the subscribed <see cref="Action"/>s will be invoked.
    /// </summary>
    public interface Event
    {
        /// <summary>
        /// Subscribe the provided <see cref="Action"/> to this <see cref="Event"/> so that when
        /// this <see cref="Event"/> is invoked, the provided <see cref="Action"/> will also be
        /// invoked.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to subscribe.</param>
        /// <returns>A <see cref="Disposable"/> that will unsubscribe the provided
        /// <see cref="Action"/> when it is disposed.</returns>
        public Disposable Subscribe(Action action);

        public static RunnableEvent Create()
        {
            return RunnableEvent.Create();
        }

        public static RunnableEvent<T1> Create<T1>()
        {
            return RunnableEvent.Create<T1>();
        }

        public static RunnableEvent<T1,T2> Create<T1,T2>()
        {
            return RunnableEvent.Create<T1,T2>();
        }
    }

    /// <summary>
    /// An <see cref="Event"/> that can have <see cref="Action"/>s subscribed to it. When the
    /// <see cref="Event"/> is invoked, the subscribed <see cref="Action"/>s will be invoked.
    /// </summary>
    public interface Event<T1>
    {
        /// <summary>
        /// Subscribe the provided <see cref="Action"/> to this <see cref="Event"/> so that when
        /// this <see cref="Event"/> is invoked, the provided <see cref="Action"/> will also be
        /// invoked.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to subscribe.</param>
        /// <returns>A <see cref="Disposable"/> that will unsubscribe the provided
        /// <see cref="Action"/> when it is disposed.</returns>
        public Disposable Subscribe(Action<T1> action);
    }

    /// <summary>
    /// An <see cref="Event"/> that can have <see cref="Action"/>s subscribed to it. When the
    /// <see cref="Event"/> is invoked, the subscribed <see cref="Action"/>s will be invoked.
    /// </summary>
    public interface Event<T1,T2>
    {
        /// <summary>
        /// Subscribe the provided <see cref="Action"/> to this <see cref="Event"/> so that when
        /// this <see cref="Event"/> is invoked, the provided <see cref="Action"/> will also be
        /// invoked.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to subscribe.</param>
        /// <returns>A <see cref="Disposable"/> that will unsubscribe the provided
        /// <see cref="Action"/> when it is disposed.</returns>
        public Disposable Subscribe(Action<T1,T2> action);
    }
}
