using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity.WeChat
{
    public class WechatOrderPayParam
    {
        //公众账号ID
        public string appid { get; set; }

        //商户号
        public string mch_id { get; set; }

        //设备号
        public string device_info { get; set; }

        //随机字符串
        public string nonce_str { get; set; }

        //签名
        public string sign { get; set; }

        //签名类型
        public string sign_type { get; set; }

        //商品描述
        public string body { get; set; }

        //商品详情
        public string detail { get; set; }

        //附加数据
        public string attach { get; set; }

        //商户订单号
        public string out_trade_no { get; set; }

        //标价币种
        public string fee_type { get; set; }

        //标价金额
        public int total_fee { get; set; }

        //终端IP
        public string spbill_create_ip { get; set; }

        //交易起始时间
        public string time_start { get; set; }

        //交易结束时间
        public string time_expire { get; set; }

        //订单优惠标记
        public string goods_tag { get; set; }

        //通知地址
        public string notify_url { get; set; }

        //交易类型
        public string trade_type { get; set; }

        //商品ID
        public string product_id { get; set; }

        //指定支付方式
        public string limit_pay { get; set; }

        //用户标识
        public string openid { get; set; }

        //电子发票入口开放标识
        public string receipt { get; set; }

        //是否需要分账
        public string profit_sharing { get; set; }

        //场景信息
        public string scene_info { get; set; }
    }
}
