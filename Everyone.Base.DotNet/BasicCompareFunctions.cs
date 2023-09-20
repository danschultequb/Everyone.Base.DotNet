using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Everyone
{
    /// <summary>
    /// A basic implementation of the <see cref="CompareFunctions"/> interface.
    /// </summary>
    public class BasicCompareFunctions : CompareFunctionsBase
    {
        private readonly IDictionary<Type,IDictionary<Type, Func<object?, object?, bool>>> equalFunctionMap;

        private readonly IDictionary<Tuple<Type, Type>, Func<object?, object?, Comparison>> compareFunctionMap;
        private readonly bool addDefaultCompareFunctions;

        private BasicCompareFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            this.equalFunctionMap = new Dictionary<Type,IDictionary<Type, Func<object?, object?, bool>>>();
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction<IEnumerable, IEnumerable>(this.EnumerablesEqual);
                this.AddEqualFunction<ITuple, ITuple>(this.TupleEqual);
                this.AddEqualFunction<Exception, Exception>(this.ExceptionEqual);
            }

            this.compareFunctionMap = new Dictionary<Tuple<Type, Type>, Func<object?, object?, Comparison>>();
            this.addDefaultCompareFunctions = addDefaultCompareFunctions;

            this.AddByteFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddSByteFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddShortFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddUShortFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddIntFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddUIntFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddLongFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddULongFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddNIntFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddNUIntFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddFloatFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddDoubleFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
            this.AddDecimalFunctions(addDefaultEqualFunctions, addDefaultCompareFunctions);
        }

        private void AddByteFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((byte lhs, byte rhs) => (int)lhs == (int)rhs);
                this.AddEqualFunction((byte lhs, sbyte rhs) => (int)lhs == (int)rhs);
                this.AddEqualFunction((byte lhs, short rhs) => (int)lhs == (int)rhs);
                this.AddEqualFunction((byte lhs, ushort rhs) => (int)lhs == (int)rhs);
                this.AddEqualFunction((byte lhs, int rhs) => (int)lhs == (int)rhs);
                this.AddEqualFunction((byte lhs, uint rhs) => (uint)lhs == (uint)rhs);
                this.AddEqualFunction((byte lhs, long rhs) => (long)lhs == (long)rhs);
                this.AddEqualFunction((byte lhs, ulong rhs) => (ulong)lhs == (ulong)rhs);
                this.AddEqualFunction((byte lhs, nint rhs) => (nint)lhs == (nint)rhs);
                this.AddEqualFunction((byte lhs, nuint rhs) => (nuint)lhs == (nuint)rhs);
                this.AddEqualFunction((byte lhs, float rhs) => (float)lhs == (float)rhs);
                this.AddEqualFunction((byte lhs, double rhs) => (double)lhs == (double)rhs);
                this.AddEqualFunction((byte lhs, decimal rhs) => (decimal)lhs == (decimal)rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((byte lhs, byte rhs) => ((byte)lhs).CompareTo((byte)rhs));
                this.AddCompareFunction((byte lhs, sbyte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((byte lhs, short rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((byte lhs, ushort rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((byte lhs, int rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((byte lhs, uint rhs) => ((uint)lhs).CompareTo((uint)rhs));
                this.AddCompareFunction((byte lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((byte lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                this.AddCompareFunction((byte lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((byte lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                this.AddCompareFunction((byte lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((byte lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((byte lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddSByteFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((sbyte lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, long rhs) => lhs == rhs);
                // this.AddEqualFunction((sbyte lhs, ulong rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((sbyte lhs, nint rhs) => lhs == rhs);
                // this.AddEqualFunction((sbyte lhs, nuint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((sbyte lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((sbyte lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((sbyte lhs, byte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((sbyte lhs, sbyte rhs) => ((sbyte)lhs).CompareTo((sbyte)rhs));
                this.AddCompareFunction((sbyte lhs, short rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((sbyte lhs, ushort rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((sbyte lhs, int rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((sbyte lhs, uint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((sbyte lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((sbyte lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambigous comparison
                this.AddCompareFunction((sbyte lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                // this.AddCompareFunction((sbyte lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((sbyte lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((sbyte lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((sbyte lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddShortFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((short lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, long rhs) => lhs == rhs);
                // this.AddEqualFunction((short lhs, ulong rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((short lhs, nint rhs) => lhs == rhs);
                // this.AddEqualFunction((short lhs, nuint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((short lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((short lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((short lhs, byte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((short lhs, sbyte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((short lhs, short rhs) => ((short)lhs).CompareTo((short)rhs));
                this.AddCompareFunction((short lhs, ushort rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((short lhs, int rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((short lhs, uint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((short lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((short lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambigous comparison
                this.AddCompareFunction((short lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                // this.AddCompareFunction((short lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((short lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((short lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((short lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddUShortFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((ushort lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, long rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, ulong rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, nint rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((ushort lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((ushort lhs, byte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((ushort lhs, sbyte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((ushort lhs, short rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((ushort lhs, ushort rhs) => ((ushort)lhs).CompareTo((ushort)rhs));
                this.AddCompareFunction((ushort lhs, int rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((ushort lhs, uint rhs) => ((uint)lhs).CompareTo((uint)rhs));
                this.AddCompareFunction((ushort lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((ushort lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                this.AddCompareFunction((ushort lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((ushort lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                this.AddCompareFunction((ushort lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((ushort lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((ushort lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddIntFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((int lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, long rhs) => lhs == rhs);
                // this.AddEqualFunction((int lhs, ulong rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((int lhs, nint rhs) => lhs == rhs);
                // this.AddEqualFunction((int lhs, nuint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((int lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((int lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((int lhs, byte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((int lhs, sbyte rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((int lhs, short rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((int lhs, ushort rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((int lhs, int rhs) => ((int)lhs).CompareTo((int)rhs));
                this.AddCompareFunction((int lhs, uint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((int lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((int lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambigous comparison
                this.AddCompareFunction((int lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                // this.AddCompareFunction((int lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((int lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((int lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((int lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddUIntFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((uint lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, long rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, ulong rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, nint rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((uint lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((uint lhs, byte rhs) => ((uint)lhs).CompareTo((uint)rhs));
                this.AddCompareFunction((uint lhs, sbyte rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((uint lhs, short rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((uint lhs, ushort rhs) => ((uint)lhs).CompareTo((uint)rhs));
                this.AddCompareFunction((uint lhs, int rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((uint lhs, uint rhs) => ((uint)lhs).CompareTo((uint)rhs));
                this.AddCompareFunction((uint lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((uint lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                this.AddCompareFunction((uint lhs, nint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((uint lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                this.AddCompareFunction((uint lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((uint lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((uint lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddLongFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((long lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, long rhs) => lhs == rhs);
                // this.AddEqualFunction((long lhs, ulong rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((long lhs, nint rhs) => lhs == rhs);
                // this.AddEqualFunction((long lhs, nuint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((long lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((long lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((long lhs, byte rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, sbyte rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, short rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, ushort rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, int rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, uint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((long lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((long lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambigous comparison
                this.AddCompareFunction((long lhs, nint rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((long lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((long lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((long lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((long lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddULongFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((ulong lhs, byte rhs) => lhs == rhs);
                //this.AddEqualFunction((ulong lhs, sbyte rhs) => lhs == rhs); // Ambiguous comparison
                //this.AddEqualFunction((ulong lhs, short rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((ulong lhs, ushort rhs) => lhs == rhs);
                //this.AddEqualFunction((ulong lhs, int rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((ulong lhs, uint rhs) => lhs == rhs);
                //this.AddEqualFunction((ulong lhs, long rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((ulong lhs, ulong rhs) => lhs == rhs);
                //this.AddEqualFunction((ulong lhs, nint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((ulong lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((ulong lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((ulong lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((ulong lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((ulong lhs, byte rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                // this.AddCompareFunction((ulong lhs, sbyte rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambiguous comparison
                // this.AddCompareFunction((ulong lhs, short rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambiguous comparison
                this.AddCompareFunction((ulong lhs, ushort rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                // this.AddCompareFunction((ulong lhs, int rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambiguous comparison
                this.AddCompareFunction((ulong lhs, uint rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                // this.AddCompareFunction((ulong lhs, long rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambiguous comparison
                this.AddCompareFunction((ulong lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                // this.AddCompareFunction((ulong lhs, nint rhs) => ((ulong)lhs).CompareTo((ulong)rhs)); // Ambiguous comparison
                this.AddCompareFunction((ulong lhs, nuint rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                this.AddCompareFunction((ulong lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((ulong lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((ulong lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddNIntFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((nint lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, long rhs) => lhs == rhs);
                // this.AddEqualFunction((nint lhs, ulong rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nint lhs, nint rhs) => lhs == rhs);
                // this.AddEqualFunction((nint lhs, nuint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nint lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((nint lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((nint lhs, byte rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((nint lhs, sbyte rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((nint lhs, short rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((nint lhs, ushort rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((nint lhs, int rhs) => ((nint)lhs).CompareTo((nint)rhs));
                this.AddCompareFunction((nint lhs, uint rhs) => ((long)lhs).CompareTo((long)rhs));
                this.AddCompareFunction((nint lhs, long rhs) => ((long)lhs).CompareTo((long)rhs));
                // this.AddCompareFunction((nint lhs, ulong rhs) => ((nint)lhs).CompareTo((nint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nint lhs, nint rhs) => ((nint)lhs).CompareTo((nint)rhs));
                // this.AddCompareFunction((nint lhs, nuint rhs) => ((nint)lhs).CompareTo((nint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nint lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((nint lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((nint lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddNUIntFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((nuint lhs, byte rhs) => lhs == rhs);
                // this.AddEqualFunction((nuint lhs, sbyte rhs) => lhs == rhs); // Ambiguous comparison
                // this.AddEqualFunction((nuint lhs, short rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nuint lhs, ushort rhs) => lhs == rhs);
                // this.AddEqualFunction((nuint lhs, int rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nuint lhs, uint rhs) => lhs == rhs);
                // this.AddEqualFunction((nuint lhs, long rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nuint lhs, ulong rhs) => lhs == rhs);
                // this.AddEqualFunction((nuint lhs, nint rhs) => lhs == rhs); // Ambiguous comparison
                this.AddEqualFunction((nuint lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((nuint lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((nuint lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((nuint lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((nuint lhs, byte rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                // this.AddCompareFunction((nuint lhs, sbyte rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                // this.AddCompareFunction((nuint lhs, short rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nuint lhs, ushort rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                // this.AddCompareFunction((nuint lhs, int rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nuint lhs, uint rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                // this.AddCompareFunction((nuint lhs, long rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nuint lhs, ulong rhs) => ((ulong)lhs).CompareTo((ulong)rhs));
                // this.AddCompareFunction((nuint lhs, nint rhs) => ((nuint)lhs).CompareTo((nuint)rhs)); // Ambigous comparison
                this.AddCompareFunction((nuint lhs, nuint rhs) => ((nuint)lhs).CompareTo((nuint)rhs));
                this.AddCompareFunction((nuint lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((nuint lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((nuint lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddFloatFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((float lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, long rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, ulong rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, nint rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((float lhs, decimal rhs) => (decimal)lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((float lhs, byte rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, sbyte rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, short rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, ushort rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, int rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, uint rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, long rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, ulong rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, nint rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, nuint rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, float rhs) => ((float)lhs).CompareTo((float)rhs));
                this.AddCompareFunction((float lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((float lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddDoubleFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((double lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, long rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, ulong rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, nint rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, float rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, double rhs) => lhs == rhs);
                this.AddEqualFunction((double lhs, decimal rhs) => (decimal)lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((double lhs, byte rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, sbyte rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, short rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, ushort rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, int rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, uint rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, long rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, ulong rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, nint rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, nuint rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, float rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, double rhs) => ((double)lhs).CompareTo((double)rhs));
                this.AddCompareFunction((double lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        private void AddDecimalFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction((decimal lhs, byte rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, sbyte rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, short rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, ushort rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, int rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, uint rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, long rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, ulong rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, nint rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, nuint rhs) => lhs == rhs);
                this.AddEqualFunction((decimal lhs, float rhs) => lhs == (decimal)rhs);
                this.AddEqualFunction((decimal lhs, double rhs) => lhs == (decimal)rhs);
                this.AddEqualFunction((decimal lhs, decimal rhs) => lhs == rhs);
            }

            if (addDefaultCompareFunctions)
            {
                this.AddCompareFunction((decimal lhs, byte rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, sbyte rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, short rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, ushort rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, int rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, uint rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, long rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, ulong rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, nint rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, nuint rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, float rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, double rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
                this.AddCompareFunction((decimal lhs, decimal rhs) => ((decimal)lhs).CompareTo((decimal)rhs));
            }
        }

        public static BasicCompareFunctions Create(bool addDefaultEqualFunctions = true, bool addDefaultCompareFunctions = true)
        {
            return new BasicCompareFunctions(
                addDefaultEqualFunctions: addDefaultEqualFunctions,
                addDefaultCompareFunctions: addDefaultCompareFunctions);
        }

        public bool EnumerablesEqual(IEnumerable? lhs, IEnumerable? rhs)
        {
            return Enumerables.SequenceEqual(lhs, rhs, this.AreEqual);
        }

        public bool TupleEqual(ITuple? lhs, ITuple? rhs)
        {
            int? lhsLength = lhs?.Length;
            int? rhsLength = rhs?.Length;
            bool result = (lhsLength == rhsLength);
            if (result && lhsLength != null)
            {
                for (int i = 0; i < lhsLength; i++)
                {
                    result = this.AreEqual(lhs![i], rhs![i]);
                    if (!result)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public bool ExceptionEqual(Exception? lhs, Exception? rhs)
        {
            bool result = (lhs == rhs);
            if (!result)
            {
                result = lhs?.GetType() == rhs?.GetType() &&
                         lhs?.Message == rhs?.Message &&
                         this.ExceptionEqual(lhs?.InnerException, rhs?.InnerException);
            }
            return result;
        }

        private Func<T?, U?, bool> GetEqualFunction<T, U>(T? lhs, U? rhs)
        {
            Func<object?, object?, bool>? result = null;
            
            Type lhsType = Types.GetType(lhs);
            IDictionary<Type, Func<object?, object?, bool>>? lhsTypeEqualFunctionMap = null;
            if (!this.equalFunctionMap.TryGetValue(lhsType, out lhsTypeEqualFunctionMap) || lhsTypeEqualFunctionMap == null)
            {
                foreach (KeyValuePair<Type, IDictionary<Type, Func<object?, object?, bool>>> entry in this.equalFunctionMap)
                {
                    if (lhsType.InstanceOf(entry.Key))
                    {
                        lhsTypeEqualFunctionMap = entry.Value;
                        break;
                    }
                }
            }

            if (lhsTypeEqualFunctionMap != null)
            {
                Type rhsType = Types.GetType(rhs);
                if (!lhsTypeEqualFunctionMap.TryGetValue(rhsType, out result) || result == null)
                {
                    foreach (KeyValuePair<Type, Func<object?, object?, bool>> entry in lhsTypeEqualFunctionMap)
                    {
                        if (rhsType.InstanceOf(entry.Key))
                        {
                            result = entry.Value;
                            break;
                        }
                    }
                }
            }

            if (result == null)
            {
                result = object.Equals;
            }

            return (T? equalLhs, U? equalRhs) => result.Invoke(equalLhs, equalRhs);
        }

        public override Disposable AddEqualFunction<T, U>(Func<T?, U?, bool> equalFunction)
            where T : default
            where U : default
        {
            if (equalFunction == null)
            {
                throw new ArgumentNullException(nameof(equalFunction));
            }

            Type lhsType = typeof(T);
            IDictionary<Type, Func<object?, object?, bool>>? lhsEqualFunctionMap = null;
            if (!this.equalFunctionMap.TryGetValue(lhsType, out lhsEqualFunctionMap) || lhsEqualFunctionMap == null)
            {
                lhsEqualFunctionMap = new Dictionary<Type, Func<object?, object?, bool>>();
                this.equalFunctionMap.Add(lhsType, lhsEqualFunctionMap);
            }

            Type rhsType = typeof(U);
            lhsEqualFunctionMap.Remove(rhsType);
            lhsEqualFunctionMap.Add(rhsType, (object? lhs, object? rhs) => equalFunction.Invoke((T?)lhs, (U?)rhs));

            return Disposable.Create(() =>
            {
                IDictionary<Type, Func<object?, object?, bool>>? lhsEqualFunctionMap = null;
                if (this.equalFunctionMap.TryGetValue(lhsType, out lhsEqualFunctionMap) && lhsEqualFunctionMap != null)
                {
                    lhsEqualFunctionMap.Remove(rhsType);
                    if (lhsEqualFunctionMap.Count == 0)
                    {
                        this.equalFunctionMap.Remove(lhsType);
                    }
                }
            });
        }

        public override bool AreEqual<T, U>(T? lhs, U? rhs)
            where T : default
            where U : default
        {
            bool result = object.ReferenceEquals(lhs, rhs);
            if (!result)
            {
                Func<T?, U?, bool> equalFunction = this.GetEqualFunction(lhs, rhs);
                result = equalFunction.Invoke(lhs, rhs);
            }
            return result;
        }

        private Func<T?, U?, Comparison> GetCompareFunction<T, U>(T? lhs, U? rhs)
        {
            Func<object?, object?, Comparison>? result = null;

            Type lhsType = Types.GetType(lhs);
            Type rhsType = Types.GetType(rhs);
            if (!this.compareFunctionMap.TryGetValue(Tuple.Create(lhsType, rhsType), out result) || result == null)
            {
                foreach (KeyValuePair<Tuple<Type, Type>, Func<object?, object?, Comparison>> entry in this.compareFunctionMap)
                {
                    bool lhsMatches = lhsType.InstanceOf(entry.Key.Item1);
                    if (lhsMatches)
                    {
                        bool rhsMatches = rhsType.InstanceOf(entry.Key.Item2);
                        if (rhsMatches)
                        {
                            result = entry.Value;
                            break;
                        }
                    }
                }

                if (this.addDefaultCompareFunctions)
                {
                    if (lhs == null)
                    {
                        if (rhs == null)
                        {
                            result = (object? compareLhs, object? compareRhs) => Comparison.Equal;
                        }
                        else
                        {
                            result = (object? compareLhs, object? compareRhs) => Comparison.LessThan;
                        }
                    }
                    else if (rhs == null)
                    {
                        result = (object? compareLhs, object? compareRhs) => Comparison.GreaterThan;
                    }
                    else if (lhs is IComparable<U> lhsComparable)
                    {
                        result = (object? objectLhs, object? objectRhs) => Comparisons.Create(((IComparable<U>)objectLhs!).CompareTo((U?)objectRhs));
                    }
                }

                if (result == null)
                {
                    throw new InvalidOperationException($"No compare function found that matches the types {Language.AndList(new[] { lhsType, rhsType }.Map(Types.GetFullName))}.");
                }
            }

            return (T? compareLhs, U? compareRhs) => result.Invoke(compareLhs, compareRhs);
        }

        public override Disposable AddCompareFunction<T, U>(Func<T?, U?, Comparison> compareFunction)
            where T : default
            where U : default
        {
            if (compareFunction == null)
            {
                throw new ArgumentNullException(nameof(compareFunction));
            }

            Tuple<Type, Type> compareFunctionKey = Tuple.Create(typeof(T), typeof(U));
            this.compareFunctionMap.Remove(compareFunctionKey, out Func<object?, object?, Comparison>? previousEqualFunction);
            this.compareFunctionMap.Add(compareFunctionKey, (object? lhs, object? rhs) => compareFunction.Invoke((T?)lhs, (U?)rhs));

            return Disposable.Create(() =>
            {
                this.compareFunctionMap.Remove(compareFunctionKey);
                if (previousEqualFunction != null)
                {
                    this.compareFunctionMap.Add(compareFunctionKey, previousEqualFunction);
                }
            });
        }

        public Disposable AddCompareFunction<T,U>(Func<T?,U?,int> compareFunction)
        {
            if (compareFunction == null)
            {
                throw new ArgumentNullException(nameof(compareFunction));
            }

            return this.AddCompareFunction((T? lhs, U? rhs) => Comparisons.Create(compareFunction.Invoke(lhs, rhs)));
        }

        public override Comparison Compare<T, U>(T? lhs, U? rhs)
            where T : default
            where U : default
        {
            Func<T?, U?, Comparison> compareFunction = this.GetCompareFunction(lhs, rhs);
            return compareFunction.Invoke(lhs, rhs);
        }
    }
}
