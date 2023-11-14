using System;

namespace Everyone
{
    public class BasicRunnableEvent : RunnableEvent
    {
        private readonly List<Action> actions = List.Create<Action>();

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
        private readonly List<Action<T1>> actions = List.Create<Action<T1>>();

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
        private readonly List<Action<T1,T2>> actions = List.Create<Action<T1,T2>>();

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
