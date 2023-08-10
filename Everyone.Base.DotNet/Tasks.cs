using System;
using System.Threading.Tasks;

namespace everyone
{
    public static partial class Tasks
    {
        public static void Await(this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            task.GetAwaiter().GetResult();
        }

        public static T Await<T>(this Task<T> task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            return task.GetAwaiter().GetResult();
        }
    }
}
