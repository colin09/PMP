using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using com.pmp.mongo.service;
using com.pmp.mongodbs.data;

namespace com.pmp.mongodbs.service
{
    public class MgUserService : BaseService<MgUser>
    { 
        public List<MgUser> SearchById(long id)
        {
            var filter = Builders<MgUser>.Filter.Eq("Id", id);
            return Search(filter);
        }
    }
}
