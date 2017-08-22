using com.pmp.common.mvc.ctl;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class ManageController : WebBaseController
    {

        private readonly MgUserService _userService;

        private readonly MgProjectService _projectService;

        public ManageController()
        {
            _userService = new MgUserService();
            _projectService = new MgProjectService();

        }




        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InitCounters()
        {
            _userService.Init();
            _projectService.Init();

            return RedirectToAction("Index");
        }


    }
}