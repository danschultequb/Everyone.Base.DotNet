using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace everyone
{
    /// <summary>
    /// A basic implementation of the <see cref="CompareFunctions"/> interface.
    /// </summary>
    public class BasicCompareFunctions : CompareFunctions
    {
        private readonly IDictionary<Tuple<Type, Type>, Func<object?, object?, bool>> equalFunctionMap;

        private BasicCompareFunctions(bool addDefaultFunctions)
        {
            this.equalFunctionMap = new Dictionary<Tuple<Type, Type>, Func<object?, object?, bool>>();

            if (addDefaultFunctions)
            {
                this.AddEqualFunction<IEnumerable, IEnumerable>(this.EnumerablesEqual);
                this.AddEqualFunction<ITuple, ITuple>(this.TupleEqual);
                this.AddEqualFunction<Exception, Exception>(this.ExceptionEqual);
            }
        }

        public static BasicCompareFunctions Create(bool addDefaultFunctions = true)
        {
            return new BasicCompareFunctions(addDefaultFunctions);
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
            return lhs?.GetType() == rhs?.GetType() &&
                   lhs?.Message == rhs?.Message;
        }

        private Func<T?, U?, bool> GetEqualFunction<T, U>(T? lhs, U? rhs)
        {
            Func<object?, object?, bool>? result = null;

            Type tType = lhs?.GetType() ?? typeof(T);
            Type uType = rhs?.GetType() ?? typeof(U);
            if (!this.equalFunctionMap.TryGetValue(Tuple.Create(tType, uType), out result) || result == null)
            {
                foreach (KeyValuePair<Tuple<Type, Type>, Func<object?, object?, bool>> entry in this.equalFunctionMap)
                {
                    bool lhsMatches = entry.Key.Item1.IsInstanceOfType(lhs);
                    bool rhsMatches = entry.Key.Item2.IsInstanceOfType(rhs);
                    if (lhsMatches && rhsMatches)
                    {
                        result = entry.Value;
                        break;
                    }
                }

                if (result == null)
                {
                    result = object.Equals;
                }
            }

            return (T? equalLhs, U? equalRhs) => result.Invoke(equalLhs, equalRhs);
        }

        public Disposable AddEqualFunction<T, U>(Func<T?, U?, bool> equalFunction)
        {
            if (equalFunction == null)
            {
                throw new ArgumentNullException(nameof(equalFunction));
            }

            Tuple<Type, Type> equalFunctionKey = Tuple.Create(typeof(T), typeof(U));
            this.equalFunctionMap.Remove(equalFunctionKey, out Func<object?, object?, bool>? previousEqualFunction);
            this.equalFunctionMap.Add(equalFunctionKey, (object? lhs, object? rhs) => equalFunction.Invoke((T?)lhs, (U?)rhs));

            return Disposable.Create(() =>
            {
                this.equalFunctionMap.Remove(equalFunctionKey);
                if (previousEqualFunction != null)
                {
                    this.equalFunctionMap.Add(equalFunctionKey, previousEqualFunction);
                }
            });
        }

        public bool AreEqual<T, U>(T? lhs, U? rhs)
        {
            bool result = object.ReferenceEquals(lhs, rhs);
            if (!result)
            {
                Func<T?, U?, bool> equalFunction = this.GetEqualFunction(lhs, rhs);
                result = equalFunction.Invoke(lhs, rhs);
            }
            return result;
        }
    }
}
