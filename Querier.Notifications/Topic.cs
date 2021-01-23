namespace Querier.Notifications
{
    public readonly struct Topic<T>
    {
        public Topic(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static implicit operator Topic<T>(string value) => new(value);
    }
}