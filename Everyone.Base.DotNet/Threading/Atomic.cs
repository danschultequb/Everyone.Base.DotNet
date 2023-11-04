namespace Everyone.Threading
{
    /// <summary>
    /// A collection of atomic operations.
    /// </summary>
    public static class Atomic
    {
        /// <summary>
        /// Compare the value in the provided <paramref name="valuePointer"/> to the
        /// <paramref name="expectedValue"/>. If they are equal, set the
        /// <paramref name="valuePointer"/> to the provided <paramref name="newValue"/> and return
        /// true. If they are not equal, don't modify the <paramref name="valuePointer"/> and
        /// return false.
        /// </summary>
        /// <param name="valuePointer">The pointer to the value to modify.</param>
        /// <param name="expectedValue">The value the <paramref name="valuePointer"/> is expected
        /// to hold.</param>
        /// <param name="newValue">The value to set the <paramref name="valuePointer"/> to if it
        /// contains the <paramref name="expectedValue"/>.</param>
        public static bool CompareAndSet(ref int valuePointer, int expectedValue, int newValue)
        {
            return expectedValue == System.Threading.Interlocked.CompareExchange(location1: ref valuePointer, value: newValue, comparand: expectedValue);
        }

        /// <summary>
        /// Compare the value in the provided <paramref name="valuePointer"/> to the
        /// <paramref name="expectedValue"/>. If they are equal, set the
        /// <paramref name="valuePointer"/> to the provided <paramref name="newValue"/> and return
        /// true. If they are not equal, don't modify the <paramref name="valuePointer"/> and
        /// return false.
        /// </summary>
        /// <param name="valuePointer">The pointer to the value to modify.</param>
        /// <param name="expectedValue">The value the <paramref name="valuePointer"/> is expected
        /// to hold.</param>
        /// <param name="newValue">The value to set the <paramref name="valuePointer"/> to if it
        /// contains the <paramref name="expectedValue"/>.</param>
        public static bool CompareAndSet(ref long valuePointer, long expectedValue, long newValue)
        {
            return expectedValue == System.Threading.Interlocked.CompareExchange(location1: ref valuePointer, value: newValue, comparand: expectedValue);
        }
    }
}
