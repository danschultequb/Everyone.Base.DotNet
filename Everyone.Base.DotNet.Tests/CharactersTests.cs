using System.Collections.Generic;

namespace Everyone
{
    public static class CharactersTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType(typeof(Characters), () =>
            {
                runner.TestMethod("Escape(this char?,IEnumerable<char>?)", () =>
                {
                    void EscapeTest(char? caller, IEnumerable<char>? dontEscape, string? expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { caller, dontEscape }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertEqual(expected, Characters.Escape(caller, dontEscape));
                            test.AssertEqual(expected, caller.Escape(dontEscape));
                        });
                    }

                    EscapeTest(null, null, null);
                    EscapeTest('a', null, "a");
                    EscapeTest('\'', null, "\\'");
                    EscapeTest('\"', null, "\\\"");
                    EscapeTest('\\', null, "\\\\");
                    EscapeTest('\0', null, "\\0");
                    EscapeTest('\a', null, "\\a");
                    EscapeTest('\b', null, "\\b");
                    EscapeTest('\f', null, "\\f");
                    EscapeTest('\n', null, "\\n");
                    EscapeTest('\r', null, "\\r");
                    EscapeTest('\t', null, "\\t");
                    EscapeTest('\v', null, "\\v");
                    EscapeTest('\u0000', null, "\\0");
                    EscapeTest('\uFFFF', null, "\uFFFF");
                    EscapeTest('\U00000000', null, "\\0");
                    EscapeTest('\x0', null, "\\0");
                    EscapeTest('\x00', null, "\\0");
                    EscapeTest('\xF', null, "\xF");

                    EscapeTest('\'', new[] { '\'' }, "'");
                    EscapeTest('\'', new[] { '\"' }, "\\'");
                });

                runner.TestMethod("Escape(this char)", () =>
                {
                    void EscapeTest(char caller, string expected)
                    {
                        runner.Test($"with {Characters.EscapeAndQuote(caller)}", (Test test) =>
                        {
                            test.AssertEqual(expected, Characters.Escape(caller));
                            test.AssertEqual(expected, caller.Escape());
                        });
                    };

                    EscapeTest('a', "a");
                    EscapeTest('\'', "\\'");
                    EscapeTest('\"', "\\\"");
                    EscapeTest('\\', "\\\\");
                    EscapeTest('\0', "\\0");
                    EscapeTest('\a', "\\a");
                    EscapeTest('\b', "\\b");
                    EscapeTest('\f', "\\f");
                    EscapeTest('\n', "\\n");
                    EscapeTest('\r', "\\r");
                    EscapeTest('\t', "\\t");
                    EscapeTest('\v', "\\v");
                    EscapeTest('\u0000', "\\0");
                    EscapeTest('\uFFFF', "\uFFFF");
                    EscapeTest('\U00000000', "\\0");
                    EscapeTest('\x0', "\\0");
                    EscapeTest('\x00', "\\0");
                    EscapeTest('\xF', "\xF");
                });

                runner.TestMethod("Quote(this char?)", () =>
                {
                    void QuoteTest(char? value, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote());
                            test.AssertEqual(expected, Characters.Quote(value));
                        });
                    };

                    QuoteTest(null, null);
                    QuoteTest('a', "'a'");
                    QuoteTest('\n', "'\n'");
                });

                runner.TestMethod("Quote(this char)", () =>
                {
                    void QuoteTest(char value, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote());
                            test.AssertEqual(expected, Characters.Quote(value));
                        });
                    };

                    QuoteTest('a', "'a'");
                    QuoteTest('\n', "'\n'");
                });

                runner.TestMethod("Quote(this char?, char)", () =>
                {
                    void QuoteTest(char? value, char quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and default quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Characters.Quote(value, quote));
                        });
                    };

                    QuoteTest(null, '\'', null);
                    QuoteTest('a', '\'', "\'a\'"); ;
                    QuoteTest('\n', '\'', "\'\n\'");
                    QuoteTest(null, '\"', null);
                    QuoteTest('a', '\"', "\"a\"");
                    QuoteTest('\n', '\"', "\"\n\"");
                    QuoteTest(null, 'a', null);
                    QuoteTest('a', 'a', "aaa");
                    QuoteTest('\n', 'a', "a\na");
                });

                runner.TestMethod("Quote(this char, char)", () =>
                {
                    void QuoteTest(char value, char quote, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and default quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Characters.Quote(value, quote));
                        });
                    };

                    QuoteTest('a', '\'', "\'a\'"); ;
                    QuoteTest('\n', '\'', "\'\n\'");
                    QuoteTest('a', '\"', "\"a\"");
                    QuoteTest('\n', '\"', "\"\n\"");
                    QuoteTest('a', 'a', "aaa");
                    QuoteTest('\n', 'a', "a\na");
                });

                runner.TestMethod("Quote(this char?, string)", () =>
                {
                    void QuoteTest(char? value, string quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Characters.Quote(value, quote));
                        });
                    };

                    QuoteTest(null, "'", null);
                    QuoteTest('a', "'", "'a'"); ;
                    QuoteTest('\n', "'", "'\n'");
                    QuoteTest(null, "\"", null);
                    QuoteTest('a', "\"", "\"a\"");
                    QuoteTest('\n', "\"", "\"\n\"");
                    QuoteTest(null, "a", null);
                    QuoteTest('a', "a", "aaa");
                    QuoteTest('\n', "a", "a\na");
                    QuoteTest(null, "abc", null);
                    QuoteTest('a', "abc", "abcaabc");
                    QuoteTest('\n', "abc", "abc\nabc");
                });

                runner.TestMethod("Quote(this char, string)", () =>
                {
                    void QuoteTest(char value, string quote, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.Quote(quote));
                            test.AssertEqual(expected, Characters.Quote(value, quote));
                        });
                    };

                    QuoteTest('a', "\'", "'a'"); ;
                    QuoteTest('\n', "\'", "'\n'");
                    QuoteTest('a', "\"", "\"a\"");
                    QuoteTest('\n', "\"", "\"\n\"");
                    QuoteTest('a', "a", "aaa");
                    QuoteTest('\n', "a", "a\na");
                    QuoteTest('a', "abc", "abcaabc");
                    QuoteTest('\n', "abc", "abc\nabc");
                });

                runner.TestMethod("EscapeAndQuote(this char?)", () =>
                {
                    void EscapeAndQuoteTest(char? value, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote());
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value));
                        });
                    };

                    EscapeAndQuoteTest(null, null);
                    EscapeAndQuoteTest('a', "'a'");
                    EscapeAndQuoteTest('\n', "'\\n'");
                });

                runner.TestMethod("EscapeAndQuote(this char)", () =>
                {
                    void EscapeAndQuoteTest(char value, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote());
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value));
                        });
                    };

                    EscapeAndQuoteTest('a', "'a'");
                    EscapeAndQuoteTest('\n', "'\\n'");
                });

                runner.TestMethod("EscapeAndQuote(this char?, char)", () =>
                {
                    void EscapeAndQuoteTest(char? value, char quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest(null, '\'', null);
                    EscapeAndQuoteTest('a', '\'', "'a'");
                    EscapeAndQuoteTest('\n', '\'', "'\\n'");
                    EscapeAndQuoteTest(null, '\"', null);
                    EscapeAndQuoteTest('a', '\"', "\"a\"");
                    EscapeAndQuoteTest('\n', '\"', "\"\\n\"");
                    EscapeAndQuoteTest(null, 'b', null);
                    EscapeAndQuoteTest('a', 'b', "bab");
                    EscapeAndQuoteTest('\n', 'b', "b\\nb");
                });

                runner.TestMethod("EscapeAndQuote(this char, char)", () =>
                {
                    void EscapeAndQuoteTest(char value, char quote, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest('a', '\'', "'a'");
                    EscapeAndQuoteTest('\n', '\'', "'\\n'");
                    EscapeAndQuoteTest('a', '\"', "\"a\"");
                    EscapeAndQuoteTest('\n', '\"', "\"\\n\"");
                    EscapeAndQuoteTest('a', 'b', "bab");
                    EscapeAndQuoteTest('\n', 'b', "b\\nb");
                });

                runner.TestMethod("EscapeAndQuote(this char?, string)", () =>
                {
                    void EscapeAndQuoteTest(char? value, string quote, string? expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest(null, "\'", null);
                    EscapeAndQuoteTest('a', "\'", "'a'");
                    EscapeAndQuoteTest('\n', "\'", "'\\n'");
                    EscapeAndQuoteTest(null, "\"", null);
                    EscapeAndQuoteTest('a', "\"", "\"a\"");
                    EscapeAndQuoteTest('\n', "\"", "\"\\n\"");
                    EscapeAndQuoteTest(null, "b", null);
                    EscapeAndQuoteTest('a', "b", "bab");
                    EscapeAndQuoteTest('\n', "b", "b\\nb");
                    EscapeAndQuoteTest(null, "cd", null);
                    EscapeAndQuoteTest('a', "cd", "cdacd");
                    EscapeAndQuoteTest('\n', "cd", "cd\\ncd");
                });

                runner.TestMethod("EscapeAndQuote(this char, string)", () =>
                {
                    void EscapeAndQuoteTest(char value, string quote, string expected)
                    {
                        runner.Test($"with {value.EscapeAndQuote()} and {quote.EscapeAndQuote()} quote", (Test test) =>
                        {
                            test.AssertEqual(expected, value.EscapeAndQuote(quote));
                            test.AssertEqual(expected, Characters.EscapeAndQuote(value, quote));
                        });
                    };

                    EscapeAndQuoteTest('a', "\'", "'a'");
                    EscapeAndQuoteTest('\n', "\'", "'\\n'");
                    EscapeAndQuoteTest('a', "\"", "\"a\"");
                    EscapeAndQuoteTest('\n', "\"", "\"\\n\"");
                    EscapeAndQuoteTest('a', "b", "bab");
                    EscapeAndQuoteTest('\n', "b", "b\\nb");
                    EscapeAndQuoteTest('a', "cd", "cdacd");
                    EscapeAndQuoteTest('\n', "cd", "cd\\ncd");
                });
            });
        }
    }
}
