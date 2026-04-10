using System;
using System.Net;

namespace TeAtiendo.Desktop.Services
{
    public sealed class ApiServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? ResponseBody { get; }

        public ApiServiceException(HttpStatusCode statusCode, string message, string? responseBody = null)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}