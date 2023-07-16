using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Helpers
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ServiceResult(T data, int statusCode, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            IsSuccess = IsStatusCodeSuccessful(statusCode);
        }

        private bool IsStatusCodeSuccessful(int statusCode)
        {
            return statusCode >= 200 && statusCode <= 299;
        }
    }

}
