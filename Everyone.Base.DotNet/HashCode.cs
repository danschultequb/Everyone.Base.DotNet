namespace everyone
{
    public class HashCode
    {
        private HashCode()
        {
            this.Value = 0;
        }

        public static HashCode Create(params object?[] values)
        {
            return new HashCode().AddAll(values);
        }

        public int Value { get; private set; }

        public static explicit operator int(HashCode hashCode)
        {
            return hashCode.Value;
        }

        public HashCode Add(object? value)
        {
            this.Value = (this.Value << 5) + this.Value;
            
            if (value != null)
            {
                this.Value ^= value.GetHashCode();
            }

            return this;
        }

        public HashCode AddAll(params object?[] values)
        {
            if (values != null)
            {
                foreach (object? value in values)
                {
                    this.Add(value);
                }
            }
            return this;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is HashCode rhs &&
                this.Value == rhs.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public static int Get(params object?[] values)
        {
            return HashCode.Create(values).Value;
        }
    }
}
