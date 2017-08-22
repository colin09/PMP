using com.pmp.common.Config;
using com.pmp.common.helper;
using com.pmp.common.logger;
using com.pmp.common.mvc.attribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.pmp.common.mvc.ctl
{

    [ErrorFilter]
    public class BaseController : System.Web.Mvc.Controller
    {
        protected int _Longin_UserId = -1;
        protected string _Longin_Phone = "0";
        protected int _Longin_UserLevel = 0;
        protected string _Longin_RealName = "";
        protected string _Longin_Company_Name = "";
        protected int _Login_CompanyReal_ID = -1;
        //公司账户是否审核
        protected int _Longin_CompanyReal_IsApprove = -1;
        //个人账户是否审核
        protected int _Longin_Person_IsApprove = -1;

        private int _msgCount = 0;


        protected ILog log { get; private set; }

        protected ViewResult View()
        {
            ViewBag.level = _Longin_UserLevel;
            ViewBag._Longin_UserId = _Longin_UserId;
            ViewBag._Longin_Phone = _Longin_Phone;
            ViewBag._Longin_UserLevel = _Longin_UserLevel;
            ViewBag._Longin_RealName = _Longin_RealName;
            ViewBag._Longin_Company_Name = _Longin_Company_Name;
            ViewBag._Login_CompanyReal_ID = _Login_CompanyReal_ID;
            ViewBag._Longin_CompanyReal_IsApprove = _Longin_CompanyReal_IsApprove;
            ViewBag._Longin_Person_IsApprove = _Longin_Person_IsApprove;

            ViewBag.msgCount = _msgCount;
            
            return base.View();
        }
        protected ViewResult View(object model)
        {
            ViewBag.level = _Longin_UserLevel;
            ViewBag._Longin_UserId = _Longin_UserId;
            ViewBag._Longin_Phone = _Longin_Phone;
            ViewBag._Longin_UserLevel = _Longin_UserLevel;
            ViewBag._Longin_RealName = _Longin_RealName;
            ViewBag._Longin_Company_Name = _Longin_Company_Name;
            ViewBag._Login_CompanyReal_ID = _Login_CompanyReal_ID;
            ViewBag._Longin_CompanyReal_IsApprove = _Longin_CompanyReal_IsApprove;
            ViewBag._Longin_Person_IsApprove = _Longin_Person_IsApprove;

            ViewBag.msgCount = _msgCount;

            return base.View(model);
        }

        protected BaseController()
        {
            log = Logger.Current();
            var cookie = HttpHelper.GetCookie(Public_const_enum.LonginCookieName);
            log.Info($"base.Init,read cookie ==> {cookie}");
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = (JObject)JsonConvert.DeserializeObject(cookie);
                if (user != null)
                {
                    _Longin_UserId = (int)user["UserId"];
                    _Longin_Phone = user["Phone"].ToString();
                    _Longin_UserLevel = (int)user["UserLevel"];
                    _Longin_RealName = string.IsNullOrWhiteSpace(user["NickName"].ToString()) ? "匿名用户" : user["NickName"].ToString();

                    _msgCount = (int)user["MsgCount"];
                }
            }
        }
    }
}
