using System;
using System.Collections.Generic;

namespace everyone
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

        public string ExpectedTrue(bool value, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: {this.ToString(true)}");
            resultList.Add($"Actual:   {this.ToString(value)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedFalse(bool value, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: {this.ToString(false)}");
            resultList.Add($"Actual:   {this.ToString(value)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedNull<T>(T actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: {this.ToString<object?>(null)}");
            resultList.Add($"Actual:   {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedNotNull<T>(T actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: not {this.ToString<object?>(null)}");
            resultList.Add($"Actual:   {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedSame<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: same as {this.ToString(expected)}");
            resultList.Add($"Actual:           {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedNotSame<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: not same as {this.ToString(expected)}");
            resultList.Add($"Actual:               {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedEqual<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: {this.ToString(expected)}");
            resultList.Add($"Actual:   {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedNotEqual<T, U>(T? expected, U? actual, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: not {this.ToString(expected)}");
            resultList.Add($"Actual:       {this.ToString(actual)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }

        public string ExpectedNotNullAndNotEmpty(string? value, string? message = null, string? newLine = null, string? expression = null)
        {
            List<string> resultList = new List<string>();

            AddMessage(resultList, message);
            AddExpression(resultList, expression);

            resultList.Add($"Expected: not null and not empty");
            resultList.Add($"Actual:   {this.ToString(value)}");

            return string.Join(newLine ?? Environment.NewLine, resultList);
        }
    }
}
