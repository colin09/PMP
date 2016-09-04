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
        public List<MgProject> GetAll(int cUser,int gUser, int type, int state, int audit, PageInfo page, out long total)
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

            total = 0L;
            return SearchByPage(filter, order => order.CreateTime, false, page.PageIndex, page.PageSize, out total);
        }




        public void Create(MgProject m)
        {
            m.ID = GetNewId();
            Insert(m);
        }


        public bool AuditProject(int id, AuditStatus auditState, string desc)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            var update = Builders<MgProject>.Update.Set(p => p.AuditStatus, auditState);

            return Update(filter, update) > 0;
        }



    }
}
