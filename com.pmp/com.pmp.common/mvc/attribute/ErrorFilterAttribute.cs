using com.pmp.common.logger;
using System;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.common.mvc.attribute
{
    public class ErrorFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        //创建 BaseController，添加 [ErrorFilterAttribute] 使用此过滤实现 Exception 过滤；




        public void OnException(ExceptionContext filterContext)
        {
            //Exception Error = filterContext.Exception;
            //string message = Error.Message;//错误信息
            //string url = HttpContext.Current.Request.RawUrl;//错误发生地址

            //Logger.Current().Error(Error);

            //filterContext.ExceptionHandled = true;
            //filterContext.Result = new RedirectResult("/Home/Error/");//跳转至错误提示页面
        }
    }
}
