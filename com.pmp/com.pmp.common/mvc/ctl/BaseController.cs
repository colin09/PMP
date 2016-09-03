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

        protected int UserId = 0;
        protected string Mobile = "0";
        protected int UserLevel = -1;
        protected string NickName = "0";


        protected ILog log { get; private set; }

        protected BaseController()
        {
            log = Logger.Current();

            var cookie = HttpHelper.GetCookie(cookieName);
            log.Info($"base.Init,read cookie ==> {cookie}");
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = JsonConvert.DeserializeObject(cookie) as JObject;
                if (user != null)
                {
                    UserId = (int)user["id"];
                    Mobile = user["mobile"].ToString();
                    UserLevel = (int)user["userLevel"];
                    NickName = user["nickName"].ToString();

                }
            }
        }
    }
}
