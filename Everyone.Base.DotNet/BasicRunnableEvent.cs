using System;
using System.Collections.Generic;

namespace everyone
{
    public class BasicRunnableEvent : RunnableEvent
    {
        private readonly List<Action> actions = new List<Action>();

        public Disposable Subscribe(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.actions.Add(action);
            return Disposable.Create(() =>
            {
                this.actions.Remove(action);
            });
        }

        public void Invoke()
        {
            foreach (Action action in this.actions)
            {
                action.Invoke();
            }
        }
    }

    public class BasicRunnableEvent<T1> : RunnableEvent<T1>
    {
        private readonly List<Action<T1>> actions = new List<Action<T1>>();

        public Disposable Subscribe(Action<T1> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.actions.Add(action);
            return Disposable.Create(() =>
            {
                this.actions.Remove(action);
            });
        }

        public void Invoke(T1 arg1)
        {
            foreach (Action<T1> action in this.actions)
            {
                action.Invoke(arg1);
            }
        }
    }

    public class BasicRunnableEvent<T1,T2> : RunnableEvent<T1,T2>
    {
        private readonly List<Action<T1,T2>> actions = new List<Action<T1,T2>>();

        public Disposable Subscribe(Action<T1,T2> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.actions.Add(action);
            return Disposable.Create(() =>
            {
                this.actions.Remove(action);
            });
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            foreach (Action<T1,T2> action in this.actions)
            {
                action.Invoke(arg1, arg2);
            }
        }
    }
}
