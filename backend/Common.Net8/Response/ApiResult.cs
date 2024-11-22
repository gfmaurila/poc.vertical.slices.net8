namespace Common.Net8.Response;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string SuccessMessage { get; set; }
    public int StatusCode { get; set; }
    public List<ErrorDetail> Errors { get; set; }

    public T Data { get; set; }

    public static ApiResult<T> CreateError(List<ErrorDetail> errors, int statusCode)
    {
        return new ApiResult<T>
        {
            Success = false,
            StatusCode = statusCode,
            Errors = errors
        };
    }

    public static ApiResult<T> CreateSuccess(T result)
    {
        return new ApiResult<T>
        {
            Success = true,
            SuccessMessage = "Cadastrado com sucesso!",
            StatusCode = 200,
            Errors = new List<ErrorDetail>()
        };
    }

    public static ApiResult<T> CreateSuccess(T value, string successMessage)
    {
        return new ApiResult<T>
        {
            Success = true,
            SuccessMessage = successMessage,
            StatusCode = 200,
            Errors = new List<ErrorDetail>(),
            Data = value
        };
    }
}
