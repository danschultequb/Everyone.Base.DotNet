using System;
using System.Text;

namespace Everyone
{
    /// <summary>
    /// A <see cref="CharacterWriteStream"/> that writes to a <see cref="StringBuilder"/>.
    /// </summary>
    public class StringBuilderCharacterStream : CharacterWriteStream, CharacterReadStream
    {
        private readonly StringBuilder builder;
        private readonly Mutex mutex;
        private readonly MutableMutexCondition streamChanged;
        private readonly Disposable disposable;
        private bool endOfStream;

        protected StringBuilderCharacterStream(Mutex mutex)
        {
            Pre.Condition.AssertNotNull(mutex, nameof(mutex));

            this.builder = new StringBuilder();
            this.mutex = mutex;
            this.streamChanged = mutex.CreateCondition(() =>
            {
                return this.builder.Length >= 1 || !this.IsNotDisposed() || this.IsEndOfStream();
            });

            this.disposable = Disposable.Create(() =>
            {
                using (this.mutex.CriticalSection().Await())
                {
                    this.streamChanged.Signal().Await();
                }
            });
        }

        public static StringBuilderCharacterStream Create(Mutex mutex)
        {
            return new StringBuilderCharacterStream(mutex);
        }

        public DisposableState GetDisposableState()
        {
            return this.disposable.GetDisposableState();
        }

        public Result<bool> Dispose()
        {
            return this.disposable.Dispose();
        }

        /// <summary>
        /// Get whether this <see cref="StringBuilderCharacterStream"/> has been ended so that
        /// it can't be written to anymore. This simulates when other streams run out of data to be
        /// read.
        /// </summary>
        public bool IsEndOfStream()
        {
            return this.endOfStream;
        }

        /// <summary>
        /// Mark this <see cref="StringBuilderCharacterStream"/> as being ended. This means
        /// that data can't be written to it anymore. This simulates when other streams run out of
        /// data to be read.
        /// </summary>
        public StringBuilderCharacterStream EndOfStream()
        {
            using (this.mutex.CriticalSection().Await())
            {
                this.endOfStream = true;
                this.streamChanged.Signal().Await();
            }

            Post.Condition.AssertTrue(this.IsEndOfStream(), "this.IsEndOfStream()");

            return this;
        }

        public Result<int> WriteCharacter(char character)
        {
            Pre.Condition.AssertNotDisposed(this, "this");
            Pre.Condition.AssertFalse(this.IsEndOfStream(), "this.IsEndOfStream()");

            return this.mutex.CriticalSection(() =>
            {
                Pre.Condition.AssertNotDisposed(this, "this");
                Pre.Condition.AssertFalse(this.IsEndOfStream(), "this.IsEndOfStream()");

                this.builder.Append(character);
                this.streamChanged.Signal().Await();

                return 1;
            });
        }

        public Result<int> WriteCharacters(char[] characters, int startIndex, int length)
        {
            Pre.Condition.AssertNotNullAndNotEmpty(characters, nameof(characters));
            Pre.Condition.AssertAccessIndex(startIndex, characters, nameof(startIndex));
            Pre.Condition.AssertLength(length, characters, startIndex, nameof(length));
            Pre.Condition.AssertNotDisposed(this, "this");
            Pre.Condition.AssertFalse(this.IsEndOfStream(), "this.IsEndOfStream()");

            return this.mutex.CriticalSection(() =>
            {
                Pre.Condition.AssertNotDisposed(this, "this");
                Pre.Condition.AssertFalse(this.IsEndOfStream(), "this.IsEndOfStream()");

                this.builder.Append(characters, startIndex, length);
                this.streamChanged.Signal().Await();

                return length;
            });
        }

        public Result<char> ReadCharacter()
        {
            Pre.Condition.AssertNotDisposed(this, "this");

            return this.mutex.CriticalSection(() =>
            {
                Pre.Condition.AssertNotDisposed(this, "this");

                while (this.builder.Length == 0)
                {
                    this.streamChanged.Watch().Await();

                    if (this.IsDisposed())
                    {
                        throw new DisposedException();
                    }
                    else if (this.IsEndOfStream())
                    {
                        throw new EmptyException();
                    }
                }

                char result = this.builder[0];
                this.builder.Remove(startIndex: 0, length: 1);

                return result;
            });
        }

        public Result<int> ReadCharacters(char[] charactersRead, int charactersReadStartIndex, int maximumToRead)
        {
            Pre.Condition.AssertNotNull(charactersRead, nameof(charactersRead));
            Pre.Condition.AssertAccessIndex(charactersReadStartIndex, charactersRead, nameof(charactersReadStartIndex));
            Pre.Condition.AssertLength(maximumToRead, charactersRead, charactersReadStartIndex, nameof(maximumToRead));
            Pre.Condition.AssertNotDisposed(this, "this");

            return this.mutex.CriticalSection(() =>
            {
                Pre.Condition.AssertNotDisposed(this, "this");

                while (this.builder.Length == 0)
                {
                    this.streamChanged.Watch().Await();

                    if (this.IsDisposed())
                    {
                        throw new DisposedException();
                    }
                    else if (this.IsEndOfStream())
                    {
                        throw new EmptyException();
                    }
                }

                int result = Math.Min(maximumToRead, this.builder.Length);
                for (int i = 0; i < result; i++)
                {
                    charactersRead[charactersReadStartIndex + i] = this.builder[i];
                }
                this.builder.Remove(0, result);

                return result;
            });
        }

        /// <summary>
        /// Remove all of the written characters.
        /// </summary>
        /// <returns>This object for method chaining.</returns>
        public StringBuilderCharacterStream Clear()
        {
            Pre.Condition.AssertNotDisposed(this, "this");
            Pre.Condition.AssertFalse(this.IsEndOfStream(), "this.IsEndOfStream()");

            using (this.mutex.CriticalSection().Await())
            {
                this.builder.Clear();
            }

            return this;
        }

        public override string ToString()
        {
            return this.builder.ToString();
        }
    }
}
