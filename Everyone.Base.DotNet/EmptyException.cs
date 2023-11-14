using System;

namespace Everyone
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when an object is attempted to be accessed when
    /// it is empty.
    /// </summary>
    public class EmptyException : Exception
    {
        public EmptyException()
        {
        }
    }
}
