namespace CarRental.Application.Common;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;
    /// <summary>
    /// Gets the primary error message, if any.
    /// </summary>
    public string? Error { get; }
    /// <summary>
    /// Gets the collection of errors, if any.
    /// </summary>
    public IReadOnlyList<string> Errors { get; }

    protected Result(bool isSuccess, string? error, IReadOnlyList<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors ?? [];
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new(true, null);
    /// <summary>
    /// Creates a failure result with a single error.
    /// </summary>
    public static Result Failure(string error) => new(false, error);
    /// <summary>
    /// Creates a failure result with multiple errors.
    /// </summary>
    public static Result Failure(IReadOnlyList<string> errors) => new(false, errors.FirstOrDefault(), errors);
}

/// <summary>
/// Represents the result of an operation with a value payload.
/// </summary>
public class Result<T> : Result
{
    /// <summary>
    /// Gets the value associated with a successful result.
    /// </summary>
    public T? Value { get; }

    private Result(bool isSuccess, T? value, string? error, IReadOnlyList<string>? errors = null)
        : base(isSuccess, error, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    public static Result<T> Success(T value) => new(true, value, null);
    /// <summary>
    /// Creates a failure result with a single error.
    /// </summary>
    public new static Result<T> Failure(string error) => new(false, default, error);
    /// <summary>
    /// Creates a failure result with multiple errors.
    /// </summary>
    public new static Result<T> Failure(IReadOnlyList<string> errors) => new(false, default, errors.FirstOrDefault(), errors);

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// </summary>
    public static implicit operator Result<T>(T value) => Success(value);
}
