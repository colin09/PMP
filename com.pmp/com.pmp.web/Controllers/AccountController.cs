using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class AccountController : Controller
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

        public ActionResult SignIn()
        {
            return View();
        }
    }
}