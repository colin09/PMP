using com.pmp.common.mvc.attribute;
using com.pmp.common.mvc.ctl;
using com.pmp.model.enums;
using com.pmp.model.request;
using com.pmp.model.response;
using com.pmp.mongo.data;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{
    public class TaskController : BaseController
    {

        private readonly MgProjectService _projectService;
        public TaskController()
        {
            _projectService = new MgProjectService();

        }

        // GET: Task
        public ActionResult Index(int pageIndex = 1, int type = 0, int state = 0)
        {
            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(0, 0, type, state, (int)AuditStatus.Pass, page, out total);

            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;

            return View(list);
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
                CreatesUserID = this._Longin_UserId,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                AuditStatus = AuditStatus.Default,
                CreateTime = DateTime.Now.ToOADate()
            };

            _projectService.Create(project);
            return RedirectToAction("AuditList");
        }



        public ActionResult Detail(int id)
        {
            var project = _projectService.GetOneById(id);

            return View(project);
        }

        public ActionResult AuditList(AuditStatus audit = AuditStatus.Default, int pageIndex = 1, int type = 0, int state = 0)
        {
            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(this._Longin_UserId, 0, type, state, (int)audit, page, out total);

            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;
            ViewBag.level = this._Longin_UserLevel;

            return View(list);
        }
        public ActionResult AuditDetail(int id)
        {
            var project = _projectService.GetOneById(id);
            ViewBag.level = this._Longin_UserLevel;

            return View(project);
        }
        public ActionResult AuditSubmit(int id, AuditStatus auditState, string auditDesc)
        {
            log.Info($"state:{auditState}, desc:{auditDesc}");
            _projectService.AuditProject(id, auditState, auditDesc);
            return RedirectToAction("AuditList");
        }




        public ActionResult MyList(AuditStatus audit = AuditStatus.Default, int pageIndex = 1, int type = 0, int state = 0)
        {
            var cUser = 0;
            var rUser = 0;
            var auditState = (int)audit;
            if (this._Longin_UserLevel == (int)UserLevel.CompanyAdmin)
            {
                cUser = this._Longin_UserId;
                auditState = -99;
            }
            else
            {
                rUser = this._Longin_UserId;
                auditState = (int)AuditStatus.Pass;
            }
            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(cUser, rUser, type, state, auditState, page, out total);

            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;
            ViewBag.level = this._Longin_UserLevel;

            return View(list);
        }
    }
}