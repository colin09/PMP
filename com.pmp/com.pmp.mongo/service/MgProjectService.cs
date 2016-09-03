using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;
using com.pmp.model.enums;

namespace com.pmp.mongo.service
{
    public class MgProjectService : BaseService<MgProject>
    {
        public void Init()
        {
            CreateCounter();
        }



        public List<MgProject> SearchById(long id)
        {
            var filter = Builders<MgProject>.Filter.Eq("ID", id);
            return Search(filter);
        }


        public List<MgProject> GetAllActive(int pageIndex, int pageSize, out long total)
        {
            var filter = Builders<MgProject>.Filter.Eq("Status", (int)ProjectStatus.Default);
            total = 0L;

            return SearchByPage(filter, order => order.CreatesTime, false, pageIndex, pageSize, out total);
        }




        public void Create(MgProject m)
        {
            m.ID = GetNewId();
            Insert(m);
        }




    }
}
