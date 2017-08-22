using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
    public class MgMessageService : BaseService<MgMessage>
    {



        public List<MgMessage> GetListByUId(int userId)
        {
            var filter = Builders<MgMessage>.Filter.Eq("ToUserId", userId);
            return Search(filter);
        }



        public int GetNoReadCount(int userId)
        {
            var filter = Builders<MgMessage>.Filter.Eq("ToUserId", userId);
            filter = filter & Builders<MgMessage>.Filter.Eq("IsRead", false);
            return Search(filter).Count();
        }






























    }

}
