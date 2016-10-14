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
        private readonly MgUserService _userService;
        private readonly MgMessageService _msgService;


        public TaskController()
        {
            _projectService = new MgProjectService();
            _solutionService = new MgSolutionService();
            _evaluationService = new MgEvaluationService();
            _cityService = new MgCityService();
            _userService = new MgUserService();
            _msgService = new MgMessageService();
        }

        // GET: Task
        public ActionResult Index(int pageIndex = 1, int type = 0, int state = -1,int province=0, int city = 0, DateTime? date = null)
        {
            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(0, 0, type, state, province, city, date, page, out total);

            var cUserIds = list.Select(l => l.CreatesUserID).ToList();
            var rUserIds = list.Select(l => l.ReceiveUserId).ToList();
            cUserIds.AddRange(rUserIds);
            var userList = _userService.GetUserListByIds(cUserIds);

            var cityIds = list.Select(l => l.CityId).ToList();
            var provIds = list.Select(l => l.ProvinceId).ToList();
            cityIds.AddRange(provIds);
            var cityList = _cityService.GetListByIds(cityIds);

            var result = list.Select(l => new TaskInfoRes
            {
                ID = l.ID,
                CategoryId = (int)l.Category,
                CategoryName = l.Category.GetName(),
                Code = l.Code,
                ContractCode = l.ContractCode,
                Status = (int)l.Status,
                StatusDesc = l.Status.GetName(),
                Name = l.Name,
                Manager = l.Manager,
                Linkman = l.Linkman,
                Mobile = l.Mobile,
                Desc = l.Desc,
                CUserID = l.CreatesUserID,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                AuditStatus = (int)l.AuditStatus,
                AuditStatusDesc = l.AuditStatus.GetName(),
                RUserId = l.ReceiveUserId,
                CUserName = userList.FirstOrDefault(u => u.Id == l.CreatesUserID)?.Name,
                RUserName = userList.FirstOrDefault(u => u.Id == l.ReceiveUserId)?.Name,
                CityName = cityList.FirstOrDefault(c => c.ID == l.CityId)?.Name,
                ProvinceName = cityList.FirstOrDefault(c => c.ID == l.ProvinceId)?.Name,
                CEvaluate = l.IsEvaluate_E,
                PEvaluate = l.IsEvaluate_I,
                FlieList = l.FlieList,
                ProcessDesc = l.ProcessDesc,
                Budget = l.Budget,
                CTime = l.CreateTime
            }).ToList();



            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;

            return View(result);
        }

        [Authorization]
        public ActionResult Create()
        {
            return View();
        }


        [ValidateInput(false)]
        [Authorization]
        public ActionResult CreateSubmint(TaskInfoReq task, HttpPostedFileBase[] files)
        {
            var project = new MgProject()
            {
                Category = (ProjectCategroy)task.Catetory,
                Code = $"{task.Catetory}-{DateTime.Now.ToOADate() }".Replace(".", ""),
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
                CreateTime = DateTime.Now,

                ProvinceId = task.Province,
                CityId = task.City,
            };
            var fileIndex = 1;

            if (files != null)
                foreach (var file in files)
                {
                    if (file.ContentLength > 1024 * 1024 * 2)
                        return Json("{'error':'文件超大。'}");

                    var saveName = $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}";
                    var pFile = new ProjectFlie();
                    pFile.Name = Path.GetFileName(file.FileName);
                    pFile.FileType = 1;
                    pFile.Path = $"../Upload/{saveName}";
                    pFile.Index = fileIndex;

                    var savePath = Path.Combine(Request.MapPath("~/Upload"), saveName);
                    file.SaveAs(savePath);

                    project.FlieList.Add(pFile);
                    fileIndex += 1;
                }

            project.ProcessDesc.Add(new ProjectProcess() { ProcessDesc = "发布项目。", UserID = this._Longin_UserId, CreateTime = DateTime.Now });

            _projectService.Create(project);
            return RedirectToAction("MyList");
        }


        public ActionResult Detail(int id)
        {
            var project = _projectService.GetOneById(id);
            if (project == null)
                return View();
            var evas = _evaluationService.GetListByProId(id);
            var evaIds = evas.Select(e => e.UserId).ToList();

            var userIds = new List<int> { project.CreatesUserID, project.ReceiveUserId };
            var procs = project.ProcessDesc.Select(p => p.UserID).ToList();
            var slns = _solutionService.GetListByProId(id);
            var slnIds = slns.Select(s => s.UserId).ToList();
            var joinIds = project.BidUsers?.Select(b => b.UserId).ToList();


            userIds.AddRange(procs);
            userIds.AddRange(slnIds);
            userIds.AddRange(evaIds);
            if (joinIds != null)
                userIds.AddRange(joinIds);
            var userList = _userService.GetUserListByIds(userIds.Distinct().ToList());
            var cityList = _cityService.GetListByIds(new List<int> { project.CityId, project.ProvinceId });

            project.ProcessDesc.ForEach(p =>
            {
                p.UserName = userList.FirstOrDefault(u => u.Id == p.UserID)?.Name;
            });

            var result = new TaskInfoRes()
            {
                ID = project.ID,
                CategoryId = (int)project.Category,
                CategoryName = project.Category.GetName(),
                Code = project.Code,
                ContractCode = project.ContractCode,
                Status = (int)project.Status,
                StatusDesc = project.Status.GetName(),
                Name = project.Name,
                Manager = project.Manager,
                Linkman = project.Linkman,
                Mobile = project.Mobile,
                Desc = project.Desc,
                CUserID = project.CreatesUserID,
                Budget = project.Budget,
                StartTime = project.StartTime,
                EndTime = project.EndTime,
                AuditStatus = (int)project.AuditStatus,
                AuditStatusDesc = project.AuditStatus.GetName(),
                RUserId = project.ReceiveUserId,
                CUserName = userList.FirstOrDefault(u => u.Id == project.CreatesUserID)?.Name,
                RUserName = userList.FirstOrDefault(u => u.Id == project.ReceiveUserId)?.Name,
                CityName = cityList.FirstOrDefault(c => c.ID == project.CityId)?.Name,
                ProvinceName = cityList.FirstOrDefault(c => c.ID == project.ProvinceId)?.Name,
                CEvaluate = project.IsEvaluate_E,
                PEvaluate = project.IsEvaluate_I,
                FlieList = project.FlieList,
                ProcessDesc = project.ProcessDesc,
                CTime = project.CreateTime,
                UTime = project.UpdateTime,
                BidUsers = project.BidUsers?.Select(b => new TaskJoinRes()
                {
                    UserId = b.UserId,
                    CTime = b.CTime,
                    State = b.Status,
                    UserName = userList.FirstOrDefault(u => u.Id == b.UserId)?.Name,
                }).ToList()
            };
            ViewBag.slns = slns.Select(s => new TaskSlnRes
            {
                SlnDesc = s.SlnDesc,
                FileList = s.FileList,
                UserName = userList.FirstOrDefault(u => u.Id == s.UserId)?.Name,
                CTime = s.CTime,
                UTime = s.UTime
            }).ToList();
            ViewBag.evas = evas.Select(e => new TaskEvaRes
            {
                Grade = e.Grade,
                Score = e.Score,
                Desc = e.Desc,
                UserName = userList.FirstOrDefault(u => u.Id == e.UserId)?.Name,
                CTime = e.CTime,
            }).ToList();

            log.Info($"========>loginUser:{ _Longin_UserId}, {result.BidUsers.ToJson()}");
            var isBided = result.BidUsers?.Where(b => b.UserId == _Longin_UserId).Any() ?? false;
            ViewBag.isBid = isBided;
            log.Info($"isBid : {isBided}");

            return View(result);
        }

        [Authorization]
        public ActionResult Modify(int id)
        {
            var project = _projectService.GetOneById(id);
            if (project == null)
                return View();

            return View(project);
        }

        [Authorization]
        public ActionResult Delete(int id)
        {
            var project = _projectService.GetOneById(id);
            if (project == null)
                return View("MyList");

            if (project.CreatesUserID != _Longin_UserId)
                return View("MyList");

            _projectService.Delete(id);

            return Json("seccess", JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [Authorization]
        public ActionResult ModifySubmit(TaskInfoReq task, HttpPostedFileBase[] files)
        {
            var project = _projectService.GetOneById(task.Id);
            if (project == null)
                return RedirectToAction("MyList");

            if (project.CreatesUserID != _Longin_UserId)
                return RedirectToAction("MyList");

            project.Category = (ProjectCategroy)task.Catetory;
            //project.Code = $"{task.Catetory}-{DateTime.Now.ToOADate() }".Replace(".", ""),
            project.Name = task.Name;
            project.ContractCode = task.ContractCode;
            //project.Status = ProjectStatus.Default,
            project.Manager = task.Manager;
            project.Linkman = task.Linkman;
            project.Mobile = task.Mobile;
            project.Desc = task.Desc;
            //project.CreatesUserID = this._Longin_UserId,
            project.StartTime = task.StartTime;
            project.EndTime = task.EndTime;
            //project.AuditStatus = AuditStatus.Default,
            project.Budget = task.Budget;
            //project.CreateTime = DateTime.Now,

            project.ProvinceId = task.Province;
            project.CityId = task.City;
            project.FlieList = project.FlieList ?? new List<ProjectFlie>();

            var fileIndex = (project.FlieList?.Max(m => m.Index) + 1) ?? 1;

            if (files != null)
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 1024 * 1024 * 2)
                            return Json("{'error':'文件超大。'}");

                        var saveName = $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}";
                        var pFile = new ProjectFlie();
                        pFile.Name = Path.GetFileName(file.FileName);
                        pFile.FileType = 1;
                        pFile.Path = $"../Upload/{saveName}";
                        pFile.Index = fileIndex;

                        var savePath = Path.Combine(Request.MapPath("~/Upload"), saveName);
                        file.SaveAs(savePath);

                        project.FlieList.Add(pFile);
                        fileIndex += 1;
                    }
                }

            _projectService.Modify(project);
            log.Info($"go to detail id :{task.Id}");
            return RedirectToAction("MyList");
            //return View("AuditDetail", new { id = task.Id });
        }


        [Authorization]
        public ActionResult DeleteProjectFile(int id, int fileIndex)
        {
            var project = _projectService.GetOneById(id);
            if (project == null)
                return View();

            return View(project);
        }



        [Authorization]
        public ActionResult Notice()
        {
            var list = _msgService.GetListByUId(_Longin_UserId);
            return View(list);
        }

        [Authorization]
        public ActionResult MyList(int pageIndex = 1, int type = 0, int state = -1)
        {
            var cUser = 0;
            var rUser = 0;

            if (this._Longin_UserLevel == (int)UserLevel.CompanyAdmin)
                cUser = this._Longin_UserId;
            else
                rUser = this._Longin_UserId;

            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(cUser, rUser, type, state,0, 0, null, page, out total);

            ViewBag.state = state;
            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;
            ViewBag.level = this._Longin_UserLevel;

            return View(list);
        }


        [Authorization]
        public ActionResult AuditDetail(int id)
        {
            var project = _projectService.GetOneById(id);
            if (project == null)
                return View();
            var evas = _evaluationService.GetListByProId(id);
            var evaIds = evas.Select(e => e.UserId).ToList();

            var userIds = new List<int> { project.CreatesUserID, project.ReceiveUserId };
            var procs = project.ProcessDesc.Select(p => p.UserID).ToList();
            var slns = _solutionService.GetListByProId(id);
            var slnIds = slns.Select(s => s.UserId).ToList();
            var joinIds = project.BidUsers?.Select(b => b.UserId).ToList();

            userIds.AddRange(procs);
            userIds.AddRange(slnIds);
            userIds.AddRange(evaIds);
            if (joinIds != null)
                userIds.AddRange(joinIds);
            var userList = _userService.GetUserListByIds(userIds.Distinct().ToList());
            var cityList = _cityService.GetListByIds(new List<int> { project.CityId, project.ProvinceId });

            project.ProcessDesc.ForEach(p =>
            {
                p.UserName = userList.FirstOrDefault(u => u.Id == p.UserID)?.Name;
            });

            var result = new TaskInfoRes()
            {
                ID = project.ID,
                CategoryId = (int)project.Category,
                CategoryName = project.Category.GetName(),
                Code = project.Code,
                ContractCode = project.ContractCode,
                Status = (int)project.Status,
                StatusDesc = project.Status.GetName(),
                Name = project.Name,
                Manager = project.Manager,
                Linkman = project.Linkman,
                Mobile = project.Mobile,
                Desc = project.Desc,
                CUserID = project.CreatesUserID,
                Budget = project.Budget,
                StartTime = project.StartTime,
                EndTime = project.EndTime,
                AuditStatus = (int)project.AuditStatus,
                AuditStatusDesc = project.AuditStatus.GetName(),
                RUserId = project.ReceiveUserId,
                CUserName = userList.FirstOrDefault(u => u.Id == project.CreatesUserID)?.Name,
                RUserName = userList.FirstOrDefault(u => u.Id == project.ReceiveUserId)?.Name,
                CityName = cityList.FirstOrDefault(c => c.ID == project.CityId)?.Name,
                ProvinceName = cityList.FirstOrDefault(c => c.ID == project.ProvinceId)?.Name,
                CEvaluate = project.IsEvaluate_E,
                PEvaluate = project.IsEvaluate_I,
                FlieList = project.FlieList,
                ProcessDesc = project.ProcessDesc,
                CTime = project.CreateTime,
                UTime = project.UpdateTime,
                BidUsers = project.BidUsers?.Select(b => new TaskJoinRes()
                {
                    UserId = b.UserId,
                    CTime = b.CTime,
                    State = b.Status,
                    UserName = userList.FirstOrDefault(u => u.Id == b.UserId)?.Name,
                }).ToList()
            };

            ViewBag.slns = slns.Select(s => new TaskSlnRes
            {
                SlnDesc = s.SlnDesc,
                FileList = s.FileList,
                UserName = userList.FirstOrDefault(u => u.Id == s.UserId)?.Name,
                CTime = s.CTime,
                UTime = s.UTime
            }).ToList();
            ViewBag.evas = evas.Select(e => new TaskEvaRes
            {
                Grade = e.Grade,
                Score = e.Score,
                Desc = e.Desc,
                UserName = userList.FirstOrDefault(u => u.Id == e.UserId)?.Name,
                CTime = e.CTime
            }).ToList();

            return View(result);
        }

        [Authorization]
        public ActionResult JoinProject(int projectId)
        {
            _projectService.JoinProject(projectId, new BidUser()
            {
                UserId = this._Longin_UserId,
                CTime = DateTime.Now,
                Status = DataStatus.Normal
            });

            return RedirectToAction("Detail", new { id = projectId });
        }

        [Authorization]
        public ActionResult GiveProject(int projectId, int userId, string desc = "")
        {
            if (userId > 0)
                _projectService.GiveProject(projectId, userId, desc);


            var project = _projectService.GetOneById(projectId);
            if (project != null && project.BidUsers != null)
            {
                var users = project.BidUsers.Where(u => u.UserId != userId).Select(u => u.UserId).ToList();
                users.ForEach(uid =>
                {
                    var msg = new MgMessage()
                    {
                        CreateUser = _Longin_UserId,
                        CTime = DateTime.Now,
                        Msg = $"任务[{project.Name}]已被抢单，再接再厉，您可以查看其它项目。",
                        ToUserId = uid,
                        ProjectCode = project.Code,
                        Type = "notice"
                    };
                    _msgService.Insert(msg);
                });
            }

            return RedirectToAction("AuditDetail", new { id = projectId });
        }


        [Authorization]
        public ActionResult CreateSln(int projectId, string desc, HttpPostedFileBase[] files)
        {
            if (files == null)
                return Json(new MsgRes(500, "请选择方案文件。"), JsonRequestBehavior.AllowGet);

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
                if (files != null)
                    foreach (var file in files)
                    {
                        if (file == null)
                            continue;
                        var saveName = $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}";
                        var pFile = new ProjectFlie();
                        pFile.Name = Path.GetFileName(file.FileName);
                        pFile.FileType = 1;
                        pFile.Path = $"../Upload/{saveName}";

                        file.SaveAs(Path.Combine(Request.MapPath("~/Upload"), saveName));

                        sln.FileList.Add(pFile);
                        fileIndex += 1;
                    }
                _solutionService.Insert(sln);

                _projectService.ModifyState(projectId, ProjectStatus.Action);
            }
            catch (Exception ex)
            {
                log.Info(ex);
            }


            return RedirectToAction("AuditDetail", new { id = projectId });
        }


        public ActionResult GetCityList(int parentId)
        {
            var list = _cityService.GetListByParentId(parentId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /*
        [Authorization]
        public ActionResult AddProcess(long projectId, string desc, HttpPostedFileBase[] files, int overState = 0)
        {
            var overMsg = overState > 0 ? " - 完成" : " - 未完";
            WriteProcess(projectId, $"[进度说明{overMsg}]{desc}", files);
            if (overState > 0)
                _projectService.ModifyState(projectId, ProjectStatus.Audit);

            return RedirectToAction("AuditDetail", new { id = projectId });
        }*/


        [Authorization]
        public ActionResult AddProcess(long projectId, ProjectProcess process, HttpPostedFileBase[] files, int overState = 0)
        {
            var overMsg = overState > 0 ? " - 完成" : " - 未完";
            WriteProcess(projectId, process, files);
            if (overState > 0)
                _projectService.ModifyState(projectId, ProjectStatus.Audit);

            return RedirectToAction("AuditDetail", new { id = projectId });
        }


        public bool WriteProcess(long projectId, ProjectProcess process, HttpPostedFileBase[] files)
        {
            try
            {
                var project = _projectService.GetOneById(projectId);
                if (project != null)
                {
                    process.UserID = this._Longin_UserId;
                    process.UserName = this._Longin_RealName;
                    process.CreateTime = DateTime.Now;

                    var fileIndex = 1;
                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            if (file != null)
                            {
                                var saveName = $"{DateTime.Now.ToOADate()}-{fileIndex}-{Path.GetExtension(file.FileName)}";
                                var pFile = new ProjectFlie();
                                pFile.Name = Path.GetFileName(file.FileName);
                                pFile.FileType = 1;
                                pFile.Path = $"../Upload/{saveName}";

                                file.SaveAs(Path.Combine(Request.MapPath("~/Upload/"), saveName));

                                process.FlieList.Add(pFile);
                            }
                        }
                    }
                    project.ProcessDesc.Add(process);

                    _projectService.AddProcess(project);
                }


                return true;
            }
            catch (Exception ex)
            {
                log.Info(ex);
            }
            return false;
        }




        /*
        [Authorization]
        public ActionResult AuditList(int pageIndex = 1, int type = 0, int state = 0)
        {
            var page = new PageInfo() { PageIndex = pageIndex };
            var total = 0L;
            var list = _projectService.GetAll(this._Longin_UserId, 0, type, state, 0, null, page, out total);

            ViewBag.pageIndex = pageIndex;
            ViewBag.total = total;
            ViewBag.level = this._Longin_UserLevel;

            return View(list);
        }*/

        [Authorization]
        public ActionResult AuditSubmit(int projectId, int isPass, string desc)
        {
            var state = ProjectStatus.Evaluation;
            var r = "验收通过";
            if (isPass < 1)
            {
                state = ProjectStatus.Action;
                r = "验收不通过";
            }

            _projectService.ModifyState(projectId, state);

            var proc = new ProjectProcess()
            {
                ProcessDesc = $"[{r}]{desc}"
            };

            WriteProcess((long)projectId, proc, null);

            return RedirectToAction("AuditDetail", new { id = projectId });
        }


        #region  -   评价相关  -
        [Authorization]
        public ActionResult GiveEvaluate(int projectId, int grade, int score, string desc)
        {
            var m = new MgEvaluation()
            {
                ProjectId = projectId,
                EvaluateType = this._Longin_UserLevel == (int)UserLevel.CompanyAdmin ? 1 : 2,
                Grade = grade,
                Score = grade * 10,
                Desc = desc,
                UserId = this._Longin_UserId
            };
            _evaluationService.Insert(m);

            var project = _projectService.GetOneById(projectId);
            if (project != null)
            {
                if (project.CreatesUserID == this._Longin_UserId)
                {
                    m.ToUserId = project.ReceiveUserId;
                    project.IsEvaluate_E = 1;

                    //计算任务人信誉
                    EvaluationGrade(project.ReceiveUserId);
                }
                else if (project.ReceiveUserId == this._Longin_UserId)
                {
                    m.ToUserId = project.CreatesUserID;
                    project.IsEvaluate_I = 1;

                    //计算雇主信誉

                }
                if (project.IsEvaluate_I > 0 && project.IsEvaluate_E > 0)
                    project.Status = ProjectStatus.Over;

                _projectService.UpdateEva_State(project);
            }

            return RedirectToAction("AuditDetail", new { id = projectId });
        }


        private void EvaluationGrade(int userId)
        {
            var avg = _evaluationService.CalculateGradeAvg(userId);
            _userService.ModifyPersonEvalScore(userId, avg);
        }


        [Authorization]
        public ActionResult Evaluation()
        {
            var total = 0;
            var result = new List<TaskEvaRes>();
            var list = _evaluationService.GetListByUserId(this._Longin_UserId);
            if (list != null)
            {
                total = list.Count();

                var proIds = list.Select(e => e.ProjectId).ToList();
                var prolist = _projectService.GetListByIds(proIds);

                var userIds = list.Select(l => l.UserId);
                var userList = _userService.GetUserListByIds(userIds.Distinct().ToList());

                result = list.Select(e => new TaskEvaRes
                {
                    Grade = e.Grade,
                    Score = e.Score,
                    Desc = e.Desc,
                    UserName = userList.FirstOrDefault(u => u.Id == e.UserId)?.Name,
                    ProjectName = prolist.FirstOrDefault(p => p.ID == e.ProjectId)?.Name ?? "--",
                    CTime = e.CTime
                }).ToList();
            }

            ViewBag.total = total;

            return View(result);
        }


        [Authorization]
        public ActionResult REvaluation()
        {
            var total = 0;
            var result = new List<TaskEvaRes>();
            var list = new List<MgEvaluation>();
            if (this._Longin_UserLevel == 2)
                list = _evaluationService.GetListByComUserId(this._Longin_UserId);
            else
                list = _evaluationService.GetListByRUserId(this._Longin_UserId);
            if (list != null)
            {
                total = list.Count();

                var proIds = list.Select(e => e.ProjectId).ToList();
                var prolist = _projectService.GetListByIds(proIds);

                var userIds = list.Select(l => l.UserId);
                var userList = _userService.GetUserListByIds(userIds.Distinct().ToList());

                result = list.Select(e => new TaskEvaRes
                {
                    Grade = e.Grade,
                    Score = e.Score,
                    Desc = e.Desc,
                    UserName = userList.FirstOrDefault(u => u.Id == e.UserId)?.Name,
                    ProjectName = prolist.FirstOrDefault(p => p.ID == e.ProjectId)?.Name ?? "--",
                    CTime = e.CTime
                }).ToList();
            }

            ViewBag.total = total;
            ViewBag.level = this._Longin_UserLevel;

            return View("Evaluation", result);
        }


        #endregion





    }



}