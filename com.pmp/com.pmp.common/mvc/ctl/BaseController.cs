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

namespace com.pmp.common.mvc.ctl
{

    [ErrorFilter]
    public class BaseController : System.Web.Mvc.Controller
    {
        private readonly string cookieName = "COOKIE_MANAGER_INFO";

        protected int _Longin_UserId = -1;
        protected string _Longin_Phone = "0";
        protected int _Longin_UserLevel = -1;
        protected string _Longin_NickName = "0";
        /// <summary>
        /// 账户状态：默认1启用，0禁用
        /// </summary>
        protected int _Longin_Status = -1;
        /// <summary>
        /// 认证状态：[默认0,提交1,审核通过2.审核不通过3.]
        /// </summary>
        protected int _Longin_Approve = -1;
        /// <summary>
        /// 注册类型
        /// </summary>
        protected int _Longin_AccountType = -1;


        protected ILog log { get; private set; }

        protected BaseController()
        {
            log = Logger.Current();

            var cookie = HttpHelper.GetCookie(cookieName);
            log.Info($"base.Init,read cookie ==> {cookie}");
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = (JObject)JsonConvert.DeserializeObject(cookie);
                if (user != null)
                {
                    _Longin_UserId = (int)user["ID"];
                    _Longin_Phone = user["Phone"].ToString();
                    _Longin_UserLevel = (int)user["Level"];
                    _Longin_NickName = user["UserName"].ToString();
                    _Longin_Status = (int)user["Status"];
                    _Longin_Approve = (int)user["IsApprove"];
                    _Longin_AccountType = (int)user["AccountType"];
                }
            }
        }
    }
}
