namespace everyone
{
    internal static class StringsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Strings), () =>
            {
                runner.TestGroup("Escape(this string?)", () =>
                {
                    void EscapeTest(string? caller, string? expected)
                    {
                        runner.Test($"with {Strings.EscapeAndQuote(caller)}", (Test test) =>
                        {
                            test.AssertEqual(expected, Strings.Escape(caller));
                            test.AssertEqual(expected, caller.Escape());
                        });
                    };

                    EscapeTest(null, null);
                    EscapeTest("", "");
                    EscapeTest("a", "a");
                    EscapeTest("\'", "\\'");
                    EscapeTest("\"", "\\\"");
                    EscapeTest("\\", "\\\\");
                    EscapeTest("\0", "\\0");
                    EscapeTest("\a", "\\a");
                    EscapeTest("\b", "\\b");
                    EscapeTest("\f", "\\f");
                    EscapeTest("\n", "\\n");
                    EscapeTest("\r", "\\r");
                    EscapeTest("\t", "\\t");
                    EscapeTest("\v", "\\v");
                    EscapeTest("\u0000", "\\0");
                    EscapeTest("\uFFFF", "\uFFFF");
                    EscapeTest("\U00000000", "\\0");
                    EscapeTest("\U0010FFFF", "\U0010FFFF");
                    EscapeTest("\x0", "\\0");
                    EscapeTest("\x00", "\\0");
                    EscapeTest("\xF", "\xF");
                });

                runner.TestGroup("Quote(this string?)", () =>
                {
                    void QuoteTest(string? value, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote());
                            test.AssertEqual(expected, Strings.Quote(value));
                        });
                    };

                    QuoteTest(null, null);
                    QuoteTest("", "\"\"");
                    QuoteTest("a", "\"a\"");
                    QuoteTest("\n", "\"\n\"");
                    QuoteTest("\"hello\"", "\"\"hello\"\"");
                });

                runner.TestGroup("Quote(this string?, char)", () =>
                {
                    void QuoteTest(string? value, char quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Strings.Quote(value, quote));
                        });
                    };

                    QuoteTest(null, '\'', null);
                    QuoteTest("", '\'', "\'\'");
                    QuoteTest("a", '\'', "\'a\'");
                    QuoteTest("\n", '\'', "\'\n\'");
                    QuoteTest("\"hello\"", '\'', "\'\"hello\"\'");
                    QuoteTest(null, '\"', null);
                    QuoteTest("", '\"', "\"\"");
                    QuoteTest("a", '\"', "\"a\"");
                    QuoteTest("\n", '\"', "\"\n\"");
                    QuoteTest("\"hello\"", '\"', "\"\"hello\"\"");
                    QuoteTest(null, 'a', null);
                    QuoteTest("", 'a', "aa");
                    QuoteTest("a", 'a', "aaa");
                    QuoteTest("\n", 'a', "a\na");
                    QuoteTest("\"hello\"", 'a', "a\"hello\"a");
                });

                runner.TestGroup("Quote(this string?, string)", () =>
                {
                    void QuoteTest(string? value, string quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Strings.Quote(value, quote));
                        });
                    };

                    QuoteTest(null, "\'", null);
                    QuoteTest("", "\'", "\'\'");
                    QuoteTest("a", "\'", "\'a\'");
                    QuoteTest("\n", "\'", "\'\n\'");
                    QuoteTest("\"hello\"", "\'", "\'\"hello\"\'");
                    QuoteTest(null, "\"", null);
                    QuoteTest("", "\"", "\"\"");
                    QuoteTest("a", "\"", "\"a\"");
                    QuoteTest("\n", "\"", "\"\n\"");
                    QuoteTest("\"hello\"", "\"", "\"\"hello\"\"");
                    QuoteTest(null, "a", null);
                    QuoteTest("", "a", "aa");
                    QuoteTest("a", "a", "aaa");
                    QuoteTest("\n", "a", "a\na");
                    QuoteTest("\"hello\"", "a", "a\"hello\"a");
                });

                runner.TestGroup("EscapeAndQuote(this string?)", () =>
                {
                    void EscapeAndQuoteTest(string? value, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote());
                            test.AssertEqual(expected, Strings.EscapeAndQuote(value));
                        });
                    };

                    EscapeAndQuoteTest(null, null);
                    EscapeAndQuoteTest("", "\"\"");
                    EscapeAndQuoteTest("a", "\"a\"");
                    EscapeAndQuoteTest("\n", "\"\\n\"");
                    EscapeAndQuoteTest("\"hello\"", "\"\\\"hello\\\"\"");
                });

                runner.TestGroup("EscapeAndQuote(this string?, char)", () =>
                {
                    void EscapeAndQuoteTest(string? value, char quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Strings.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest(null, '\'', null);
                    EscapeAndQuoteTest("", '\'', "\'\'");
                    EscapeAndQuoteTest("a", '\'', "\'a\'");
                    EscapeAndQuoteTest("\n", '\'', "\'\\n\'");
                    EscapeAndQuoteTest("\"hello\"", '\'', "\'\\\"hello\\\"\'");
                    EscapeAndQuoteTest(null, '\"', null);
                    EscapeAndQuoteTest("", '\"', "\"\"");
                    EscapeAndQuoteTest("a", '\"', "\"a\"");
                    EscapeAndQuoteTest("\n", '\"', "\"\\n\"");
                    EscapeAndQuoteTest("\"hello\"", '\"', "\"\\\"hello\\\"\"");
                    EscapeAndQuoteTest(null, 'a', null);
                    EscapeAndQuoteTest("", 'a', "aa");
                    EscapeAndQuoteTest("a", 'a', "aaa");
                    EscapeAndQuoteTest("\n", 'a', "a\\na");
                    EscapeAndQuoteTest("\"hello\"", 'a', "a\\\"hello\\\"a");
                });

                runner.TestGroup("EscapeAndQuote(this string?, string)", () =>
                {
                    void EscapeAndQuoteTest(string? value, string quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Strings.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest(null, "\'", null);
                    EscapeAndQuoteTest("", "\'", "\'\'");
                    EscapeAndQuoteTest("a", "\'", "\'a\'");
                    EscapeAndQuoteTest("\n", "\'", "\'\\n\'");
                    EscapeAndQuoteTest("\"hello\"", "\'", "\'\\\"hello\\\"\'");
                    EscapeAndQuoteTest(null, "\"", null);
                    EscapeAndQuoteTest("", "\"", "\"\"");
                    EscapeAndQuoteTest("a", "\"", "\"a\"");
                    EscapeAndQuoteTest("\n", "\"", "\"\\n\"");
                    EscapeAndQuoteTest("\"hello\"", "\"", "\"\\\"hello\\\"\"");
                    EscapeAndQuoteTest(null, "a", null);
                    EscapeAndQuoteTest("", "a", "aa");
                    EscapeAndQuoteTest("a", "a", "aaa");
                    EscapeAndQuoteTest("\n", "a", "a\\na");
                    EscapeAndQuoteTest("\"hello\"", "a", "a\\\"hello\\\"a");
                });
            });
        }
    }
}
