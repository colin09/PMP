using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;

namespace com.pmp.mongo.service
{
    public class MgProjectService : BaseService<MgProject>
    {
        public List<MgProject> SearchById(long id)
        {
            var filter = Builders<MgProject>.Filter.Eq("Id", id);
            return Search(filter);
        }

        

    }
}
