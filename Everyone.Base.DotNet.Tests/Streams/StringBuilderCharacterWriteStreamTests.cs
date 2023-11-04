namespace Everyone
{
    public static class StringBuilderCharacterWriteStreamTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<StringBuilderCharacterWriteStream>(() =>
            {
                CharacterWriteStreamTests.Test(runner, StringBuilderCharacterWriteStream.Create);

                runner.TestMethod("Create()", (Test test) =>
                {
                    using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                    {
                        test.AssertNotNull(stream);
                        test.AssertNotDisposed(stream);
                        test.AssertEqual("", stream.ToString());
                    }
                });

                runner.TestMethod("WriteCharacter(char)", () =>
                {
                    runner.Test("with not disposed stream", (Test test) =>
                    {
                        using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                        {
                            Result<int> writeCharacterResult = stream.WriteCharacter('a');
                            test.AssertNotNull(writeCharacterResult);
                            test.AssertEqual("", stream.ToString());

                            for (int i = 0; i < 3; i++)
                            {
                                test.AssertEqual(1, writeCharacterResult.Await());
                                test.AssertEqual("a", stream.ToString());
                            }

                            test.AssertEqual(1, stream.WriteCharacter('b').Await());
                            test.AssertEqual("ab", stream.ToString());
                        }
                    });

                    runner.Test("with stream disposed before write", (Test test) =>
                    {
                        using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                        {
                            test.AssertTrue(stream.Dispose().Await());

                            test.AssertThrows(() => stream.WriteCharacter('a'),
                                new PreConditionFailure(
                                    "Expression: this",
                                    "Expected: not disposed",
                                    "Actual:   disposed"));
                            test.AssertEqual("", stream.ToString());
                        }
                    });
                });

                runner.TestMethod("WriteCharacters(char[])", () =>
                {
                    void WriteCharactersTest(char[] characters)
                    {
                        runner.Test($"with {runner.ToString(characters)}", (Test test) =>
                        {
                            using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                            {
                                Result<int> writeCharactersResult = stream.WriteCharacters(characters);
                                test.AssertNotNull(writeCharactersResult);
                                test.AssertEqual("", stream.ToString());

                                for (int i = 0; i < 3; i++)
                                {
                                    test.AssertEqual(characters.Length, writeCharactersResult.Await());
                                    test.AssertEqual(new string(characters), stream.ToString());
                                }
                            }
                        });
                    }

                    WriteCharactersTest(new[] { 'a' });
                    WriteCharactersTest(new[] { 'a', 'b' });
                    WriteCharactersTest(new[] { 'a', 'b', 'c' });
                });

                runner.TestMethod("WriteCharacters(char[],int,int)", () =>
                {
                    void WriteCharactersTest(char[] characters, int startIndex, int length)
                    {
                        runner.Test($"with {Language.AndList(new object[] { characters, startIndex, length }.Map(runner.ToString))}", (Test test) =>
                        {
                            using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                            {
                                Result<int> writeCharactersResult = stream.WriteCharacters(characters, startIndex, length);
                                test.AssertNotNull(writeCharactersResult);
                                test.AssertEqual("", stream.ToString());

                                for (int i = 0; i < 3; i++)
                                {
                                    test.AssertEqual(length, writeCharactersResult.Await());
                                    test.AssertEqual(new string(characters, startIndex, length), stream.ToString());
                                }
                            }
                        });
                    }

                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: 0,
                        length: 0);
                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: 0,
                        length: 1);
                    WriteCharactersTest(
                        characters: new[] { 'a', 'b', 'c' },
                        startIndex: 1,
                        length: 2);
                });

                runner.TestMethod("Clear()", (Test test) =>
                {
                    using (StringBuilderCharacterWriteStream stream = StringBuilderCharacterWriteStream.Create())
                    {
                        StringBuilderCharacterWriteStream clearResult = stream.Clear();
                        test.AssertSame(stream, clearResult);
                        test.AssertEqual("", stream.ToString());

                        stream.WriteCharacter('a').Await();
                        test.AssertEqual("a", stream.ToString());

                        stream.Clear();
                        test.AssertEqual("", stream.ToString());
                    }
                });
            });
        }
    }
}
