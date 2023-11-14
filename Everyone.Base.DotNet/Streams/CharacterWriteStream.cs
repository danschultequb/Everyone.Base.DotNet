using System.Collections.Generic;

namespace Everyone
{
    /// <summary>
    /// An interface for writing characters to a stream.
    /// </summary>
    public interface CharacterWriteStream : Disposable
    {
        /// <summary>
        /// Write the provided <see cref="char"/>.
        /// </summary>
        /// <param name="character">The <see cref="char"/> to write.</param>
        /// <returns>The number of characters that were written.</returns>
        public Result<int> WriteCharacter(char character);

        /// <summary>
        /// Write the provided <see cref="char"/>s.
        /// </summary>
        /// <param name="characters">The <see cref="char"/>s to write.</param>
        /// <param name="startIndex">The index in the <paramref name="characters"/> to start
        /// writing from.</param>
        /// <param name="length">The number of <see cref="char"/>s to write.</param>
        /// <returns>The number of <see cref="char"/>s that were written.</returns>
        public Result<int> WriteCharacters(char[] characters, int startIndex, int length);
    }

    public abstract class CharacterWriteStreamDecorator : CharacterWriteStream
    {
        private readonly CharacterWriteStream innerStream;

        protected CharacterWriteStreamDecorator(CharacterWriteStream innerStream)
        {
            Pre.Condition.AssertNotNull(innerStream, nameof(innerStream));

            this.innerStream = innerStream;
        }

        public DisposableState GetDisposableState()
        {
            return this.innerStream.GetDisposableState();
        }

        public Result<bool> Dispose()
        {
            return this.innerStream.Dispose();
        }

        public Result<int> WriteCharacter(char character)
        {
            return this.innerStream.WriteCharacter(character);
        }

        public Result<int> WriteCharacters(char[] characters, int startIndex, int length)
        {
            return this.innerStream.WriteCharacters(characters, startIndex, length);
        }
    }

    /// <summary>
    /// A collection of extension methods for <see cref="CharacterWriteStream"/>s.
    /// </summary>
    public static class CharacterWriteStreams
    {
        /// <summary>
        /// Write the provided <see cref="char"/>s.
        /// </summary>
        /// <param name="stream">The <see cref="CharacterWriteStream"/> to write the
        /// <see cref="char"/>s to.</param>
        /// <param name="characters">The <see cref="char"/>s to write.</param>
        /// <returns>The number of <see cref="char"/>s that were written.</returns>
        public static Result<int> WriteCharacters(this CharacterWriteStream stream, char[] characters)
        {
            Pre.Condition.AssertNotNull(stream, nameof(stream));
            Pre.Condition.AssertNotNullAndNotEmpty(characters, nameof(characters));

            return stream.WriteCharacters(characters, 0, characters.Length);
        }

        /// <summary>
        /// Write the provided <see cref="char"/>s.
        /// </summary>
        /// <param name="stream">The <see cref="CharacterWriteStream"/> to write the
        /// <see cref="char"/>s to.</param>
        /// <param name="characters">The <see cref="char"/>s to write.</param>
        /// <returns>The number of <see cref="char"/>s that were written.</returns>
        public static Result<int> WriteCharacters(this CharacterWriteStream stream, string characters)
        {
            Pre.Condition.AssertNotNull(stream, nameof(stream));
            Pre.Condition.AssertNotNullAndNotEmpty(characters, nameof(characters));

            return stream.WriteCharacters(characters.ToCharArray());
        }

        /// <summary>
        /// Write the provided <paramref name="characters"/>.
        /// </summary>
        /// <param name="stream">The <see cref="CharacterWriteStream"/> to write to.</param>
        /// <param name="characters">The <see cref="char"/>s to write.</param>
        /// <returns>The number of characters that were written.</returns>
        public static Result<int> WriteCharacters(this CharacterWriteStream stream, IEnumerable<char> characters)
        {
            Pre.Condition.AssertNotNull(stream, nameof(stream));
            Pre.Condition.AssertNotNull(characters, nameof(characters));

            return Result.Create(() =>
            {
                int result = 0;
                foreach (char c in characters)
                {
                    result += stream.WriteCharacter(c).Await();
                }
                return result;
            });
        }

        public static Result<long> WriteCharacters(this CharacterWriteStream writeStream, CharacterReadStream readStream)
        {
            Pre.Condition.AssertNotNull(writeStream, nameof(writeStream));
            Pre.Condition.AssertNotDisposed(writeStream, nameof(writeStream));
            Pre.Condition.AssertNotNull(readStream, nameof(readStream));
            Pre.Condition.AssertNotDisposed(readStream, nameof(readStream));

            return Result.Create(() =>
            {
                long result = 0;

                char[] buffer = new char[1024];
                int charactersInBuffer = 0;

                int charactersRead = readStream.ReadCharacters(buffer, charactersInBuffer, buffer.Length - charactersInBuffer).Await();
                charactersInBuffer += charactersRead;
                bool readFilledBuffer = (charactersInBuffer == buffer.Length);

                int charactersWritten = writeStream.WriteCharacters(buffer, 0, charactersInBuffer).Await();
                charactersInBuffer -= charactersWritten;
                bool writeEmptiedBuffer = (charactersInBuffer == 0);

                if (charactersWritten == buffer.Length)
                {
                    buffer = new char[buffer.Length * 2];
                }

                return result;
            });
        }
    }
}
