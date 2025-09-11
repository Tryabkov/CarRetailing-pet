namespace Core.Entities;

public class OperationResult<T>
{
    public OperationResult(OperationResultType type, T? value, string? errorMessage = null)
    {
        Type = type;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public OperationResultType Type { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }

    public static OperationResult<T> Success(T value)
    {
        return new OperationResult<T>(OperationResultType.Success, value);
    }

    public static OperationResult<T> NotFound()
    {
        return new OperationResult<T>(OperationResultType.NotFound, default);
    }

    public static OperationResult<T> Forbidden()
    {
        return new OperationResult<T>(OperationResultType.Forbidden, default);
    }


    public static OperationResult<T> ServerError(string? message = null)
    {
        return new OperationResult<T>(OperationResultType.ServerError, default, message);
    }
}

public enum OperationResultType
{
    Success,
    NotFound,
    Forbidden,
    ServerError
}