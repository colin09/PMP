using com.pmp.common.Config;
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
    public class AccountController : WebBaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            ViewBag.level = this._Longin_UserLevel;
            return View();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 个人基本信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountPersonal()
        {
            var userId = _Longin_UserId;
            if (!string.IsNullOrWhiteSpace(Request["userId"]))
            {
                userId = int.Parse(Request["userId"]);
            }
            var model = new MgUserService().SearchById(userId);
            if (model[0].PersonInfo == null)
                model[0].PersonInfo = new MgPersonInfo();

            ViewBag.level = this._Longin_UserLevel;
            return View(model[0].PersonInfo);
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
            model.Email = Request.Form["txtEmail"];
            new MgUserService().UpdateAccountInfo(_Longin_Phone, model);
            Response.Redirect("/Account/AccountPersonal");
            return View();
        }

        /// <summary>
        /// 个人认证
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountP_Approve()
        {
            ViewBag.phone = _Longin_Phone;
            ViewBag.level = this._Longin_UserLevel;
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
            mgPersonReal.IsApprove = 1;
            mgPersonReal.CreatesTime = DateTime.Now.ToString();
            new MgUserService().UpdateAccountApprove(_Longin_Phone, mgPersonReal);
            Response.Redirect("/Account/AccountP_Approve");
            return View();
        }

        /// <summary>
        /// 公司认证
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountCompany_Approve()
        {
            ViewBag.phone = _Longin_Phone;
            ViewBag.level = this._Longin_UserLevel;
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
            mr.CompayCity = Request.Form["txtcity"];
            mr.BuinessScope = Request.Form["txtrange"];
            mr.OrganizationCode = Request.Form["txtzzjgcode"];
            mr.CompanyJustImg = Request.Form["txtzzjgimg"];
            mr.Address = Request.Form["txtaddress"];
            mr.CompanyAgainstImg = Request.Form["txt_yyzzfbimg"];
            mr.Phone = Request.Form["txtPhone"];
            mr.ContactsName = Request.Form["txtlxrName"];
            mr.CUserID = _Longin_UserId;
            mr.IsApprove = 1;
            mr.CTime = DateTime.Now.ToString();
            new MgUserService().UpdateAccountCompanyReal(_Longin_Phone, mr);
            Response.Redirect("/Account/AccountP_Approve");
            return View();
        }

        /// <summary>
        /// 系统管理员审核
        /// </summary>
        /// <returns></returns>
        [AuthorizationAttribute]
        public ActionResult AccountAudit()
        {
            ViewBag.level = this._Longin_UserLevel;
            return View();
        }

        /// <summary>
        /// 系统管理员审核
        /// </summary>
        /// <returns></returns>
        public string AccountAuditRes()
        {
            var accountType = Request["type"];
            var accountId = Request["id"];
            var resAudit = Request["res"];
            var notpassstr = Request["notpassstr"];
            bool res = true;
            if (int.Parse(accountType) == 1)
                res = new MgUserService().UpdateAudit(int.Parse(accountId), int.Parse(resAudit), notpassstr);
            else
            {
                res = new MgCompanyRealService().UpdateAudit(int.Parse(accountId), int.Parse(resAudit), notpassstr);
            }
            return res.ToString();
        }


        /// <summary>
        /// 获取审核列表
        /// </summary>
        /// <returns></returns>
        public string GetAccountAuditList()
        {
            var auditType = int.Parse(Request["AuditType"]);
            var accountType = int.Parse(Request["AccountType"]);

            return HttpHelper.ObjectToJson(new MgUserService().SearchAllByAudit(auditType, accountType));
        }


        /// <summary>
        /// 个人详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountDetail()
        {
            long uid = 0;
            if (!string.IsNullOrWhiteSpace(Request["uid"]))
                uid = long.Parse(Request["uid"]);
            else
                uid = long.Parse(_Longin_UserId.ToString());

            MgCompanyReal mgCompanyReal = new MgCompanyReal();
            MgUser mgUser = new MgUser() { PersonInfo = new MgPersonInfo(), PersonReal = new MgPersonReal() };

            mgUser = new MgUserService().SearchById(uid)[0];
            if (mgUser.CompanyReal_ID > 0)
            {
                mgCompanyReal = new MgCompanyRealService().SearchById(mgUser.CompanyReal_ID)[0];
            }
            ViewBag.CompanyReal = mgCompanyReal;
            ViewBag.mgUser = mgUser;
            ViewBag.level = this._Longin_UserLevel;
            return View();
        }



        public ActionResult UserList()
        {
            var type = Request.QueryString["type"];
            var sel_type = Request.Form["sel_type"];
            var name = Request.Form["name"];
            List<MgUser> list = new List<MgUser>();
            if (type == "1")
            {
                if (sel_type == "1")
                    list = new MgUserService().SearchWhere(name, "");
                else if (sel_type == "2")
                    list = new MgUserService().SearchWhere("", name);
                else
                    list = new MgUserService().SearchWhere("", "");
            }
            else
            {
                var companyID = Request["companyID"];
                if (sel_type == "1")
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), name, "");
                else if (sel_type == "2")
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), "", name);
                else
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), "", "");
            }
            ViewBag.accountType = int.Parse(type);
            ViewBag.sel_type = string.IsNullOrWhiteSpace(Request.Form["sel_type"]) ? 2 : int.Parse(Request.Form["sel_type"]);
            ViewBag.name = name;
            return View(list);
        }

        /// <summary>
        /// 公司列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyUserList()
        {
            var sel_type = Request.Form["sel_type"];
            var name = Request.Form["name"];
            List<MgCompanyReal> list = new List<MgCompanyReal>();
            if (sel_type == "1")
                list = new MgCompanyRealService().SearchWhere("", name, "");
            else if (sel_type == "2")
                list = new MgCompanyRealService().SearchWhere(name, "", "");
            else if (sel_type == "3")
                list = new MgCompanyRealService().SearchWhere("", "", name);
            else
                list = new MgCompanyRealService().SearchWhere("", "", "");
            ViewBag.sel_type = string.IsNullOrWhiteSpace(Request.Form["sel_type"]) ? 2 : int.Parse(Request.Form["sel_type"]);
            ViewBag.name = name;
            return View(list);
        }

        public ActionResult UserDetail()
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
                    {
                        LoginUser ui = new LoginUser()
                        {
                            NickName = info[0].PersonInfo.RealName,
                            Phone = info[0].Phone,
                            UserId = info[0].ID,
                            UserLevel = (UserLevel)info[0].Level
                        };
                        RecordUserLogonStatus(HttpHelper.ObjectToJson(ui));
                    }
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
                log.Info($"注册失败 == name> {name },{ex}");
            }
            return "{\"IsSuccess\":\"" + IsSuccess + "\",\"message\":\"" + message + "\"}";
        }

        public string AcconutRegistered()
        {
            bool IsSuccess = true;
            string message = "注册成功！";
            var name = Request["code"];
            var password = Request["password"];
            var tempType = int.Parse(Request["type"]);
            try
            {
                MgUserService mgUserService = new MgUserService();
                if (mgUserService.SearchLogin(name.ToString()).Count > 0)
                {
                    IsSuccess = false;
                    message = "失败,手机号已注册！";
                }
                else
                {
                    UserLevel ul = UserLevel.Default;
                    if (tempType == 1)
                        ul = UserLevel.Person;
                    else if (tempType == 2)
                        ul = UserLevel.CompanyAdmin;

                    mgUserService.CreateUser(name.Trim(), password.Trim(), (int)ul);
                }
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
            new HttpHelper().SetSession(Public_const_enum.LonginCookieName, value);
        }
    }
}

