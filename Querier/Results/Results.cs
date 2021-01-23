namespace Querier
{
    public static class Results
    {
        public static NotFoundResult<T> NotFound<T>(T key) => new(key);
        public static ConflictResult<T> Conflict<T>(T key) => new(key);
        public static ForbidResult Forbid() => new();
        public static ValueResult<T> Value<T>(T value) => new(value);
    }
}