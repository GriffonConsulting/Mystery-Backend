﻿using System.ComponentModel.DataAnnotations;

namespace Application.Common.Requests
{
    public class RequestResult<T> : RequestResult
    {
        [Required]
        public T Result { get; set; }
    }

    public class RequestResult : IRequestResult
    {
        public string Message { get; set; }

        public static RequestResult<T> Create<T>(T result)
        {
            return new RequestResult<T>
            {
                Result = result
            };
        }
    }
}
