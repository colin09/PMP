using com.pmp.common.helper;
using com.pmp.common.mvc.ctl;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public string SignIn()
        {
            bool IsSuccess = true;
            string message = "登录成功！";
            var name = Request["code"];
            var password = Request["password"];
            try
            {
                MgUserService mgUserService = new MgUserService();
                var info = mgUserService.SearchLogin(name.ToString().Trim());
                if (info.Count > 0)
                {
                    if (info[0].Password != password.Trim())
                    {
                        IsSuccess = false;
                        message = "登录失败,密码错误！";
                    }
                    else
                        RecordUserLogonStatus(HttpHelper.ObjectToJson(info));
                }
                else
                {
                    IsSuccess = false;
                    message = "登录失败,账号为注册！";
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                message = "失败,程序异常！";
                log.Info($"注册失败 == name> {name }");
            }
            return "{\"IsSuccess\":\"" + IsSuccess + "\",\"message\":\"" + message + "\"}";
        }

        public string AcconutRegistered()
        {
            bool IsSuccess = true;
            string message = "注册成功！";
            var name = Request["code"];
            var password = Request["password"];
            var type = Request["type"];
            try
            {
                MgUserService mgUserService = new MgUserService();
                if (mgUserService.SearchLogin(name.ToString()).Count > 0)
                {
                    IsSuccess = false;
                    message = "失败,手机号已注册！";
                }
                else
                    mgUserService.CreateUser(name.Trim(), password.Trim(), int.Parse(type));
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                message = "失败,数据库连接异常！";
                log.Info($"注册失败 == name> {name }");
            }
            return "{\"IsSuccess\":\"" + IsSuccess + "\",\"message\":\"" + message + "\"}"; ;
        }


        public void RecordUserLogonStatus(string value)
        {
            HttpHelper.WriteCookie("COOKIE_MANAGER_INFO", value, 14400);
        }

    }
}