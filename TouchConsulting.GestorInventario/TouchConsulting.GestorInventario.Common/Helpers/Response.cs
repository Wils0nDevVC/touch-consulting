﻿
using System.Collections.Generic;

namespace TouchConsulting.GestorInventario.Common.Helpers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

        public List<string>? Errors { get; set; }
    }
}
