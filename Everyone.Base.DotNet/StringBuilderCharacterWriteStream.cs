using System.Text;

namespace Everyone
{
    /// <summary>
    /// A <see cref="CharacterWriteStream"/> that writes to a <see cref="StringBuilder"/>.
    /// </summary>
    public class StringBuilderCharacterWriteStream : CharacterWriteStream
    {
        private readonly StringBuilder builder;
        private readonly Disposable disposable;

        protected StringBuilderCharacterWriteStream()
        {
            this.builder = new StringBuilder();
            this.disposable = Disposable.Create();
        }

        public static StringBuilderCharacterWriteStream Create()
        {
            return new StringBuilderCharacterWriteStream();
        }
        public bool IsDisposed()
        {
            return this.disposable.IsDisposed();
        }

        public Result<bool> Dispose()
        {
            return this.disposable.Dispose();
        }

        public Result<int> WriteCharacter(char character)
        {
            Pre.Condition.AssertNotDisposed(this, "this");

            return Result.Create(() =>
            {
                this.builder.Append(character);
                return 1;
            });
        }

        public Result<int> WriteCharacters(char[] characters, int startIndex, int length)
        {
            Pre.Condition.AssertNotNullAndNotEmpty(characters, nameof(characters));
            Pre.Condition.AssertAccessIndex(startIndex, characters, nameof(startIndex));
            Pre.Condition.AssertLength(length, characters, startIndex, nameof(length));
            Pre.Condition.AssertNotDisposed(this, "this");

            return Result.Create(() =>
            {
                this.builder.Append(characters, startIndex, length);
                return length;
            });
        }

        /// <summary>
        /// Remove all of the written characters.
        /// </summary>
        /// <returns>This object for method chaining.</returns>
        public StringBuilderCharacterWriteStream Clear()
        {
            this.builder.Clear();

            return this;
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
