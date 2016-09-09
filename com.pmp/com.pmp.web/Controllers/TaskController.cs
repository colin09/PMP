using com.pmp.common.helper;
using com.pmp.common.mvc.attribute;
using com.pmp.common.mvc.ctl;
using com.pmp.model.data;
using com.pmp.model.enums;
using com.pmp.model.request;
using com.pmp.model.response;
using com.pmp.mongo.data;
using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.pmp.web.Controllers
{


    public class TaskController : BaseController
    {

        private readonly MgProjectService _projectService;
        private readonly MgSolutionService _solutionService;
        private readonly MgEvaluationService _evaluationService;
        private readonly MgCityService _cityService;


        public TaskController()
        {
            _projectService = new MgProjectService();
            _solutionService = new MgSolutionService();
            _evaluationService = new MgEvaluationService();
            _cityService = new MgCityService();
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
        public ActionResult CreateSubmint(TaskInfoReq task, HttpPostedFileBase[] files)
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
                Budget = task.Budget,
                CreateTime = DateTime.Now
            };
            var fileIndex = 1;
            foreach (var file in files)
            {
                var pFile = new ProjectFlie();
                pFile.Name = Path.GetFileName(file.FileName);
                pFile.FileType = 1;
                pFile.Path = Path.Combine(Request.MapPath("~/Upload"), $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}");

                var savePath = pFile.Path;
                file.SaveAs(savePath);

                project.FlieList.Add(pFile);
                fileIndex += 1;
            }

            project.ProcessDesc.Add(new ProjectProcess() { ProcessDesc = "发布项目。", UserID = this._Longin_UserId, CreateTime = DateTime.Now });

            _projectService.Create(project);
            return RedirectToAction("AuditList");
        }


        public ActionResult Detail(int id)
        {
            var project = _projectService.GetOneById(id);

            var slns = _solutionService.GetListByProId(id);
            ViewBag.slns = slns;

            var evas = _evaluationService.GetListByProId(id);
            ViewBag.evas = evas;

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


        public ActionResult CreateSln(int projectId, string desc, HttpPostedFileBase[] files)
        {
            if (files == null)
                return Content("没有选择文件", "text/plain");

            try
            {
                var sln = new MgSolution()
                {
                    ID = _projectService.GetNewId(),
                    ProjectId = projectId,
                    SlnDesc = desc,
                    //FileList = new List<string>() { "../upload/" + Path.GetFileName(file.FileName) },
                    UserId = this._Longin_UserId,
                };

                var fileIndex = 1;
                foreach (var file in files)
                {
                    var pFile = new ProjectFlie();
                    pFile.Name = Path.GetFileName(file.FileName);
                    pFile.FileType = 1;
                    pFile.Path = Path.Combine(Request.MapPath("~/Upload"), $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}");

                    var savePath = pFile.Path;
                    file.SaveAs(savePath);

                    sln.FileList.Add(pFile);
                    fileIndex += 1;
                }
                _solutionService.Insert(sln);
            }
            catch (Exception ex)
            {
                log.Info(ex);
            }


            return RedirectToAction("Detail", new { id = projectId });
        }






        #region  -   评价相关  -

        public ActionResult GiveEvaluate(int projectId, int graed, int score,string desc)
        {
            var m = new MgEvaluation()
            {
                ProjectId = projectId,
                EvaluateType = this._Longin_UserLevel == (int)UserLevel.CompanyAdmin ? 1 : 2,
                Grade = graed,
                Score = score,
                Desc = desc,
                UserId = this._Longin_UserId
            };
            _evaluationService.Insert(m);

            return RedirectToAction("Detail", new { id = projectId });
        }



        #endregion


        public ActionResult GetCityList(int parentId)
        {
            var list = _cityService.GetListByParentId(parentId);
            return Json(list,JsonRequestBehavior.AllowGet);
        }




    }



}