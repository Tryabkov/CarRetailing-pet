namespace Core.Entities;

public class OperationResult<T>
{
        public OperationResultType Type { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }

        public OperationResult(OperationResultType type, T? value, string? errorMessage = null)
        {
            Type = type;
            Value = value;
            ErrorMessage = errorMessage;
        }
        
        public static OperationResult<T> Success (T value) =>
            new(OperationResultType.Success, value);

        public static OperationResult<T> NotFound () =>
            new(OperationResultType.NotFound, default);

        public static OperationResult<T> Forbidden () =>
            new(OperationResultType.Forbidden, default);

        
        public static OperationResult<T> ServerError (string? message = null) =>
            new(OperationResultType.ServerError, default, message);
}

public enum OperationResultType
{
    Success,
    NotFound,
    Forbidden,
    ServerError
}