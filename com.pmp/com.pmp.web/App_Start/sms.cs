using com.pmp.web.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.pmp.web.App_Start
{
    public class sms
    {
        public static bool SendSms(string phone, string mess)
        {
            SDKClientClient sdkclient = new SDKClientClient();
            return sdkclient.sendSMS("", "greple", "", new string[] { phone }, "【绿色苹果】验证码为:" + mess, "", "GBK", 5, 0) == 0;
        }
    }
}