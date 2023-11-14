using System;

namespace Everyone
{
    public static class CharacterWriteStreamTests
    {
        public static void Test(TestRunner runner, Func<CharacterWriteStream> creator)
        {
            runner.TestType<CharacterWriteStream>(() =>
            {
                runner.TestMethod("WriteCharacters(char[])", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        using (CharacterWriteStream stream = creator.Invoke())
                        {
                            test.AssertThrows(() => stream.WriteCharacters((char[])null!),
                                new PreConditionFailure(
                                    "Expression: characters",
                                    "Expected: not null and not empty",
                                    "Actual:   null"));
                        }
                    });

                    runner.Test("with []", (Test test) =>
                    {
                        using (CharacterWriteStream stream = creator.Invoke())
                        {
                            test.AssertThrows(() => stream.WriteCharacters(new char[0]),
                                new PreConditionFailure(
                                    "Expression: characters",
                                    "Expected: not null and not empty",
                                    "Actual:   []"));
                        }
                    });
                });

                runner.TestMethod("WriteCharacters(char[],int,int)", () =>
                {
                    void WriteCharactersTest(char[] characters, int startIndex, int length, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object[] { characters, startIndex, length }.Map(runner.ToString))}", (Test test) =>
                        {
                            using (CharacterWriteStream stream = creator.Invoke())
                            {
                                test.AssertThrows(expected, () =>
                                {
                                    stream.WriteCharacters(characters, startIndex, length).Await();
                                });
                            }
                        });
                    }

                    WriteCharactersTest(
                        characters: new char[0],
                        startIndex: 0,
                        length: 0,
                        expected: new PreConditionFailure(
                            "Expression: characters",
                            "Expected: not null and not empty",
                            "Actual:   []"));
                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: -1,
                        length: 0,
                        expected: new PreConditionFailure(
                            "Expression: startIndex",
                            "Expected: 0",
                            "Actual:   -1"));
                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: 1,
                        length: 0,
                        expected: new PreConditionFailure(
                            "Expression: startIndex",
                            "Expected: 0",
                            "Actual:   1"));
                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: 0,
                        length: -1,
                        expected: new PreConditionFailure(
                            "Expression: length",
                            "Expected: between 0 and 1",
                            "Actual:   -1"));
                    WriteCharactersTest(
                        characters: new[] { 'a' },
                        startIndex: 0,
                        length: 2,
                        expected: new PreConditionFailure(
                            "Expression: length",
                            "Expected: between 0 and 1",
                            "Actual:   2"));
                });
            });
        }
    }
}
