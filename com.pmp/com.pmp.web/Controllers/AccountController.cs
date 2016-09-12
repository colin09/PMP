using com.pmp.common.Config;
using com.pmp.common.helper;
using com.pmp.common.mvc.ctl;
using com.pmp.model.response;
using com.pmp.mongo.data;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class AccountController : WebBaseController
    {
        // GET: Account
        [Authorization]
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
        [Authorization]
        public ActionResult AccountPersonal()
        {
            var userId = _Longin_UserId;
            //if (!string.IsNullOrWhiteSpace(Request["userId"]))
            //{
            //    userId = int.Parse(Request["userId"]);
            //}
            var model = new MgUserService().SearchById(userId);
            if (model[0].PersonInfo == null)
                model[0].PersonInfo = new MgPersonInfo();

            ViewBag.level = this._Longin_UserLevel;
            return View(model[0].PersonInfo);
        }

        //提交个人信息
        [Authorization]
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
            model.Position = Request.Form["txtposition"];
            new MgUserService().UpdateAccountInfo(_Longin_Phone, model);
            return Redirect("/Account/AccountDetail");
        }

        /// <summary>
        /// 个人认证
        /// </summary>
        /// <returns></returns>
        [Authorization]
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
        [Authorization]
        public ActionResult AccountP_ApproveSumbit()
        {
            MgPersonReal mgPersonReal = new MgPersonReal();
            mgPersonReal.RealName = Request.Form["txtname"];
            mgPersonReal.CardId = Request.Form["txtcardID"];
            mgPersonReal.Gender = Request.Form["radiossex"];
            mgPersonReal.CardJustImg = UploadFiles("cardFile1");
            mgPersonReal.CardAgainstImg = UploadFiles("cardFile2");

            if (!string.IsNullOrWhiteSpace(Request.Form["seloccupation"]))
                mgPersonReal.Profession = Request.Form["seloccupation"];
            mgPersonReal.Address = Request.Form["txtaddress"];
            mgPersonReal.IsApprove = 1;
            mgPersonReal.CreatesTime = DateTime.Now.ToString();
            new MgUserService().UpdateAccountApprove(_Longin_Phone, mgPersonReal);
            return Redirect("/Account/AccountDetail");
        }


        /// <summary>
        /// 验证是否指定的图片格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsImage(string str)
        {
            bool isimage = false;
            string thestr = str.ToLower();
            thestr = thestr.Substring(thestr.Length - 4, 4);
            //限定只能上传jpg和gif图片
            string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
            //对上传的文件的类型进行一个个匹对
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (thestr == allowExtension[i])
                {
                    isimage = true;
                    break;
                }
            }
            return isimage;
        }


        private string UploadFiles(string fromfileName)
        {
            //接受上传文件
            HttpPostedFileBase postFile = Request.Files[fromfileName];
            if (postFile != null)
            {
                if (IsImage(postFile.FileName))
                {
                    DateTime time = DateTime.Now;
                    //获取上传目录 转换为物理路径
                    string uploadPath = Server.MapPath("~/CardImages/");
                    //文件名
                    string fileName = time.ToString("yyyyMMddHHmmssfff");
                    //后缀名称
                    string filetrype = System.IO.Path.GetExtension(postFile.FileName);
                    //获取文件大小
                    long contentLength = postFile.ContentLength;
                    //文件不能大于2M
                    if (contentLength <= 1024 * 2048)
                    {
                        //如果不存在path目录
                        if (!Directory.Exists(uploadPath))
                        {
                            //那么就创建它
                            Directory.CreateDirectory(uploadPath);
                        }
                        //保存文件的物理路径
                        string saveFile = uploadPath + fileName + filetrype;
                        try
                        {
                            //保存文件
                            postFile.SaveAs(saveFile);
                            return fileName + filetrype;
                        }
                        catch
                        {
                            //上传失败
                        }
                    }
                    else
                    {
                        //"文件大小超过限制要求";
                    }
                }
                else
                {
                    //文件大小超过限制要求;
                }
            }
            else
            {
                //"请选择文件";
            }
            return "";
        }

        /// <summary>
        /// 公司认证
        /// </summary>
        /// <returns></returns>
        [Authorization]
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
        [Authorization]
        public ActionResult AccountCompany_ApproveSumbit()
        {
            MgCompanyReal mr = new MgCompanyReal();
            mr.Name = Request.Form["txtname"];
            mr.RegistrID = Request.Form["txtcode"];
            mr.CompanyAddress = Request.Form["txtcompanyaddress"];
            mr.CompayCity = Request.Form["txtcity"];
            mr.BuinessScope = Request.Form["txtrange"];
            mr.OrganizationCode = Request.Form["txtzzjgcode"];
            mr.CompanyJustImg = UploadFiles("txtzzjgimg");
            mr.CompanyAgainstImg = UploadFiles("txt_yyzzfbimg");
            mr.Phone = Request.Form["txtPhone"];
            mr.ContactsName = Request.Form["txtlxrName"];
            mr.CUserID = _Longin_UserId;
            mr.IsApprove = 1;
            mr.CTime = DateTime.Now.ToString();
            new MgUserService().UpdateAccountCompanyReal(_Longin_Phone, mr, _Login_CompanyReal_ID);
            return Redirect("/Account/AccountDetail");
        }

        /// <summary>
        /// 公司员工添加
        /// </summary>
        /// <returns></returns>
        public ActionResult CompandUserAdd()
        {
            var res = false;
            if (!string.IsNullOrWhiteSpace(Request["txtphone"]))
            {
                MgUser mu = new MgUser();
                mu.Phone = Request["txtphone"];
                mu.Password = Request["txtphone"];
                mu.Level = UserLevel.CompanyUser;
                mu.CompanyReal_ID = _Login_CompanyReal_ID;
                new MgUserService().CreateCompanyUser(mu, Request["txtname"], Request["txtposition"], Request["radiossex"]);
                res = true;
            }
            ViewBag.IsSessonType = res.ToString();
            return View("/Account/CompandUserAdd");
        }

        //public ActionResult C_UserAdd()
        //{
        //    MgUser mu = new MgUser();
        //    mu.Phone = Request["txtphone"];
        //    mu.Password = Request["txtphone"];
        //    mu.Level = UserLevel.CompanyUser;
        //    mu.CompanyReal_ID = _Login_CompanyReal_ID;
        //    new MgUserService().CreateCompanyUser(mu, Request["txtname"], Request["txtposition"], Request["radiossex"]);

        //    ViewBag.IsSessonType = "true";
        //    return View("/Account/CompandUserAdd");
        //}

        public string IsExistPhone()
        {
            var phone = Request["phone"];
            var list = new MgUserService().SearchLogin(phone);
            if (list != null && list.Count > 0)
            {
                return "1";
            }
            return "0";
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
        [Authorization]
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
        [Authorization]
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
        [Authorization]
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

        public ActionResult AccountApprove()
        {
            return View();
        }

        [Authorization]
        public ActionResult UserList()
        {
            var type = Request.QueryString["type"];
            var sel_type = Request.Form["sel_type"];
            var name = Request.Form["name"];
            var companyID = Request["companyID"];
            List<MgUser> list = new List<MgUser>();
            int companyIsApprove = 0;
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
                //公司管理账户菜单连接无参数，默认登录用户公司
                if (string.IsNullOrWhiteSpace(companyID))
                {
                    companyID = _Login_CompanyReal_ID.ToString();
                    type = "2";
                }

                if (sel_type == "1")
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), name, "", ref companyIsApprove);
                else if (sel_type == "2")
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), "", name, ref companyIsApprove);
                else
                    list = new MgUserService().SearchuSserByCompanyId(int.Parse(companyID), "", "", ref companyIsApprove);
            }
            ViewBag.accountType = int.Parse(type);
            ViewBag.sel_type = string.IsNullOrWhiteSpace(Request.Form["sel_type"]) ? 2 : int.Parse(Request.Form["sel_type"]);
            ViewBag.name = name;
            ViewBag.companyId = companyID;
            ViewBag.CompanyIsApprove = companyIsApprove;
            return View(list);
        }

        /// <summary>
        /// 公司列表
        /// </summary>
        /// <returns></returns>
        [Authorization]
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
                    message = "登录失败,账号未注册！";
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
                    UserLevel ul = UserLevel.Administrator;
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

        [Authorization]
        public ActionResult GetCompanyUsers()
        {
            var service = new MgUserService();

            int CompanyIsApprove = 0;

            var cUser = service.SearchById(this._Longin_UserId).FirstOrDefault();
            var list = service.SearchuSserByCompanyId(cUser.CompanyReal_ID, "", "",ref CompanyIsApprove)
                  .Select(u => new SimpleUserRes
                  {
                      Id = u.ID,
                      Name = u.PersonInfo.RealName
                  });
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }
}

