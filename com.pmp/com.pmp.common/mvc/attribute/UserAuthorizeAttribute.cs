using com.pmp.common.helper;
using com.pmp.common.logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.common.mvc.attribute
{
    public class UserAuthorizeAttribute: AuthorizeAttribute
    {

        private readonly string cookieName = "COOKIE_MANAGER_INFO";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Logger.Current().Info("<HR />");
            var flag = false;
            var cookie = HttpHelper.GetCookie(cookieName);
            if (!string.IsNullOrEmpty(cookie))
                flag = true;
            return flag;
        }




        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUri = filterContext.RequestContext.HttpContext.Request.Url;
            Logger.Current().Info("获取用户Cookie失败，跳转到登录。");
            
                filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
