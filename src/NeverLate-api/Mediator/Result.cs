namespace NeverLate_api.Mediator;

public class Result<TValue, TErrorReason> where TErrorReason: struct
{
    public bool IsSuccess { get; private set; }
    public TValue Value { get; private set; }
    public string[] Errors { get; private set; }
    public TErrorReason ErrorReason { get; set; }

    private Result() { }

    public static Result<TValue, TErrorReason> Success(TValue value) => new Result<TValue, TErrorReason>() { IsSuccess = true, Value = value };
    public static Result<TValue, TErrorReason> Failure(TErrorReason errorReason, params string[] errors) => new Result<TValue, TErrorReason>() { IsSuccess = false, Errors = errors, ErrorReason = errorReason};
}