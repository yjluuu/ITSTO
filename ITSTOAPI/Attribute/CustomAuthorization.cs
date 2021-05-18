using Bo.Interface.IBusiness;
using Common.Tool;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSTOAPI.Attribute
{
    public class CustomAuthorization : IAuthorizationFilter
    {
        //private readonly ILogger _log = new Log4NetProvider().CreateLogger();
        //private readonly ILogger<CustomAuthorization> _log;
        private readonly ILog _log;
        private readonly IInterfaceUserService interfaceUserService;
        private readonly IInterfaceMappingService interfaceMappingService;
        private IConfiguration Configuration { get; }
        public CustomAuthorization(IInterfaceUserService interfaceUserService, IInterfaceMappingService interfaceMappingService, IConfiguration Configuration)
        {
            this._log = LogManager.GetLogger(typeof(CustomAuthorization));
            this.interfaceUserService = interfaceUserService;
            this.interfaceMappingService = interfaceMappingService;
            this.Configuration = Configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            #region 验证接口用户名是否正确
            string user = context.HttpContext.Request.Headers["User"].ToString();
            if (string.IsNullOrEmpty(user))
            {
                context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "User不能为空") { });
                OnAuthEnd(context);
                return;
            }
            InterfaceUser interfaceUser = new InterfaceUser();
            var redisInterfaceUserInfo = RedisClient.redisClient.GetStringKey($"Api.InterfaceUser:{user}").ToString();
            if (string.IsNullOrEmpty(redisInterfaceUserInfo))
            {
                interfaceUser = interfaceUserService.GetInterfaceUserByUser(new InterfaceUser() { User = user });
                if (interfaceUser == null)
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.ParamError, "接口用户名不正确") { });
                    OnAuthEnd(context);
                    return;
                }
                else
                {
                    RedisClient.redisClient.SetStringKey($"Api.InterfaceUser:{user}", JsonConvert.SerializeObject(interfaceUser));
                }
            }
            else
            {
                interfaceUser = JsonConvert.DeserializeObject<InterfaceUser>(redisInterfaceUserInfo);
            }
            #endregion
            #region 验证接口签名
            //接口验签
            var isDebug = Convert.ToBoolean(Configuration["Authorization:IsDebug"]);
            if (isDebug)
            {
                string requestTime = context.HttpContext.Request.Headers["RequestTime"].ToString();
                string nonce = context.HttpContext.Request.Headers["Nonce"].ToString();
                string signature = context.HttpContext.Request.Headers["Signature"].ToString();
                if (string.IsNullOrEmpty(requestTime))
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "RequestTime不能为空") { });
                    OnAuthEnd(context);
                    return;
                }
                if (!new System.Text.RegularExpressions.Regex(@"^\d{10}$").IsMatch(requestTime))
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.ParamError, "RequestTime格式错误") { });
                    OnAuthEnd(context);
                    return;
                }
                if (string.IsNullOrEmpty(nonce))
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Nonce不能为空") { });
                    OnAuthEnd(context);
                    return;
                }
                if (string.IsNullOrEmpty(signature))
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Signature不能为空") { });
                    OnAuthEnd(context);
                    return;
                }

                //使用十位时间戳（秒）
                var date = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
                //十三位时间戳（毫秒）
                //var date = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
                long time = Convert.ToInt64(requestTime);
                if (System.Math.Abs(date - time) > 5 * 60)
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.SignatureError, "签名已过期") { });
                    OnAuthEnd(context);
                    return;
                }
                //Signature生成规则：user+&&&+pass+&&&+requestTime+&&&+nonce生成字符串进行加密
                var sign = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}&&&{interfaceUser.Pass}&&&{requestTime}&&&{nonce}"));
                if (!signature.Equals(sign))
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.SignatureError, "签名错误") { });
                    OnAuthEnd(context);
                    return;
                }
                //是否重复请求
                var redisSignature = RedisClient.redisClient.GetStringKey($"Api.Signature:{signature}").ToString();
                if (string.IsNullOrEmpty(redisSignature))
                {
                    RedisClient.redisClient.SetStringKey($"Api.Signature:{signature}", user, TimeSpan.FromSeconds(5));
                }
                else
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.RepeatOperation, "请求忒频繁咯") { });
                    OnAuthEnd(context);
                    return;
                }
            }
            #endregion
            #region 判断是否有调用接口的权限
            VerificationJurisdiction(context, interfaceUser);
            #endregion
        }

        private void OnAuthEnd(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = 200;
            context.HttpContext.Response.ContentType = "application/json";
        }

        /// <summary>
        /// 验证接口用户是否有调用接口的权限
        /// </summary>
        /// <param name="context"></param>
        /// <param name="interfaceUser"></param>
        private void VerificationJurisdiction(AuthorizationFilterContext context, InterfaceUser interfaceUser)
        {
            string wholePath = context.HttpContext.Request.Path.ToString();
            int secondSlash = wholePath.IndexOf("/", wholePath.IndexOf("/") + 1 + 1);
            string path = wholePath.Substring(secondSlash);
            var isExistsKey = RedisClient.redisClient.KeyExists($"Api.InterfaceMapping:{interfaceUser.User}");
            if (!isExistsKey)
            {
                var interfaceMappingRes = interfaceMappingService.GetInterfaceMappingByUser(new InterfaceMapping { Brand = interfaceUser.Brand, InterfaceUserId = interfaceUser.Id });
                if (interfaceMappingRes != null && interfaceMappingRes.Any())
                {
                    foreach (var item in interfaceMappingRes)
                    {
                        RedisClient.redisClient.SetHashKey<string>($"Api.InterfaceMapping:{interfaceUser.User}", item.InterfacePath, item.InterfaceUserId.ToString());
                    }
                }
                else
                {
                    context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.SystemError, "没有配置用户接口权限") { });
                    OnAuthEnd(context);
                    return;
                }
            }
            var redisInterfaceMapping = RedisClient.redisClient.GetHashKey($"Api.InterfaceMapping:{interfaceUser.User}", path);
            if (string.IsNullOrEmpty(redisInterfaceMapping))
            {
                context.Result = new JsonResult(new ApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoAuthority, "没有权限") { });
                OnAuthEnd(context);
                return;
            }

        }
    }
}
