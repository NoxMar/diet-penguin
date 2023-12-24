namespace DietPenguin.Core;

public class Result<T>
{
    public bool IsSuccess { get; }

    private readonly T? _value;

    public T Value => (IsSuccess, _value) switch
    {
        (false, _) => throw new InvalidOperationException("You shouldn't access value of failed result"),
        (true, null) => throw new ArgumentNullException(nameof(_value)),
        _ => _value
    };
    public Error Error { get; }

    private Result(bool isSuccess, T? value, Error error)
    {
        IsSuccess = isSuccess;
        _value = value;
        Error = error;
    }

    public static Result<T> Success(T value) =>
        new(true, value, Error.None);

    public static Result<T> Failure(Error error) =>
        new(false, default, error);
}

public static class ResultExtensions
{
    public static TReturn Match<TResult, TReturn>(
        this Result<TResult> result,
        Func<TResult, TReturn> onSuccess,
        Func<Error, TReturn> onFailure)
    => result.IsSuccess ? onSuccess(result.Value) :
        onFailure(result.Error);
}