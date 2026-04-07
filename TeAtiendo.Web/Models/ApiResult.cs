namespace TeAtiendo.Web.Models
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        public static ApiResult<T> Success(T data, int statusCode = 200)
        {
            return new ApiResult<T> { IsSuccess = true, Data = data, StatusCode = statusCode };
        }

        public static ApiResult<T> Failure(string error, int statusCode)
        {
            return new ApiResult<T> { IsSuccess = false, ErrorMessage = error, StatusCode = statusCode };
        }
    }
}