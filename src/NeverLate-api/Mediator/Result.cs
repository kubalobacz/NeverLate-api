namespace NeverLate_api.Mediator;

public class Result<TValue>
{
    public bool IsSuccess { get; private init; }
    public TValue Value { get; private init; }

    private Result() { }

    public static Result<TValue> Success(TValue value) => new() { IsSuccess = true, Value = value };
    public static Result<TValue> Failure() => new() { IsSuccess = false};
}

public class Result<TValue, TErrorReason> where TErrorReason: struct
{
    public bool IsSuccess { get; private init; }
    public TValue Value { get; private init; }
    public TErrorReason ErrorReason { get; private init; }

    private Result() { }

    public static Result<TValue, TErrorReason> Success(TValue value) => new() { IsSuccess = true, Value = value };
    public static Result<TValue, TErrorReason> Failure(TErrorReason errorReason) => new() { IsSuccess = false, ErrorReason = errorReason};
}