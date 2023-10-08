using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;

namespace Everyone
{
    public class BasicToStringFunctions : MutableToStringFunctions
    {
        private readonly IDictionary<Type, Func<object?, string>> toStringMap = new Dictionary<Type, Func<object?, string>>();

        private BasicToStringFunctions(bool addDefaultFunctions)
        {
            this.toStringMap = new Dictionary<Type, Func<object?, string>>();

            if (addDefaultFunctions)
            {
                this.AddToStringFunction<char?>(this.CharacterToString);
                this.AddToStringFunction<string>(this.StringToString);
                this.AddToStringFunction<IEnumerable>(this.EnumerableToString);
                this.AddToStringFunction<ITuple>(this.TupleToString);
                this.AddToStringFunction<Exception>(this.ExceptionToString);
            }
        }

        public static BasicToStringFunctions Create(bool addDefaultFunctions = true)
        {
            return new BasicToStringFunctions(addDefaultFunctions);
        }

        public string CharacterToString(char? value)
        {
            return value == null ? "null" : Characters.EscapeAndQuote(value)!;
        }

        public string StringToString(string? value)
        {
            return value == null ? "null" : Strings.EscapeAndQuote(value)!;
        }

        public string EnumerableToString(IEnumerable? values)
        {
            return Enumerables.ToString(values, this.ToString);
        }

        public string TupleToString(ITuple? tuple)
        {
            string result = "null";
            if (tuple != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append('(');
                for (int i = 0; i < tuple.Length; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(',');
                    }
                    builder.Append(this.ToString(tuple[i]));
                }
                builder.Append(')');
                result = builder.ToString();
            }
            return result;
        }

        public string ExceptionToString(Exception? exception)
        {
            string result;
            if (exception == null)
            {
                result = "null";
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                while (exception is AwaitException)
                {
                    builder.Append($"{Types.GetFullName(exception)}: ");
                    exception = exception.InnerException!;
                }
                builder.Append($"{Types.GetFullName(exception)}: {Strings.EscapeAndQuote(exception.Message)}");

                result = builder.ToString();
            }
            return result;
        }

        public Disposable AddToStringFunction<T>(Func<T?, string> toStringFunction)
        {
            if (toStringFunction == null)
            {
                throw new ArgumentNullException(nameof(toStringFunction));
            }

            this.toStringMap.Remove(typeof(T), out Func<object?, string>? existingToStringFunction);
            this.toStringMap.Add(typeof(T), (object? value) => toStringFunction.Invoke((T?)value));

            return Disposable.Create(() =>
            {
                this.toStringMap.Remove(typeof(T));
                if (existingToStringFunction != null)
                {
                    this.toStringMap.Add(typeof(T), existingToStringFunction);
                }
            });
        }

        public string ToString<T>(T? value)
        {
            Type valueType = Types.GetType(value);
            Func<object?, string>? toStringFunction = null;
            if (!this.toStringMap.TryGetValue(valueType, out toStringFunction) || toStringFunction == null)
            {
                foreach (KeyValuePair<Type, Func<object?, string>> entry in this.toStringMap)
                {
                    if (entry.Key.IsInstanceOfType(value))
                    {
                        toStringFunction = entry.Value;
                        break;
                    }
                }

                if (toStringFunction == null)
                {
                    toStringFunction = (object? o) => o?.ToString() ?? "null";
                }
            }
            return toStringFunction(value);
        }
    }
}
