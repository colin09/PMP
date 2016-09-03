using com.pmp.common.helper;
using com.pmp.common.mvc.ctl;
using com.pmp.mongo.data;
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


        public ActionResult AccountPersonal()
        {
            var model = new MgUserService().SearchLogin(Phone);
            if (model[0].PersonInfo == null)
                model[0].PersonInfo = new MgPersonInfo();
            return View(model[0].PersonInfo);
        }

        /// <summary>
        /// 个人认证
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountP_Approve()
        {
            //var model = new MgUserService().SearchLogin(Phone);
            //if (model[0].PersonReal == null)
            //    model[0].PersonReal = new MgPersonReal();
            //return View(model[0].PersonReal);
            ViewBag.phone = Phone;
            return View();
        }

        /// <summary>
        /// 个人认证提交
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountP_ApproveSumbit()
        {
            MgPersonReal mgPersonReal = new MgPersonReal();
            mgPersonReal.RealName = Request.Form["txtname"];
            mgPersonReal.CardId = Request.Form["txtcardID"];
            mgPersonReal.Gender = Request.Form["radiossex"];
            mgPersonReal.CardJustImg = Request.Form["txtcardImg1"];
            mgPersonReal.CardAgainstImg = Request.Form["txtcardImg2"];
            if (!string.IsNullOrWhiteSpace(Request.Form["seloccupation"]))
                mgPersonReal.Profession = int.Parse(Request.Form["seloccupation"]);
            mgPersonReal.Address = Request.Form["txtaddress"];
            new MgUserService().UpdateAccountApprove(Phone, mgPersonReal);
            Response.Redirect("/Account/AccountP_Approve");
            return View();
        }

        /// <summary>
        /// 公司认证
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountCompany_Approve()
        {
            ViewBag.phone = Phone;
            return View();
        }
        /// <summary>
        /// 提交公司认证
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountCompany_ApproveSumbit()
        {
            MgCompanyReal mr = new MgCompanyReal();
            mr.Name = Request.Form["txtname"];
            mr.RegistrID = Request.Form["txtcode"];
            mr.CompanyAddress = Request.Form["txtcompanyaddress"];
            mr.BuinessScope = Request.Form["txtrange"];
            mr.OrganizationCode = Request.Form["txtzzjgcode"];
            mr.CompanyJustImg = Request.Form["txtzzjgimg"];
            mr.Address = Request.Form["txtaddress"];
            mr.CompanyAgainstImg = Request.Form["txt_yyzzfbimg"];
            new MgUserService().UpdateAccountCompanyReal(Phone, mr);
            Response.Redirect("/Account/AccountP_Approve");
            return View();
        }


        //提交个人信息
        public ActionResult AccountPersonalInfoSumbit()
        {
            MgPersonInfo model = new MgPersonInfo();
            model.RealName = Request.Form["txtname"];
            model.Address = Request.Form["txtaddress"];
            model.Birthday = Request.Form["txtbirthday"];
            model.Gender = Request.Form["radiossex"];
            model.Skill = Request.Form["txtskll"];
            model.Introduction = Request.Form["txtintro"];
            new MgUserService().UpdateAccountInfo(Phone, model);
            Response.Redirect("/Account/AccountPersonal");
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
                        RecordUserLogonStatus(HttpHelper.ObjectToJson(info[0]));
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
                message = "失败,数据库异常！";
                log.Info($"注册失败 == name> {name }");
            }
            return "{\"IsSuccess\":\"" + IsSuccess + "\",\"message\":\"" + message + "\"}";
        }


        public void RecordUserLogonStatus(string value)
        {
            HttpHelper.WriteCookie("COOKIE_MANAGER_INFO", value, 14400);
        }

    }
}