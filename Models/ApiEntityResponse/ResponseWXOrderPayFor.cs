using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityResponse
{
    public class ResponseWXOrderPayFor
    {
        /// <summary>
        /// 返回结果【Success/Error】
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 同一下单接口的prepay_id参数
        /// </summary>
        public string PrepayId { get; set; }
        /// <summary>
        /// 第二次签名
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TradeType { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>

        public string OutTradeNo { get; set; }

        public string AppId { get; set; }
    }
}
