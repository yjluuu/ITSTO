using Routine.Models.EnmuEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityResponse
{
    public class ApiBaseResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Msg { get; set; }
        public object ReturnObj { get; set; }
        public ApiBaseResponse()
        {
            IsSuccess = true;
            StatusCode = (int)ApiBaseResponseStatusCodeEnum.Success;
            Msg = "操作成功";
        }
        public ApiBaseResponse(ApiBaseResponseStatusCodeEnum statusCodeEnum, string message)
        {
            IsSuccess = false;
            StatusCode = (int)statusCodeEnum;
            Msg = message;
        }
    }
}
