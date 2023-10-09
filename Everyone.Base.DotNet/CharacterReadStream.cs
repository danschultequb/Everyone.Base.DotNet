using System;

namespace Everyone
{
    /// <summary>
    /// An interface for reading characters from a stream.
    /// </summary>
    public interface CharacterReadStream : Disposable
    {
        /// <summary>
        /// Read the next <see cref="char"/>.
        /// </summary>
        public Result<char> ReadCharacter();

        /// <summary>
        /// Read (up to) the next <paramref name="maximumToRead"/> characters into the
        /// <paramref name="charactersRead"/> <see cref="char"/>[].
        /// </summary>
        /// <param name="charactersRead">The <see cref="char"/>[] that the read characters will be
        /// assigned to.</param>
        /// <param name="charactersReadStartIndex">The index in <paramref name="charactersRead"/>
        /// to start assigning read characters to.</param>
        /// <param name="maximumToRead">The maximum number of characters that should be read.</param>
        /// <returns>The number of characters that were read.</returns>
        public Result<int> ReadCharacters(char[] charactersRead, int charactersReadStartIndex, int maximumToRead);
    }

    public abstract class CharacterReadStreamDecorator : CharacterReadStream
    {
        private readonly CharacterReadStream innerStream;

        protected CharacterReadStreamDecorator(CharacterReadStream innerStream)
        {
            Pre.Condition.AssertNotNull(innerStream, nameof(innerStream));

            this.innerStream = innerStream;
        }

        public bool IsDisposed()
        {
            return this.innerStream.IsDisposed();
        }

        public Result<bool> Dispose()
        {
            return this.innerStream.Dispose();
        }

        public Result<char> ReadCharacter()
        {
            return this.innerStream.ReadCharacter();
        }

        public Result<int> ReadCharacters(char[] charactersRead, int charactersReadStartIndex, int maximumToRead)
        {
            return this.innerStream.ReadCharacters(charactersRead, charactersReadStartIndex, maximumToRead);
        }
    }

    /// <summary>
    /// A collection of extension methods for <see cref="CharacterReadStream"/>s.
    /// </summary>
    public static class CharacterReadStreams
    {
        /// <summary>
        /// Read (up to) the next <paramref name="maximumToRead"/> characters.
        /// </summary>
        /// <param name="maximumToRead">The maximum number of characters that will be read.</param>
        /// <returns>The characters that were read.</returns>
        public static Result<char[]> ReadCharacters(this CharacterReadStream readStream, int maximumToRead)
        {
            Pre.Condition.AssertNotNull(readStream, nameof(readStream));
            Pre.Condition.AssertNotDisposed(readStream, nameof(readStream));
            Pre.Condition.AssertGreaterThanOrEqualTo(maximumToRead, 0, nameof(maximumToRead));

            return Result.Create(() =>
            {
                char[] result = new char[maximumToRead];
                if (maximumToRead > 0)
                {
                    int charactersRead = readStream.ReadCharacters(result, 0, maximumToRead).Await();
                    Array.Resize(ref result, charactersRead);
                }
                return result;
            });
        }

        public static Result<int> ReadCharacters(this CharacterReadStream readStream, char[] charactersRead)
        {
            Pre.Condition.AssertNotNull(readStream, nameof(readStream));
            Pre.Condition.AssertNotDisposed(readStream, nameof(readStream));
            Pre.Condition.AssertNotNullAndNotEmpty(charactersRead, nameof(charactersRead));

            return readStream.ReadCharacters(charactersRead, 0, charactersRead.Length);
        }
    }
}
