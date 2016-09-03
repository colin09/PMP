using com.pmp.common.mvc.attribute;
using com.pmp.common.mvc.ctl;
using com.pmp.model.enums;
using com.pmp.model.request;
using com.pmp.mongo.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class TaskController : BaseController
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }



        //[UserAuthorize]
        public ActionResult CreateSubmint(TaskInfoReq task)
        {
            var project = new MgProject()
            {
                Code = task.Code,
                Name = task.Name,
                ContractCode = task.ContractCode,
                Status = ProjectStatus.Default,
                Manager = task.Manager,
                Linkman = task.Linkman,
                Mobile = task.Mobile,
                Desc = task.Desc,
                CreatesUserID = this.UserId,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                AuditStatus = AuditStatus.Default,
                CreatesTime = DateTime.Now.ToOADate()
            };






            return View();
        }


        public ActionResult List()
        {
            return View();
        }
    }
}