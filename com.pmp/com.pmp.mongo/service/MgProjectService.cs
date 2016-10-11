using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;
using com.pmp.model.enums;
using com.pmp.model.response;

namespace com.pmp.mongo.service
{
    public class MgProjectService : BaseService<MgProject>
    {
        public void Init()
        {
            CreateCounter();
        }



        public MgProject GetOneById(long id)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            return Search(filter).FirstOrDefault();
        }


        public List<MgProject> GetAllActive(int pageIndex, int pageSize, out long total)
        {
            var filter = Builders<MgProject>.Filter.Eq("Status", (int)ProjectStatus.Default);
            total = 0L;

            return SearchByPage(filter, order => order.CreateTime, false, pageIndex, pageSize, out total);
        }
        public List<MgProject> GetAll0(int cUser, int gUser, int type, int state, int audit, int city, DateTime? date, PageInfo page, out long total)
        {
            var filter = Builders<MgProject>.Filter.Gt("Status", -6);
            if (audit > -2)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.AuditStatus, (AuditStatus)audit);
            if (cUser > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.CreatesUserID, cUser);
            if (gUser > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.ReceiveUserId, gUser);
            if (type > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.Category, (ProjectCategroy)type);
            if (state > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.Status, (ProjectStatus)state);
            if (city > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.CityId, city);
            if (date != null)
            {
                var endDate = date.Value.AddDays(1).Date;
                filter = filter & Builders<MgProject>.Filter.Gte(p => p.CreateTime, date.Value) & Builders<MgProject>.Filter.Lt(p => p.CreateTime, endDate);
            }
            total = 0L;
            return SearchByPage(filter, order => order.CreateTime, false, page.PageIndex, page.PageSize, out total);
        }


        public List<MgProject> GetAll(int cUser, int gUser, int type, int state, int city, DateTime? date, PageInfo page, out long total)
        {
            var filter = Builders<MgProject>.Filter.Gt("Status", -6);
            if (cUser > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.CreatesUserID, cUser);
            if (gUser > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.ReceiveUserId, gUser);
            if (type > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.Category, (ProjectCategroy)type);
            if (state > -1)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.Status, (ProjectStatus)state);
            if (city > 0)
                filter = filter & Builders<MgProject>.Filter.Eq(p => p.CityId, city);
            if (date != null)
            {
                var endDate = date.Value.AddDays(1).Date;
                filter = filter & Builders<MgProject>.Filter.Gte(p => p.CreateTime, date.Value) & Builders<MgProject>.Filter.Lt(p => p.CreateTime, endDate);
            }
            total = 0L;
            return SearchByPage(filter, order => order.CreateTime, false, page.PageIndex, page.PageSize, out total);
        }




        public void Create(MgProject m)
        {
            m.ID = GetNewId();
            Insert(m);
        }

        public bool Modify(MgProject m)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", m.ID);
            var update = Builders<MgProject>.Update.Set(p => p.Category, m.Category)
                .Set(p => p.Name, m.Name)
                .Set(p => p.ContractCode, m.ContractCode)
                .Set(p => p.Manager, m.Manager)
                .Set(p => p.Linkman, m.Linkman)
                .Set(p => p.Mobile, m.Mobile)
                .Set(p => p.Desc, m.Desc)
                .Set(p => p.StartTime, m.StartTime)
                .Set(p => p.EndTime, m.EndTime)
                .Set(p => p.Budget, m.Budget)
                .Set(p => p.ProvinceId, m.ProvinceId)
                .Set(p => p.CityId, m.CityId)
                .Set(p => p.FlieList, m.FlieList);

            return Update(filter, update) > 0;
        }


        public bool DeleteFile(int id, int fileIndex)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var m = Search(filter).FirstOrDefault();
            if (m == null)
                return false;
            var list = m.FlieList.Select(f => f.Index != fileIndex).ToList();
            var update = Builders<MgProject>.Update.Set(p => p.FlieList, m.FlieList);
            return Update(filter, update) > 0;
        }

        /*
        public bool AuditProject(int id, AuditStatus auditState, string desc)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var update = Builders<MgProject>.Update.Set(p => p.AuditStatus, auditState)
                .Set(p=>p.Status,ProjectStatus.Wait);

            return Update(filter, update) > 0;
        }*/

        public bool ModifyState(long id, ProjectStatus state)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var update = Builders<MgProject>.Update.Set(p => p.Status, state);

            return Update(filter, update) > 0;
        }

        public bool JoinProject(int id, BidUser user)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var project = GetOneById(id);
            if (project == null)
                return false;
            var list = project.BidUsers;
            if (list == null)
                list = new List<BidUser>();
            list.Add(user);

            var update = Builders<MgProject>.Update.Set(p => p.BidUsers, list);

            return Update(filter, update) > 0;
        }

        public bool GiveProject(int id, int userId, string desc)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var update = Builders<MgProject>.Update.Set(p => p.ReceiveUserId, userId)
                .Set(p => p.Status, ProjectStatus.Wait);

            return Update(filter, update) > 0;
        }


        public IList<MgProject> GetListByIds(List<int> ids)
        {
            var filter = Builders<MgProject>.Filter.In(m => m.ID, ids);
            return Search(filter);
        }

        public IList<MgProject> GetListByRUser(int userId)
        {
            var filter = Builders<MgProject>.Filter.Eq("ReceiveUserId", userId);
            return Search(filter);
        }

        public IList<MgProject> GetListByCUser(int userId)
        {
            var filter = Builders<MgProject>.Filter.Eq("CreatesUserID", userId);
            return Search(filter);
        }

        public bool AddProcess(MgProject m)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", m.ID);
            var update = Builders<MgProject>.Update.Set(p => p.ProcessDesc, m.ProcessDesc);

            return Update(filter, update) > 0;
        }


        public bool UpdateEva_State(MgProject m)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", m.ID);
            var update = Builders<MgProject>.Update.Set(p => p.IsEvaluate_E, m.IsEvaluate_E)
                .Set(p => p.IsEvaluate_I, m.IsEvaluate_I).Set(p => p.Status, m.Status);

            return Update(filter, update) > 0;
        }

    }
}
