using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Everyone
{
    /// <summary>
    /// A basic implementation of the <see cref="CompareFunctions"/> interface.
    /// </summary>
    public class BasicCompareFunctions : CompareFunctions
    {
        private readonly IDictionary<Tuple<Type, Type>, Func<object?, object?, bool>> equalFunctionMap;

        private readonly IDictionary<Tuple<Type, Type>, Func<object?, object?, Comparison>> compareFunctionMap;
        private readonly bool addDefaultCompareFunctions;

        private BasicCompareFunctions(bool addDefaultEqualFunctions, bool addDefaultCompareFunctions)
        {
            this.equalFunctionMap = new Dictionary<Tuple<Type, Type>, Func<object?, object?, bool>>();
            if (addDefaultEqualFunctions)
            {
                this.AddEqualFunction<IEnumerable, IEnumerable>(this.EnumerablesEqual);
                this.AddEqualFunction<ITuple, ITuple>(this.TupleEqual);
                this.AddEqualFunction<Exception, Exception>(this.ExceptionEqual);
            }

            this.compareFunctionMap = new Dictionary<Tuple<Type, Type>, Func<object?, object?, Comparison>>();
            this.addDefaultCompareFunctions = addDefaultCompareFunctions;
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
            return lhs?.GetType() == rhs?.GetType() &&
                   lhs?.Message == rhs?.Message;
        }

        private Func<T?, U?, bool> GetEqualFunction<T, U>(T? lhs, U? rhs)
        {
            Func<object?, object?, bool>? result = null;

            Type lhsType = Types.GetType(lhs);
            Type rhsType = Types.GetType(rhs);
            if (!this.equalFunctionMap.TryGetValue(Tuple.Create(lhsType, rhsType), out result) || result == null)
            {
                foreach (KeyValuePair<Tuple<Type, Type>, Func<object?, object?, bool>> entry in this.equalFunctionMap)
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

        private Func<T?, U?, Comparison> GetCompareFunction<T, U>(T? lhs, U? rhs)
        {
            Func<object?, object?, Comparison>? result = null;

            Type tType = lhs?.GetType() ?? typeof(T);
            Type uType = rhs?.GetType() ?? typeof(U);
            if (!this.compareFunctionMap.TryGetValue(Tuple.Create(tType, uType), out result) || result == null)
            {
                foreach (KeyValuePair<Tuple<Type, Type>, Func<object?, object?, Comparison>> entry in this.compareFunctionMap)
                {
                    bool lhsMatches = entry.Key.Item1.InstanceOf(tType);
                    bool rhsMatches = entry.Key.Item2.InstanceOf(uType);
                    if (lhsMatches && rhsMatches)
                    {
                        result = entry.Value;
                        break;
                    }
                }

                if (this.addDefaultCompareFunctions && lhs is IComparable<U> lhsComparable)
                {
                    result = (object? objectLhs, object? objectRhs) => Comparisons.Create(((IComparable<U>)objectLhs!).CompareTo((U?)objectRhs));
                }

                if (result == null)
                {
                    throw new InvalidOperationException($"No compare function found that matches the types {Language.AndList(new[] { tType, uType }.Map(Types.GetFullName))}");
                }
            }

            return (T? compareLhs, U? compareRhs) => result.Invoke(compareLhs, compareRhs);
        }

        public Disposable AddCompareFunction<T, U>(Func<T?, U?, Comparison> compareFunction)
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

        public Comparison Compare<T, U>(T? lhs, U? rhs)
        {
            Func<T?, U?, Comparison> compareFunction = this.GetCompareFunction(lhs, rhs);
            return compareFunction.Invoke(lhs, rhs);
        }
    }
}
