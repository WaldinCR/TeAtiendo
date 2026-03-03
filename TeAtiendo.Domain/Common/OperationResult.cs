using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeAtiendo.Domain.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data) => new() { Success = true, Data = data };
        public static OperationResult<T> Fail(string message) => new() { Success = false, Message = message };
    }
}
