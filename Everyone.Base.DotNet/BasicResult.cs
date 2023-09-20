namespace Everyone
{
    public class BasicResult : ResultBase
    {
        private readonly Error? error;

        protected BasicResult(Error? error)
        {
            this.error = error;
        }

        public static BasicResult Create()
        {
            return new BasicResult(error: null);
        }

        public static BasicResult Create(Error error)
        {
            Pre.Condition.AssertNotNull(error, nameof(error));

            return new BasicResult(error);
        }

        public static BasicResult<T> Create<T>(Error error)
        {
            Pre.Condition.AssertNotNull(error, nameof(error));

            return BasicResult<T>.Create(error);
        }

        public static BasicResult<T> Create<T>(T value)
        {
            return BasicResult<T>.Create(value);
        }

        public override void Await()
        {
            if (this.error != null)
            {
                throw new AwaitErrorException(this.error);
            }
        }
    }

    public class BasicResult<T> : ResultBase<T>
    {
        private readonly T? value;
        private readonly Error? error;

        protected BasicResult(T? value, Error? error)
        {
            this.value = value;
            this.error = error;
        }

        public static BasicResult<T> Create(Error? error)
        {
            return new BasicResult<T>(value: default, error: error);
        }

        public static BasicResult<T> Create(T value)
        {
            return new BasicResult<T>(value: value, error: null);
        }

        public override T Await()
        {
            if (this.error != null)
            {
                throw new AwaitErrorException(this.error);
            }
            return this.value!;
        }
    }
}
