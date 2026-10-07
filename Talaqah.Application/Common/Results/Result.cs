namespace Talaqah.Application.Common.Results;
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Data { get; }
    public IReadOnlyList<string> Errors { get; }
    public string Message { get; }

    private Result(bool isSuccess, T? data, IReadOnlyList<string> errors, string message)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
        Message = message;
    }

    public static Result<T> Success(T data, string message = "")
        => new(true, data, Array.Empty<string>(), message);

    public static Result<T> Failure(string error)
        => new(false, default, [error], string.Empty);

    public static Result<T> Failure(params string[] errors)
        => new(false, default, errors, string.Empty);

    public static Result<T> Failure(IEnumerable<string> errors)
        => new(false, default, errors.ToArray(), string.Empty);
}
