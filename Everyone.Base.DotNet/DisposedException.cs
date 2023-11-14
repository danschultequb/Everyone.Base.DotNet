using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when an object is disposed while an operation is
    /// being performed.
    /// </summary>
    public class DisposedException : Exception
    {
        public DisposedException()
            : base("The operation cannot be performed because the object was disposed.")
        {
        }
    }
}
