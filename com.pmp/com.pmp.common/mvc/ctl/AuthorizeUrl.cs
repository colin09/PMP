using com.pmp.common.Config;
using com.pmp.common.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.common.mvc.ctl
{
    public class AuthorizeUrl : FilterAttribute, IAuthorizationFilter
    {

        /// <summary>
        /// 处理用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!isURl(filterContext.HttpContext))
            {
                filterContext.HttpContext.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                filterContext.HttpContext.Response.End();
                return;
            }
        }

        private bool isURl(HttpContextBase httpcontext)
        {
            if (httpcontext.Request.UrlReferrer != null)
            {
                string Url = httpcontext.Request.UrlReferrer.Host;

                string[] UrlArray = Url.Split('.');

                StringBuilder domin = new StringBuilder();

                for (int i = 1; i < UrlArray.Length; i++)
                {
                    domin.Append(".");
                    domin.Append(UrlArray[i]);
                }
                if (domin.ToString() == "")
                    return true;
                else
                    return false;
            }
            return false;
        }
    }

    /// <summary>
    /// 表示需要用户登录才可以使用的特性
    /// <para>如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 处理用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!isLogin(filterContext.HttpContext))
            {
                RedirectToLogin(filterContext.HttpContext);
                filterContext.HttpContext.Response.End();
                return;
            }
        }
        private void RedirectToLogin(HttpContextBase HttpContext)
        {
            HttpContext.Response.Redirect("/Account/Login");
        }

        private bool isLogin(HttpContextBase httpcontext)
        {
            // if (httpcontext.Session[Public_const_enum.LonginCookieName] != null)
            if (HttpHelper.GetCookie(Public_const_enum.LonginCookieName) != null)
                return true;
            else
                return false;
        }
    }
}
