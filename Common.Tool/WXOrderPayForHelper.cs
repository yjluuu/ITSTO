using Newtonsoft.Json;
using Routine.Models.ApiEntityResponse;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Common.Tool
{
    public class WXOrderPayForHelper
    {
        /// <summary>
        /// 微信支付调用方法
        /// </summary>
        /// <param name="openid">前台获取用户标识前台获取用户标识</param>
        /// <param name="body">商品的描述</param>
        /// <param name="total_fee">支付金额单位为（分）比如一元等于100分</param>
        /// <param name="attach">描述</param>
        /// <returns></returns>
        //public static string GetAll(string openid, string body, decimal total_fee, string attach)
        //{
        //    if (openid == null)
        //    {
        //        return "fail";
        //    }
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    var ip = context.HttpContext?.Connection.RemoteIpAddress.ToString();
        //    return Getprepay(body, openid, GetRandomString(30), getRandomTime(), Convert.ToInt32(total_fee * 100), attach);
        //}

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="body">商品描述</param>
        /// <param name="openid">前台获取用户标识前台获取用户标识</param>
        /// <param name="nonce_str">随机字符串，不长于32位。 </param>
        /// <param name="out_trade_no">商户订单号不长于32位。</param>
        /// <param name="total_fee">支付金额单位为（分）比如一元等于100分</param>
        /// <param name="spbill_create_ip">终端ip</param>
        /// <returns></returns>

        public static string Getprepay(string body, string openid, string nonce_str, string out_trade_no, int total_fee,
                                        string attach, string appid, string mch_id, string partnerkey, string notifyurl,
                                        string url)
        {
            var strA = "appid=" + appid + "&attach=" + attach + "&body=" + body + "&mch_id=" + mch_id + "&nonce_str=" + nonce_str
                + "&notify_url=" + notifyurl + "&openid=" + openid + "&out_trade_no=" + out_trade_no + "&spbill_create_ip=61.50.221.43&total_fee="
                + total_fee + "&trade_type=JSAPI";
            string strk = strA + "&key=" + partnerkey;  //key为商户平台设置的密钥key
            string strMD5 = MD5(strk).ToUpper();//MD5签名
                                                //签名
            var formData = "<xml>";
            formData += "<appid>" + appid + "</appid>";//appid  
            formData += "<attach>" + attach + "</attach>"; //附加数据(描述)
            formData += "<body>" + body + "</body>";//商品描述
            formData += "<mch_id>" + mch_id + "</mch_id>";//商户号  
            formData += "<nonce_str>" + nonce_str + "</nonce_str>";//随机字符串，不长于32位。  
            formData += "<notify_url>" + notifyurl + "</notify_url>";//通知地址
            formData += "<openid>" + openid + "</openid>";//openid
            formData += "<out_trade_no>" + out_trade_no + "</out_trade_no>";//商户订单号
            formData += "<spbill_create_ip>61.50.221.43</spbill_create_ip>";//终端IP
            formData += "<total_fee>" + total_fee + "</total_fee>";//支付金额单位为（分）
            formData += "<trade_type>JSAPI</trade_type>";//交易类型(JSAPI--公众号支付)
            formData += "<sign>" + strMD5 + "</sign>"; //签名
            formData += "</xml>";
            // 请求数据
            var getdata = sendPost(url, formData);
            //获取xml数据
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(getdata);
            ResponseWXOrderPayFor w = new ResponseWXOrderPayFor();
            if (doc != null)
            {
                XmlNode xmlNode = doc["xml"];
                if (xmlNode["return_code"].InnerText.Equals("SUCCESS") && xmlNode["result_code"].InnerText.Equals("SUCCESS"))
                {
                    #region 再次签名
                    //时间戳
                    string _time = getTime().ToString();
                    //prepay_id
                    string prepay_id = xmlNode["prepay_id"].InnerText;
                    //再次签名返回数据至小程序
                    string strB = "appId=" + appid + "&nonceStr=" + nonce_str + "&package=prepay_id=" + prepay_id + "&signType=MD5&timeStamp=" + _time + "&key=" + partnerkey;
                    #endregion
                    w.Result = "Success";
                    w.ErrMsg = "统一下单成功";
                    w.TimeStamp = _time;
                    w.NonceStr = nonce_str;
                    w.PrepayId = "prepay_id=" + prepay_id;
                    w.Sign = MD5(strB).ToUpper(); ;
                    w.SignType = "MD5";
                    w.TradeType = "JSAPI";
                    w.OutTradeNo = out_trade_no;
                }
                else
                {
                    w.Result = "Error";
                    w.ErrMsg = xmlNode["return_msg"].InnerText;
                }

            }
            else
            {
                w.Result = "Error";
                w.ErrMsg = "统一下单失败";
            }
            return JsonConvert.SerializeObject(w);
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static long getTime()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);

        }
        private static string sendPost(string URL, string urlArgs)
        {

            WebClient wCient = new WebClient();
            wCient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(urlArgs);
            byte[] responseData = wCient.UploadData(URL, "POST", postData);

            string returnStr = System.Text.Encoding.UTF8.GetString(responseData);//返回接受的数据 
            return returnStr;
        }

        /// <summary>
        /// 生成随机串    
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        private static string GetRandomString(int length)
        {
            const string key = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            if (length < 1)
                return string.Empty;

            Random rnd = new Random();
            byte[] buffer = new byte[8];

            ulong bit = 31;
            ulong result = 0;
            int index = 0;
            StringBuilder sb = new StringBuilder((length / 5 + 1) * 5);

            while (sb.Length < length)
            {
                rnd.NextBytes(buffer);

                buffer[5] = buffer[6] = buffer[7] = 0x00;
                result = BitConverter.ToUInt64(buffer, 0);

                while (result > 0 && sb.Length < length)
                {
                    index = (int)(bit & result);
                    sb.Append(key[index]);
                    result = result >> 5;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        private static string getRandomTime()
        {
            Random rd = new Random();//用于生成随机数
            string DateStr = DateTime.Now.ToString("yyyyMMddHHmmssMM");//日期
            string str = DateStr + rd.Next(10000).ToString().PadLeft(4, '0');//带日期的随机数
            return str;
        }
        /// <summary>
        /// MD5签名方法  
        /// </summary>  
        /// <param name="inputText">加密参数</param>  
        /// <returns></returns>  
        private static string MD5(string inputText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(inputText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
        }


    }
}
