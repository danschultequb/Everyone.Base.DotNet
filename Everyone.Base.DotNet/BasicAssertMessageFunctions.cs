using System;
using System.Collections;
using System.Collections.Generic;

namespace Everyone
{
    public class BasicAssertMessageFunctions : AssertMessageFunctions
    {
        private BasicAssertMessageFunctions(ToStringFunctions? toStringFunctions = null)
        {
            this.ToStringFunctions = toStringFunctions ?? ToStringFunctions.Create();
        }

        public static BasicAssertMessageFunctions Create(ToStringFunctions? toStringFunctions = null)
        {
            return new BasicAssertMessageFunctions(toStringFunctions);
        }

        public ToStringFunctions ToStringFunctions { get; }

        private string ToString<T>(T? value)
        {
            return this.ToStringFunctions.ToString(value);
        }

        private static void AddMessage(List<string> list, string? message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                list.Add($"Message: {message}");
            }
        }

        private static void AddExpression(List<string> list, string? expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                list.Add($"Expression: {expression}");
            }
        }

        private string Expected(string expected, string actual, AssertParameters? parameters)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, parameters?.Message);
            AddExpression(resultList, parameters?.Expression);

            resultList.Add($"Expected: {expected}");
            resultList.Add($"Actual:   {actual}");

            return string.Join(parameters?.NewLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedTrue(bool? value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: this.ToString(true),
                actual:   this.ToString(value),
                parameters: parameters);
        }

        public string ExpectedFalse(bool? value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: this.ToString(false),
                actual:   this.ToString(value),
                parameters: parameters);
        }

        public string ExpectedNull<T>(T value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: this.ToString<object?>(null),
                actual:   this.ToString(value),
                parameters: parameters);
        }

        public string ExpectedNotNull<T>(T value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"not {this.ToString<object?>(null)}",
                actual:   $"{this.ToString(value)}",
                parameters: parameters);
        }

        public string ExpectedSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"same as {this.ToString(expected)}",
                actual:   $"{this.ToString(actual)}",
                parameters: parameters);
        }

        public string ExpectedNotSame<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"not same as {this.ToString(expected)}",
                actual:   $"{this.ToString(actual)}",
                parameters: parameters);
        }

        public string ExpectedEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            return this.Expected(
                expected: this.ToString(expected),
                actual:   this.ToString(actual),
                parameters: parameters);
        }

        public string ExpectedNotEqual<T, U>(T? expected, U? actual, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"not {this.ToString(expected)}",
                actual:   $"{this.ToString(actual)}",
                parameters: parameters);
        }

        public string ExpectedNotNullAndNotEmpty(string? value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"not null and not empty",
                actual:   $"{this.ToString(value)}",
                parameters: parameters);
        }

        public string ExpectedNotNullAndNotEmpty(IEnumerable? value, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"not null and not empty",
                actual: $"{this.ToString(value)}",
                parameters: parameters);
        }

        public string ExpectedGreaterThan<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"greater than {this.ToString(lowerBound)}",
                actual:   $"{this.ToString(value)}",
                parameters: parameters);
        }

        public string ExpectedGreaterThanOrEqualTo<T, U>(T? value, U? lowerBound, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"greater than or equal to {this.ToString(lowerBound)}",
                actual:   $"{this.ToString(value)}",
                parameters: parameters);
        }

        public string ExpectedBetween<T, U, V>(T? lowerBound, U? value, V? upperBound, AssertParameters? parameters)
        {
            return this.Expected(
                expected: $"between {Language.AndList(this.ToString(lowerBound), this.ToString(upperBound))}",
                actual:   $"{this.ToString(value)}",
                parameters: parameters);
        }
    }
}
