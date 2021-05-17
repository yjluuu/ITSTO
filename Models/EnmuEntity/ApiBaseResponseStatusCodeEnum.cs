using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Routine.Models.EnmuEntity
{
    public enum ApiBaseResponseStatusCodeEnum
    {
        //成功
        Success = 2000,
        //请求参数不详
        NoDetailedInfo = 2001,
        //没有权限
        NoAuthority = 2002,
        //签名错误
        SignatureError = 2003,
        //参数错误
        ParamError = 2004,
        //系统错误
        SystemError = 2005,
        //重复操作
        RepeatOperation = 2006,
        //请求超时
        RequestOvertime = 2007,
    }
}
