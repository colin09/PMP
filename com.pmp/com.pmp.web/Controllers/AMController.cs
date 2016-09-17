using com.pmp.common.helper;
using com.pmp.mongo.data;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class AMController : Controller
    {
        // GET: AM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }

        public string _Sesson_Code = "_Registered_Code";

        public string CreateCode()
        {
            string phone = Request["password"];
            var code = "";
            Random rd = new Random();
            for (int i = 0; i <= 5; i++)
            {
                code += rd.Next(0, 9).ToString();
            }
            new HttpHelper().SetSession(_Sesson_Code, code);
            return com.pmp.web.App_Start.sms.SendSms(phone, code).ToString();
        }

        public string AddAdmin()
        {
            bool IsSuccess = true;
            string message = "成功！";
            var name = Request["code"];
            var password = Request["password"];
            var key = Request["key"];

            try
            {
                if (key != com.pmp.onetime.Public_Key.Administrator_Key)
                {
                    IsSuccess = false;
                    message = "密钥错误！";
                }
                else
                {
                    MgUserService mgUserService = new MgUserService();
                    if (mgUserService.SearchLogin(name.ToString()).Count > 0)
                    {
                        IsSuccess = false;
                        message = "失败,手机号已注册！";
                    }
                    else
                    {
                        //var code = new HttpHelper().GetSession(_Sesson_Code);
                        //if (code == key)
                        //{
                        UserLevel ul = UserLevel.Administrator;
                        mgUserService.CreateUser(name.Trim(), password.Trim(), (int)ul);
                        //}
                        //else
                        //{
                        //    IsSuccess = false;
                        //    message = "验证码错误！";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                message = "失败,数据库异常！";
            }
            return "{\"IsSuccess\":\"" + IsSuccess + "\",\"message\":\"" + message + "\"}";
        }
    }
}